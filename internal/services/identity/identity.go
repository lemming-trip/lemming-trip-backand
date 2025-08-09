package identity

import (
	"context"
	"log/slog"

	"github.com/jackc/pgx/v5/pgxpool"
	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/status"

	identitypb "github.com/valeravorobjev/lemming-trip/internal/pb/identity"
)

type Service struct {
	identitypb.UnimplementedIdentityServiceServer
	logger *slog.Logger
	db     *pgxpool.Pool
}

func NewService(logger *slog.Logger, db *pgxpool.Pool) *Service {
	return &Service{
		logger: logger,
		db:     db,
	}
}

func (s *Service) RegisterLocal(ctx context.Context, req *identitypb.RegisterLocalRequest) (*identitypb.RegisterLocalResponse, error) {
	s.logger.Info("registering local identity", "email", req.Email)

	if req.Email == "" {
		return nil, status.Error(codes.InvalidArgument, "email is required")
	}

	if req.Password == "" {
		return nil, status.Error(codes.InvalidArgument, "password is required")
	}

	// TODO: Реализовать регистрацию
	// 1. Проверить уникальность email
	// 2. Захешировать пароль
	// 3. Создать пользователя в таблице users
	// 4. Создать аккаунт в таблице accounts
	// 5. Отправить код активации

	return &identitypb.RegisterLocalResponse{
		Identity: &identitypb.Identity{
			Id:    "generated-uuid",
			Email: req.Email,
		},
	}, nil
}

func (s *Service) LoginLocal(ctx context.Context, req *identitypb.LoginLocalRequest) (*identitypb.LoginResponse, error) {
	s.logger.Info("login local identity", "login", req.Login)

	if req.Login == "" {
		return nil, status.Error(codes.InvalidArgument, "email is required")
	}

	if req.Password == "" {
		return nil, status.Error(codes.InvalidArgument, "password is required")
	}

	// TODO: Реализовать логин
	// 1. Найти аккаунт по email
	// 2. Проверить пароль
	// 3. Проверить активацию
	// 4. Создать сессию
	// 5. Записать в audit log

	return &identitypb.LoginResponse{
		Identity: &identitypb.Identity{
			Id:    "user-uuid",
			Email: req.Login,
		},
		AccessToken:  "generated-access-token",
		RefreshToken: "generated-refresh-token",
		RequiresMfa:  false,
	}, nil
}

func (s *Service) GetIdentity(ctx context.Context, req *identitypb.GetIdentityRequest) (*identitypb.GetIdentityResponse, error) {
	s.logger.Info("getting identity", "id", req.IdentityId)

	if req.IdentityId == "" {
		return nil, status.Error(codes.InvalidArgument, "identity id is required")
	}

	// TODO: Запросить пользователя из базы данных
	return &identitypb.GetIdentityResponse{
		Identity: &identitypb.Identity{
			Id:    req.IdentityId,
			Email: "user@example.com",
		},
	}, nil
}

func (s *Service) ActivateAccount(ctx context.Context, req *identitypb.ActivateAccountRequest) (*identitypb.ActivateAccountResponse, error) {
	s.logger.Info("activating account", "code", req.ActivationCode)

	if req.ActivationCode == "" {
		return nil, status.Error(codes.InvalidArgument, "activation code is required")
	}

	// TODO: Проверить и активировать аккаунт
	return &identitypb.ActivateAccountResponse{
		Success: true,
	}, nil
}

func (s *Service) ChangePassword(ctx context.Context, req *identitypb.ChangePasswordRequest) (*identitypb.ChangePasswordResponse, error) {
	s.logger.Info("changing password", "identity_id", req.IdentityId)

	// TODO: Изменить пароль
	return &identitypb.ChangePasswordResponse{
		Success: true,
	}, nil
}

func (s *Service) Logout(ctx context.Context, req *identitypb.LogoutRequest) (*identitypb.LogoutResponse, error) {
	s.logger.Info("logout", "session_id", req.SessionId)

	// TODO: Инвалидировать сессию
	return &identitypb.LogoutResponse{
		Success: true,
	}, nil
}
