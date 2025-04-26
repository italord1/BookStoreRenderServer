# Use the official .NET SDK image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file and restore the dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the code and publish the app
COPY . ./
RUN dotnet publish -c Release -o out

# Set the entry point to start the app
ENTRYPOINT ["dotnet", "out/BookStoreProg.dll"]
