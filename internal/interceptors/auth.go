package interceptors

import (
	"context"
	"log/slog"
	"strings"

	"google.golang.org/grpc"
	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/metadata"
	"google.golang.org/grpc/status"
)

func Auth(logger *slog.Logger) grpc.UnaryServerInterceptor {
	return func(ctx context.Context, req interface{}, info *grpc.UnaryServerInfo, handler grpc.UnaryHandler) (interface{}, error) {
		// Методы, которые не требуют авторизации
		publicMethods := map[string]bool{
			"/lemmingtrip.identity.IdentityService/RegisterLocal":        true,
			"/lemmingtrip.identity.IdentityService/RegisterOAuth":        true,
			"/lemmingtrip.identity.IdentityService/CheckAvailability":    true,
			"/lemmingtrip.identity.IdentityService/LoginLocal":           true,
			"/lemmingtrip.identity.IdentityService/LoginOAuth":           true,
			"/lemmingtrip.identity.IdentityService/ActivateAccount":      true,
			"/lemmingtrip.identity.IdentityService/RequestPasswordReset": true,
			"/lemmingtrip.identity.IdentityService/ResetPassword":        true,
			"/lemmingtrip.identity.IdentityService/SendMagicLink":        true,
			"/lemmingtrip.identity.IdentityService/ConfirmMagicLink":     true,
		}

		// Если метод публичный, пропускаем проверку авторизации
		if publicMethods[info.FullMethod] {
			logger.Debug("public method, skipping auth", "method", info.FullMethod)
			return handler(ctx, req)
		}

		// Получаем метаданные из контекста
		md, ok := metadata.FromIncomingContext(ctx)
		if !ok {
			return nil, status.Error(codes.Unauthenticated, "missing metadata")
		}

		// Проверяем наличие заголовка авторизации
		authValues := md.Get("authorization")
		if len(authValues) == 0 {
			return nil, status.Error(codes.Unauthenticated, "missing authorization header")
		}

		authHeader := authValues[0]
		if !strings.HasPrefix(authHeader, "Bearer ") {
			return nil, status.Error(codes.Unauthenticated, "invalid authorization header format")
		}

		token := strings.TrimPrefix(authHeader, "Bearer ")
		if token == "" {
			return nil, status.Error(codes.Unauthenticated, "missing token")
		}

		// TODO: Валидировать токен
		// - JWT токен или session токен из базы данных user_sessions
		// - Проверить срок действия
		// - Добавить identity_id в контекст для использования в сервисах

		logger.Debug("request authenticated",
			"method", info.FullMethod,
			"token_prefix", token[:min(len(token), 10)]+"...")

		return handler(ctx, req)
	}
}
