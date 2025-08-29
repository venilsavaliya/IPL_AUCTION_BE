# ================================
# STEP 1: Build the application
# ================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers (for faster builds)
COPY *.sln .
COPY IplAuction.Api/*.csproj ./IplAuction.Api/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src/IplAuction.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


# ================================
# STEP 2: Run the application
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 8080 for Render
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "IplAuction.Api.dll"]
