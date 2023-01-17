#Database Stage

#Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
ENV ASPNETCORE_URLS=http://*:5000
WORKDIR /source
COPY . . 
RUN dotnet restore ./API/RealTimeChat.API.csproj --disable-parallel
RUN dotnet publish ./API/RealTimeChat.API.csproj -c release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal 
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000


ENTRYPOINT ["dotnet", "RealTimeChat.API.dll"]