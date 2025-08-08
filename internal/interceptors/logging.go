package interceptors

import (
	"context"
	"log/slog"
	"time"

	"google.golang.org/grpc"
	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/status"
)

func Logging(logger *slog.Logger) grpc.UnaryServerInterceptor {
	return func(ctx context.Context, req interface{}, info *grpc.UnaryServerInfo, handler grpc.UnaryHandler) (interface{}, error) {
		start := time.Now()

		logger.Info("grpc request started", "method", info.FullMethod)

		resp, err := handler(ctx, req)

		duration := time.Since(start)

		if err != nil {
			// Получаем код статуса gRPC
			code := codes.Unknown
			if s, ok := status.FromError(err); ok {
				code = s.Code()
			}

			logger.Error("grpc request completed with error",
				"method", info.FullMethod,
				"duration", duration,
				"grpc_code", code.String(),
				"error", err.Error())
		} else {
			logger.Info("grpc request completed successfully",
				"method", info.FullMethod,
				"duration", duration)
		}

		return resp, err
	}
}
