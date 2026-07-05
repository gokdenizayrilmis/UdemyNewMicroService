FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY ["UdemyNewMicroService/UdemyNewMicroService.Catalog.Api/UdemyNewMicroService.Catalog.Api.csproj", "UdemyNewMicroService/UdemyNewMicroService.Catalog.Api/"]
COPY ["UdemyNewMicroService.Shared/UdemyNewMicroService.Shared.csproj", "UdemyNewMicroService.Shared/"]
RUN dotnet restore "UdemyNewMicroService/UdemyNewMicroService.Catalog.Api/UdemyNewMicroService.Catalog.Api.csproj"

# Copy everything else and publish
COPY . .
RUN dotnet publish "UdemyNewMicroService/UdemyNewMicroService.Catalog.Api/UdemyNewMicroService.Catalog.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "UdemyNewMicroService.Catalog.Api.dll"]
