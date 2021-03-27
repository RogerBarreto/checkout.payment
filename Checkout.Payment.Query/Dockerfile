#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Checkout.Payment.Query/Checkout.Payment.Query.csproj", "Checkout.Payment.Query/"]
COPY ["Checkout.Payment.Query.Application/Checkout.Payment.Query.Application.csproj", "Checkout.Payment.Query.Application/"]
COPY ["Checkout.Payment.Query.Domain/Checkout.Payment.Query.Domain.csproj", "Checkout.Payment.Query.Domain/"]
COPY ["Checkout.Payment.Query.Seedwork/Checkout.Payment.Query.Seedwork.csproj", "Checkout.Payment.Query.Seedwork/"]
COPY ["Checkout.Payment.Query.Data/Checkout.Payment.Query.Data.csproj", "Checkout.Payment.Query.Data/"]
RUN dotnet restore "Checkout.Payment.Query/Checkout.Payment.Query.csproj"
COPY . .
WORKDIR "/src/Checkout.Payment.Query"
RUN dotnet build "Checkout.Payment.Query.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Checkout.Payment.Query.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Checkout.Payment.Query.dll"]