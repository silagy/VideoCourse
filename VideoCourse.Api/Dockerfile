FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["VideoCourse.Api/VideoCourse.Api.csproj", "VideoCourse.Api/"]
COPY ["VideoCourse.Application/VideoCourse.Application.csproj", "VideoCourse.Application/"]
COPY ["VideoCourse.Domain/VideoCourse.Domain.csproj", "VideoCourse.Domain/"]
COPY ["VideoCourse.Infrastructure/VideoCourse.Infrastructure.csproj", "VideoCourse.Infrastructure/"]
RUN dotnet restore "VideoCourse.Api/VideoCourse.Api.csproj"
COPY . .
WORKDIR "/src/VideoCourse.Api"
RUN dotnet build "VideoCourse.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VideoCourse.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideoCourse.Api.dll"]
