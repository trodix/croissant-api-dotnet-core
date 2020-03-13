# Croissant API

> This project is a Resful API written in dotnet core 3.1

- See [here](https://trello.com/b/gMKz9W66/croissant-api) for Trello

## Requirements

- dotnet core 3.1 sdk
- postgresql

## Setup

- `dotnet watch run` for hot reload

### Docker

- `docker build -t croissant-api .`
- `docker-compose up`

Go to <https://localhost:5001/swagger> for API documentation

Go to <https://localhost:5001/api> for API calls

## Database

- Create a migration `dotnet ef migrations add InitialCreate`
- Migrate `dotnet ef database update`

For more info, see the [documentation](https://docs.microsoft.com/fr-fr/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
