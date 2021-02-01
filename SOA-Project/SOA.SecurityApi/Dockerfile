#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["SOA.SecurityApi/SOA-SecurityApi.csproj", "SOA.SecurityApi/"]
COPY ["SOA.Common/SOA.Common.csproj", "SOA.Common/"]
RUN dotnet restore "SOA.SecurityApi/SOA-SecurityApi.csproj"
COPY . .
WORKDIR "/src/SOA.SecurityApi"
RUN dotnet build "SOA-SecurityApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOA-SecurityApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOA-SecurityApi.dll"]