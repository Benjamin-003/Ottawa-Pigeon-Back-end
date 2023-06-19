
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy csprj Projects
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ottawa.Pigeon.Application/Ottawa.Pigeon.Application.csproj", "Ottawa.Pigeon.Application/"]
COPY ["Ottawa.Pigeon.Domain/Ottawa.Pigeon.Domain.csproj","Ottawa.Pigeon.Domain/"]
COPY ["Ottawa.Pigeon.Infrastructure/Ottawa.Pigeon.Infrastructure.csproj" ,"Ottawa.Pigeon.Infrastructure/"]
COPY ["Ottawa.Pigeon.WebAPI/Ottawa.Pigeon.WebAPI.csproj","Ottawa.Pigeon.WebAPI/"]

RUN dotnet restore "Ottawa.Pigeon.WebAPI/Ottawa.Pigeon.WebAPI.csproj"

# Copy All Files
COPY . .


WORKDIR "/src/Ottawa.Pigeon.WebAPI"
RUN dotnet build "Ottawa.Pigeon.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ottawa.Pigeon.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ottawa.Pigeon.WebAPI.dll","--environment=Staging"]