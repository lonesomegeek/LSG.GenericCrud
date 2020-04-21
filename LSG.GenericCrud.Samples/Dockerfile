# # Stage 1
# FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
# WORKDIR /build
# COPY . .
# RUN dotnet restore
# RUN dotnet publish -c Release -o /app
# manual steps
# dotnet build -c Release
# dotnet publish -c Release -o app
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
# COPY --from=build /app .
COPY /app .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet LSG.GenericCrud.Samples.dll
# ENTRYPOINT ["dotnet", "LSG.GenericCrud.Samples.dll"]