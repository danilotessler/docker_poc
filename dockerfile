#Base image with Chrome
#-----------------------
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS baserelease

#BUILD
#------
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder

WORKDIR /app

COPY ./ /app/

RUN dotnet restore
RUN dotnet publish --configuration Release --output /app/release/

#RUN
#------
FROM baserelease AS release

#Copy the test app
WORKDIR /app

COPY --from=builder /app/release/ /app/

EXPOSE 5002
ENV DOTNET_URLS=http://+:5002

ENTRYPOINT ["dotnet", "docker_poc.dll"]