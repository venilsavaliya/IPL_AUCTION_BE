# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY IPL_AUCTION.sln ./
COPY IplAuction.Api/*.csproj ./IplAuction.Api/
COPY IplAuction.Service/*.csproj ./IplAuction.Service/
COPY IplAuction.Repository/*.csproj ./IplAuction.Repository/
COPY IplAuction.Entities/*.csproj ./IplAuction.Entities/

RUN dotnet restore IPL_AUCTION_BE.sln

# Copy the rest of the code
COPY . .

WORKDIR /src/IplAuction.Api
RUN dotnet publish -c Release -o /app

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .

# Run the API
ENTRYPOINT ["dotnet", "IplAuction.Api.dll"]
