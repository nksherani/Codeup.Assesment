#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Codeup.Assesment.API/Codeup.Assesment.API.csproj", "Codeup.Assesment.API/"]
RUN dotnet restore "Codeup.Assesment.API/Codeup.Assesment.API.csproj"
COPY . .
WORKDIR "/src/Codeup.Assesment.API"
RUN dotnet build "Codeup.Assesment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Codeup.Assesment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Codeup.Assesment.API.dll"]