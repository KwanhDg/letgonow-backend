# Sử dụng image base của .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy file .csproj và restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ mã nguồn và build
COPY . ./
RUN dotnet publish -c Release -o out

# Sử dụng image runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Cấu hình port mà ứng dụng sẽ chạy
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Lệnh chạy ứng dụng
ENTRYPOINT ["dotnet", "LetGoNowApi.dll"]