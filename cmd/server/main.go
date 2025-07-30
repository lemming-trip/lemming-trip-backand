package main

import (
	"context"
	"log"
	"net"

	"google.golang.org/grpc"

	modelsv1 "github.com/valeravorobjev/lemming-trip/internal/gen/lemmingtrip/models/v1"
	servicesv1 "github.com/valeravorobjev/lemming-trip/internal/gen/lemmingtrip/services/v1"
)

// Реализация сервера
type lemmingTripServer struct {
	servicesv1.UnimplementedLemmingTripServiceServer
}

// Реализация метода CreateUser
func (s *lemmingTripServer) CreateUser(ctx context.Context, req *modelsv1.CreateUserRequest) (*modelsv1.CreateUserResponse, error) {
	log.Printf("Received CreateUser request: %+v", req)

	// Здесь будет ваша бизнес-логика
	// Например, создание пользователя в базе данных

	// Пример ответа
	response := &modelsv1.CreateUserResponse{
		Id:    12345,
		Name:  "John Doe",
		Email: "john.doe@lemmingtrip.com",
	}

	log.Printf("Sending CreateUser response: %+v", response)
	return response, nil
}

func main() {
	// Порт для gRPC сервера
	const port = ":50051"

	// Создание TCP листенера
	lis, err := net.Listen("tcp", port)
	if err != nil {
		log.Fatalf("Failed to listen on port %s: %v", port, err)
	}

	// Создание gRPC сервера
	server := grpc.NewServer(
		// Добавляем middleware для логирования (опционально)
		grpc.UnaryInterceptor(loggingInterceptor),
	)

	// Регистрация нашего сервиса
	lemmingTripSrv := &lemmingTripServer{}
	servicesv1.RegisterLemmingTripServiceServer(server, lemmingTripSrv)

	if err := server.Serve(lis); err != nil {
		log.Fatalf("Failed to serve gRPC server: %v", err)
	}
}

// Middleware для логирования запросов
func loggingInterceptor(ctx context.Context, req any, info *grpc.UnaryServerInfo, handler grpc.UnaryHandler) (any, error) {
	log.Printf("gRPC call: %s", info.FullMethod)

	resp, err := handler(ctx, req)

	if err != nil {
		log.Printf("gRPC call %s failed: %v", info.FullMethod, err)
	} else {
		log.Printf("gRPC call %s completed successfully", info.FullMethod)
	}

	return resp, err
}
