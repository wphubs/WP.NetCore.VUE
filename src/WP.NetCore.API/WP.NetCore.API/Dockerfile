FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["WP.NetCore.API/WP.NetCore.API.csproj", "WP.NetCore.API/"]
COPY ["WP.NetCore.Repository.EFCore/WP.NetCore.Repository.EFCore.csproj", "WP.NetCore.Repository.EFCore/"]
COPY ["WP.NetCore.Common/WP.NetCore.Common.csproj", "WP.NetCore.Common/"]
COPY ["WP.NetCore.Model/WP.NetCore.Model.csproj", "WP.NetCore.Model/"]
COPY ["WP.NetCore.Services/WP.NetCore.Services.csproj", "WP.NetCore.Services/"]
COPY ["WP.NetCore.IServices/WP.NetCore.IServices.csproj", "WP.NetCore.IServices/"]
RUN dotnet restore "WP.NetCore.API/WP.NetCore.API.csproj"
COPY . .
WORKDIR "/src/WP.NetCore.API"
RUN dotnet build "WP.NetCore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WP.NetCore.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WP.NetCore.API.dll","-b","0.0.0.0"]