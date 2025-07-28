package main

import (
	"fmt"

	"github.com/jackc/pgx/v5/pgtype"
	"github.com/valeravorobjev/lemming-trip/internal/sqlc/db"
)

func main() {
	fmt.Println("Hello")

	a := db.Author{ID: 1, Name: "Hello", Bio: pgtype.Text{String: "Hello", Valid: true}}

	fmt.Printf("Id = %d, Name = %s\n", a.ID, a.Name)
}
