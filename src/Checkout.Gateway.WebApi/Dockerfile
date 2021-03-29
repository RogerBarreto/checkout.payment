#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Checkout.Gateway.WebApi/Checkout.Gateway.WebApi.csproj", "src/Checkout.Gateway.WebApi/"]
RUN dotnet restore "src/Checkout.Gateway.WebApi/Checkout.Gateway.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Checkout.Gateway.WebApi"
RUN dotnet build "Checkout.Gateway.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Checkout.Gateway.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Checkout.Gateway.WebApi.dll"]