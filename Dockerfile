# ===== Build stage =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копируем solution и csproj из КОРНЯ
COPY GymTrackerProject.sln ./
COPY GymTrackerProject.csproj ./

# Восстанавливаем зависимости
RUN dotnet restore GymTrackerProject.sln

# Копируем остальной код
COPY . ./

# Публикуем проект (csproj в корне)
RUN dotnet publish GymTrackerProject.csproj -c Release -o /app/publish /p:UseAppHost=false

# ===== Runtime stage =====
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Koyeb удобно слушать на 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

COPY --from=build /app/publish ./

ENTRYPOINT ["dotnet", "GymTrackerProject.dll"]
