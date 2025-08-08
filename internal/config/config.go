package config

import (
	"os"
	"strconv"

	"github.com/valeravorobjev/lemming-trip/internal/database"
)

type Config struct {
	GRPCAddress string
	Database    database.Config
}

func Load() (*Config, error) {
	cfg := &Config{
		GRPCAddress: getEnv("GRPC_ADDRESS", ":50051"),
		Database: database.Config{
			Host:     getEnv("DB_HOST", "localhost"),
			Port:     getEnvInt("DB_PORT", 5432),
			User:     getEnv("DB_USER", "postgres"),
			Password: getEnv("DB_PASSWORD", "postgres"),
			DBName:   getEnv("DB_NAME", "lemmingtrip"),
			SSLMode:  getEnv("DB_SSL_MODE", "disable"),
		},
	}

	return cfg, nil
}

func getEnv(key, defaultValue string) string {
	if value := os.Getenv(key); value != "" {
		return value
	}
	return defaultValue
}

func getEnvInt(key string, defaultValue int) int {
	if value := os.Getenv(key); value != "" {
		if intValue, err := strconv.Atoi(value); err == nil {
			return intValue
		}
	}
	return defaultValue
}
