# ===== Build stage =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Если есть solution:
COPY *.sln ./

# Скопируй csproj (папку поправь под свой проект)
COPY GymTrackerProject/*.csproj GymTrackerProject/
RUN dotnet restore

# Скопировать всё и собрать
COPY . .
WORKDIR /src/GymTrackerProject
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ===== Runtime stage =====
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# В контейнерах ASP.NET обычно слушает 8080 (и Koyeb это удобно) :contentReference[oaicite:1]{index=1}
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GymTrackerProject.dll"]
