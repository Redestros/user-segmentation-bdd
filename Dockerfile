FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/UserSegmentation.Web/UserSegmentation.Web.csproj", "src/UserSegmentation.Web/"]
COPY ["Directory.Packages.props", "src/UserSegmentation.Web/"]
COPY ["src/UserSegmentation.Application/UserSegmentation.Application.csproj", "src/UserSegmentation.Application/"]
COPY ["Directory.Packages.props", "src/UserSegmentation.Application/"]
COPY ["src/UserSegmentation.Core/UserSegmentation.Core.csproj", "src/UserSegmentation.Core/"]
COPY ["Directory.Packages.props", "src/UserSegmentation.Core/"]
COPY ["src/UserSegmentation.SharedKernel/UserSegmentation.SharedKernel.csproj", "src/UserSegmentation.SharedKernel/"]
COPY ["Directory.Packages.props", "src/UserSegmentation.SharedKernel/"]
COPY ["src/UserSegmentation.Infrastructure/UserSegmentation.Infrastructure.csproj", "src/UserSegmentation.Infrastructure/"]
COPY ["Directory.Packages.props", "src/UserSegmentation.Infrastructure/"]
RUN dotnet restore "src/UserSegmentation.Web/UserSegmentation.Web.csproj"
COPY . .
WORKDIR "/src/src/UserSegmentation.Web"
RUN dotnet build "UserSegmentation.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserSegmentation.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserSegmentation.Web.dll"]
