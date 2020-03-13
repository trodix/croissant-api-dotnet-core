FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY . /app
RUN dotnet restore
RUN dotnet ef database update
EXPOSE 5001
CMD ["dotnet", "run"]