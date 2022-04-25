FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
RUN echo 'Asia/Shanghai' >/etc/timezone
WORKDIR /app
COPY . . 
ENV ASPNETCORE_ENVIRONMENT production
ENV SKYWALKING__SERVICENAME=WP.NetCore.API
ENV ASPNETCORE_HOSTINGSTARTUPASSEMBLIES=SkyAPM.Agent.AspNetCore
EXPOSE 8081 
ENTRYPOINT ["dotnet", "WP.NetCore.API.dll","-b","0.0.0.0"]