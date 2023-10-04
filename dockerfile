#Base image with Chrome
#-----------------------
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS baserelease

EXPOSE 5002
ENV DOTNET_URLS=http://+:5002
