FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NERDMVC/NERDMVC.csproj", "NERDMVC/"]
RUN dotnet restore "NERDMVC/NERDMVC.csproj"
COPY . .
WORKDIR "/src/NERDMVC"
RUN dotnet build "NERDMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NERDMVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NERDMVC.dll"]
