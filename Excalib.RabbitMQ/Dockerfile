FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Excalib.RabbitMQ/Excalib.RabbitMQ.csproj", "Excalib.RabbitMQ/"]
RUN dotnet restore "Excalib.RabbitMQ/Excalib.RabbitMQ.csproj"
COPY . .
WORKDIR "/src/Excalib.RabbitMQ"
RUN dotnet build "Excalib.RabbitMQ.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Excalib.RabbitMQ.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Excalib.RabbitMQ.dll"]
