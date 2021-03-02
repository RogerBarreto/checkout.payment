#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Checkout.Payment.Gateway/Checkout.Payment.Gateway.csproj", "./Checkout.Payment.Gateway/"]
COPY ["./Checkout.Payment.Gateway.Application/Checkout.Payment.Gateway.Application.csproj", "./Checkout.Payment.Gateway.Application/"]
COPY ["./Checkout.Payment.Gateway.Domain/Checkout.Payment.Gateway.Domain.csproj", "./Checkout.Payment.Gateway.Domain/"]
COPY ["./Checkout.Payment.Gateway.MicroServices/Checkout.Payment.Gateway.MicroServices.csproj", "./Checkout.Payment.Gateway.MicroServices/"]
COPY ["./Checkout.Payment.Gateway.Seedwork/Checkout.Payment.Gateway.Seedwork.csproj", "./Checkout.Payment.Gateway.Seedwork/"]
RUN dotnet restore "./Checkout.Payment.Gateway/Checkout.Payment.Gateway.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Checkout.Payment.Gateway/Checkout.Payment.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Checkout.Payment.Gateway/Checkout.Payment.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Checkout.Payment.Gateway.dll"]