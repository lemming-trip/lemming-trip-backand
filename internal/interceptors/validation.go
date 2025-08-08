package interceptors

import (
	"context"
	"log/slog"

	"google.golang.org/grpc"
)

func Validation(logger *slog.Logger) grpc.UnaryServerInterceptor {
	return func(ctx context.Context, req interface{}, info *grpc.UnaryServerInfo, handler grpc.UnaryHandler) (interface{}, error) {
		// Здесь можно добавить общую валидацию всех запросов
		// Например:
		// - Проверка максимального размера запроса
		// - Валидация общих полей (UUID format, email format)
		// - Rate limiting по методам

		logger.Debug("validating request", "method", info.FullMethod)

		return handler(ctx, req)
	}
}
