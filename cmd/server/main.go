package main

import (
	"context"
	"log/slog"
	"net"
	"os"
	"os/signal"
	"syscall"

	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"

	"github.com/valeravorobjev/lemming-trip/internal/config"
	"github.com/valeravorobjev/lemming-trip/internal/database"
	"github.com/valeravorobjev/lemming-trip/internal/interceptors"
	auditpb "github.com/valeravorobjev/lemming-trip/internal/pb/audit"
	identitypb "github.com/valeravorobjev/lemming-trip/internal/pb/identity"
	"github.com/valeravorobjev/lemming-trip/internal/services/audit"
	"github.com/valeravorobjev/lemming-trip/internal/services/identity"
)

func main() {
	ctx, cancel := signal.NotifyContext(context.Background(), syscall.SIGINT, syscall.SIGTERM)
	defer cancel()

	// Настраиваем структурированное логирование
	logger := slog.New(slog.NewJSONHandler(os.Stdout, &slog.HandlerOptions{
		Level: slog.LevelInfo,
	}))

	// Загружаем конфигурацию
	cfg, err := config.Load()
	if err != nil {
		logger.Error("failed to load config", "error", err)
		os.Exit(1)
	}

	// Подключаемся к базе данных
	db, err := database.NewConnection(cfg.Database)
	if err != nil {
		logger.Error("failed to connect to database", "error", err)
		os.Exit(1)
	}
	defer db.Close()

	// Создаем gRPC сервер с интерсепторами
	grpcServer := grpc.NewServer(
		grpc.ChainUnaryInterceptor(
			interceptors.Recovery(logger),
			interceptors.Logging(logger),
			interceptors.Validation(logger),
			interceptors.Auth(logger),
		),
	)

	// Включаем reflection для разработки
	reflection.Register(grpcServer)

	// Создаем и регистрируем сервисы
	identityService := identity.NewService(logger, db)
	identitypb.RegisterIdentityServiceServer(grpcServer, identityService)

	auditService := audit.NewService(logger, db)
	auditpb.RegisterAuditServiceServer(grpcServer, auditService)

	// Создаем TCP listener
	listener, err := net.Listen("tcp", cfg.GRPCAddress)
	if err != nil {
		logger.Error("failed to create listener", "error", err)
		os.Exit(1)
	}

	logger.Info("starting gRPC server", "address", listener.Addr().String())

	// Запускаем сервер в горутине
	errChan := make(chan error, 1)
	go func() {
		if err := grpcServer.Serve(listener); err != nil {
			errChan <- err
		}
	}()

	// Ждем сигнала завершения или ошибки
	select {
	case <-ctx.Done():
		logger.Info("shutting down gRPC server gracefully")
		grpcServer.GracefulStop()
		logger.Info("server stopped")
	case err := <-errChan:
		logger.Error("server error", "error", err)
		os.Exit(1)
	}
}
