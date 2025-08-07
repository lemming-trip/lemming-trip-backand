package main

import (
	"context"
	"log"
	"net"

	"github.com/google/uuid"
	"google.golang.org/grpc"
	"google.golang.org/protobuf/types/known/timestamppb"

	modelsv1 "github.com/valeravorobjev/lemming-trip/internal/gen/models/v1"
	servicesv1 "github.com/valeravorobjev/lemming-trip/internal/gen/services/v1"
)

// Реализация сервера
type lemmingTripServer struct {
	servicesv1.UnimplementedTripServiceServer
}

// Реализация метода CreateUser
func (s *lemmingTripServer) CreateUser(ctx context.Context, req *modelsv1.CreateUserRequest) (*modelsv1.CreateUserResponse, error) {
	log.Printf("Received CreateUser request: %+v", req)

	// Здесь будет ваша бизнес-логика
	// Например, создание пользователя в базе данных

	// Create new user from request data
	user := &modelsv1.User{
		Id:          uuid.New().String(),
		Email:       req.Email,
		IsActive:    req.IsActive,
		UserRole:    req.UserRole,
		Avatar:      req.Avatar,
		Phone:       req.Phone,
		City:        req.City,
		Address:     req.Address,
		FirstName:   req.FirstName,
		LastName:    req.LastName,
		MiddleName:  req.MiddleName,
		DateBirth:   req.DateBirth,
		Description: req.Description,
		Ban:         false,
		CreatedAt:   timestamppb.Now(),
		UpdatedAt:   timestamppb.Now(),
		LastSeenAt:  timestamppb.Now(),
		PrivacySettings: &modelsv1.PrivacySettings{
			ProfileVisible: true,
			EmailVisible:   false,
		},
		Preferences: &modelsv1.UserPreferences{
			Preferences: make(map[string]string),
		},
	}

	response := &modelsv1.CreateUserResponse{
		User: user,
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
	servicesv1.RegisterTripServiceServer(server, lemmingTripSrv)

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
