# build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only csproj files and restore dependencies first
COPY MiniShop.Api/*.csproj MiniShop.Api/
COPY MiniShop.Infrastructure/*.csproj MiniShop.Infrastructure/
COPY MiniShop.Application/*.csproj MiniShop.Application/
COPY MiniShop.Domain/*.csproj MiniShop.Domain/

# get all package 
RUN dotnet restore MiniShop.Api/MiniShop.Api.csproj

COPY . .

# create publish (folder) and add dll &..
RUN dotnet publish MiniShop.Api/MiniShop.Api.csproj -c Release -o /app/publish


# runtimestage
# uses light version of dotnet(dotnet/aspnet compare to dotnet/sdk)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

#copy files in publish fron buildstage to this stage 
COPY --from=build /app/publish .

#when container run , this command execut
ENTRYPOINT ["dotnet", "MiniShop.Api.dll"]