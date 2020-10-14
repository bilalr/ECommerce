#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/runtime:2.2-nanoserver-1803 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1803 AS build
WORKDIR /src
COPY ["ECommerce.API.Products.Tests/ECommerce.API.Products.Tests.csproj", "ECommerce.API.Products.Tests/"]
COPY ["ECommerce.API.Products/ECommerce.API.Products.csproj", "ECommerce.API.Products/"]
RUN dotnet restore "ECommerce.API.Products.Tests/ECommerce.API.Products.Tests.csproj"
COPY . .
WORKDIR "/src/ECommerce.API.Products.Tests"
RUN dotnet build "ECommerce.API.Products.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.API.Products.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.Products.Tests.dll"]