#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore ./S3GraphQLAndMSA.csproj
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "/app/S3GraphQLAndMSA.dll"]