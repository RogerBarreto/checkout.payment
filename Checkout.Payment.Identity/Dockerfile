#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Checkout.Payment.Identity/Checkout.Payment.Identity.csproj", "Checkout.Payment.Identity/"]
COPY ["Checkout.Payment.Identity.Data/Checkout.Payment.Identity.Data.csproj", "Checkout.Payment.Identity.Data/"]
COPY ["Checkout.Payment.Identity.Domain/Checkout.Payment.Identity.Domain.csproj", "Checkout.Payment.Identity.Domain/"]
RUN dotnet restore "Checkout.Payment.Identity/Checkout.Payment.Identity.csproj"
COPY . .
WORKDIR "/src/Checkout.Payment.Identity"
RUN dotnet build "Checkout.Payment.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Checkout.Payment.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Checkout.Payment.Identity.dll"]