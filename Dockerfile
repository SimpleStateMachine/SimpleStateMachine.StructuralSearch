FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SimpleStateMachine.StructuralSearch.Action/SimpleStateMachine.StructuralSearch.Action.csproj", "SimpleStateMachine.StructuralSearch.Action/"]
RUN dotnet restore "SimpleStateMachine.StructuralSearch.Action/SimpleStateMachine.StructuralSearch.Action.csproj"
COPY . .
WORKDIR "/src/SimpleStateMachine.StructuralSearch.Action"
RUN dotnet build "SimpleStateMachine.StructuralSearch.Action.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleStateMachine.StructuralSearch.Action.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleStateMachine.StructuralSearch.Action.dll"]
