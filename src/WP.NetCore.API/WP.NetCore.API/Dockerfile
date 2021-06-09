FROM mcr.microsoft.com/dotnet/aspnet:5.0
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
RUN echo 'Asia/Shanghai' >/etc/timezone
WORKDIR /app
COPY . . 
EXPOSE 8081 
ENTRYPOINT ["dotnet", "WP.NetCore.API.dll","-b","0.0.0.0"]