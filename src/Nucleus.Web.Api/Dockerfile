FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Nucleus.Web.Api/Nucleus.Web.Api.csproj", "src/Nucleus.Web.Api/"]
COPY ["src/Nucleus.EntityFramework/Nucleus.EntityFramework.csproj", "src/Nucleus.EntityFramework/"]
COPY ["src/Nucleus.Core/Nucleus.Core.csproj", "src/Nucleus.Core/"]
COPY ["src/Nucleus.Application/Nucleus.Application.csproj", "src/Nucleus.Application/"]
COPY ["src/Nucleus.Utilities/Nucleus.Utilities.csproj", "src/Nucleus.Utilities/"]
COPY ["src/Nucleus.Web.Core/Nucleus.Web.Core.csproj", "src/Nucleus.Web.Core/"]
RUN dotnet restore "src/Nucleus.Web.Api/Nucleus.Web.Api.csproj"
COPY . .

WORKDIR "/src/src/Nucleus.Web.Api"
RUN dotnet build "Nucleus.Web.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Nucleus.Web.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nucleus.Web.Api.dll"]