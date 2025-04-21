dotnet ef migrations add Initial \
  --project LemmingTrip.Db/LemmingTrip.Db.csproj \
  --startup-project LemmingTrip.Grpc.Services/LemmingTrip.Grpc.Services.csproj
  
  dotnet ef migrations remove \
    --project LemmingTrip.Db/LemmingTrip.Db.csproj \
    --startup-project LemmingTrip.Grpc.Services/LemmingTrip.Grpc.Services.csproj
  
  
  dotnet ef database update \
    --project LemmingTrip.Db/LemmingTrip.Db.csproj \
    --startup-project LemmingTrip.Grpc.Services/LemmingTrip.Grpc.Services.csproj