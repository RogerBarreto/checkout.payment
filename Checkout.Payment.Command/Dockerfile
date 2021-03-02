#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Checkout.Payment.Command/Checkout.Payment.Command.csproj", "Checkout.Payment.Command/"]
COPY ["Checkout.Payment.Command.Data/Checkout.Payment.Command.Data.csproj", "Checkout.Payment.Command.Data/"]
COPY ["Checkout.Payment.Command.Domain/Checkout.Payment.Command.Domain.csproj", "Checkout.Payment.Command.Domain/"]
COPY ["Checkout.Payment.Command.Seedwork/Checkout.Payment.Command.Seedwork.csproj", "Checkout.Payment.Command.Seedwork/"]
COPY ["Checkout.Payment.Command.Application/Checkout.Payment.Command.Application.csproj", "Checkout.Payment.Command.Application/"]
RUN dotnet restore "Checkout.Payment.Command/Checkout.Payment.Command.csproj"
COPY . .
WORKDIR "/src/Checkout.Payment.Command"
RUN dotnet build "Checkout.Payment.Command.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Checkout.Payment.Command.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Checkout.Payment.Command.dll"]