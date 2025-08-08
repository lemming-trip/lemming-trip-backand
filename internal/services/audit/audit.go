package audit

import (
	"context"
	"log/slog"

	"github.com/jackc/pgx/v5/pgxpool"
	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/status"

	auditpb "github.com/valeravorobjev/lemming-trip/internal/pb/audit"
)

type Service struct {
	auditpb.UnimplementedAuditServiceServer
	logger *slog.Logger
	db     *pgxpool.Pool
}

func NewService(logger *slog.Logger, db *pgxpool.Pool) *Service {
	return &Service{
		logger: logger,
		db:     db,
	}
}

func (s *Service) CreateSecurityAuditLog(ctx context.Context, req *auditpb.CreateSecurityAuditLogRequest) (*auditpb.CreateSecurityAuditLogResponse, error) {
	s.logger.Info("creating security audit log",
		"identity_id", req.IdentityId,
		"event_type", req.EventType.String())

	if req.IdentityId == "" {
		return nil, status.Error(codes.InvalidArgument, "identity_id is required")
	}

	// TODO: Сохранить в таблицу security_audit_log
	return &auditpb.CreateSecurityAuditLogResponse{
		Event: &auditpb.SecurityAuditLog{
			Id:          "generated-uuid",
			IdentityId:  req.IdentityId,
			EventType:   req.EventType,
			EventStatus: req.EventStatus,
			IpAddress:   req.IpAddress,
			UserAgent:   req.UserAgent,
			Details:     req.Details,
		},
	}, nil
}

func (s *Service) ListSecurityAuditLogs(ctx context.Context, req *auditpb.ListSecurityAuditLogsRequest) (*auditpb.ListSecurityAuditLogsResponse, error) {
	s.logger.Info("listing security audit logs", "identity_id", req.IdentityId)

	// TODO: Запросить логи из базы данных с пагинацией и фильтрацией
	return &auditpb.ListSecurityAuditLogsResponse{
		SecurityAuditLogs: []*auditpb.SecurityAuditLog{},
		Total:             0,
	}, nil
}

func (s *Service) GetSecurityAuditLog(ctx context.Context, req *auditpb.GetSecurityAuditLogRequest) (*auditpb.GetSecurityAuditLogResponse, error) {
	s.logger.Info("getting security audit log", "id", req.Id)

	if req.Id == "" {
		return nil, status.Error(codes.InvalidArgument, "audit log id is required")
	}

	// TODO: Запросить конкретный лог из базы данных
	return &auditpb.GetSecurityAuditLogResponse{
		Event: &auditpb.SecurityAuditLog{
			Id: req.Id,
		},
	}, nil
}

func (s *Service) DeleteSecurityAuditLog(ctx context.Context, req *auditpb.DeleteSecurityAuditLogRequest) (*auditpb.DeleteSecurityAuditLogResponse, error) {
	s.logger.Info("deleting security audit log", "id", req.Id)

	// TODO: Удалить лог из базы данных
	return &auditpb.DeleteSecurityAuditLogResponse{
		Success: true,
	}, nil
}

func (s *Service) ExportSecurityAuditLogs(ctx context.Context, req *auditpb.ExportSecurityAuditLogsRequest) (*auditpb.ExportSecurityAuditLogsResponse, error) {
	s.logger.Info("exporting security audit logs", "format", req.Format)

	// TODO: Реализовать экспорт в различных форматах
	return &auditpb.ExportSecurityAuditLogsResponse{
		File:     []byte("exported data in " + req.Format),
		Filename: "security_audit_logs." + req.Format,
	}, nil
}
