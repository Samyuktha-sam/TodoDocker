# ./TodoApi/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /App/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /App/out .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "TodoApi.dll"]


#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
#WORKDIR /App
#
## Copy everything
#COPY . ./
## Restore as distinct layers
#RUN dotnet restore
## Build and publish a release
#RUN dotnet publish -c Release -o out
#
## Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /App
#COPY --from=build-env /App/*.db .
#COPY --from=build-env /App/out .
#ENTRYPOINT ["dotnet", "TodoApi.dll"]