FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App
COPY . ./
RUN dotnet restore
RUN dotnet publish Digital.Pages.Api/Digital.Pages.Api.csproj -c Release -o release

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS digital-pages-api
WORKDIR /App
COPY --from=build-env /App/release .
ENTRYPOINT ["dotnet", "Digital.Pages.Api.dll"]