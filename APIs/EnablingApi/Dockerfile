FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY EnablingApi.csproj .
RUN dotnet restore 
COPY . .
RUN dotnet publish -c release -o /app

# layer the asp.net sdk on top of the current image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "EnablingApi.dll" ]