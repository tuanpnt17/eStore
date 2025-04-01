# Base image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore dependencies
COPY ["eStore/eStore.csproj", "eStore/"]
COPY ["BusinessObject/BusinessObject.csproj", "BusinessObject/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "eStore/eStore.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "eStore/eStore.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80

# Expose port 80
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "eStore.dll"]