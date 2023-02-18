# FROM mcr.microsoft.com/mssql/server:2022-latest AS build
# ENV ACCEPT_EULA=Y
# ENV MSSQL_SA_PASSWORD=Password123!


# WORKDIR /tmp
# COPY RTC.bak . 
# COPY RestoreDB.sql .

# RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 20  &&\
#     /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Password123!" -i /tmp/RestoreDB.sql &&\
#     pkill sqlservr

# FROM mcr.microsoft.com/mssql/server:2022-latest AS release
# ENV ACCEPT_EULA=Y

# COPY --from=build /var/opt/mssql/data /var/opt/mssql/data

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS buildApi
WORKDIR /source

COPY ./*.sln .
COPY ./API/*.csproj                                         ./API/
COPY ./RealTimeChat.API.DataAccess/*.csproj                 ./RealTimeChat.API.DataAccess/
COPY ./RealTimeChat.API.Helpers/*.csproj                    ./RealTimeChat.API.Helpers/
COPY ./RealTimeChat.BusinessLogic.AccountLogic/*.csproj     ./RealTimeChat.BusinessLogic.AccountLogic/
COPY ./RealTimeChat.BussinesLogic/*csproj                   ./RealTimeChat.BussinesLogic/
RUN dotnet restore 


COPY ./API/.                                                ./API/
COPY ./RealTimeChat.API.DataAccess/.                        ./RealTimeChat.API.DataAccess/
COPY ./RealTimeChat.API.Helpers/.                           ./RealTimeChat.API.Helpers/
COPY ./RealTimeChat.BusinessLogic.AccountLogic/.            ./RealTimeChat.BusinessLogic.AccountLogic/
COPY ./RealTimeChat.BussinesLogic/.                         ./RealTimeChat.BussinesLogic/
RUN dotnet publish -c Release -o /app 

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=buildApi /app ./
ENTRYPOINT ["dotnet", "RealTimeChat.API.dll"]