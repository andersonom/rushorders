FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 53835
EXPOSE 44309

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY RushOrders/RushOrders.csproj RushOrders/
RUN dotnet restore RushOrders/RushOrders.csproj
COPY . .
WORKDIR /src/RushOrders
RUN dotnet build RushOrders.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RushOrders.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RushOrders.dll"]
