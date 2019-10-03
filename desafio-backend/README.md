# Desafio T - Flight API

## Getting Started

These instructions will get you a copy of the project up and running on your local machine or build a Docker container.

### Prerequisites

```
* ASP.NET / .NET Core 2.2
* Docker (if you will run in a container)
```

## Build

```
dotnet build
```

## Run

```
dotnet run --project .\DesafioT.API\DesafioT.API.csproj
```

## Running the tests

Unit Tests
```
dotnet test
```

API Tests (with Postman)

```
https://localhost:5000/Flights/Airports
    - Returns a airport list
```

```
https://localhost:5000/Flights/AvailableFlights/BSB/AJU/2019-02-10
    - Returns flight BSB > AJU
    - Returns flight BSB > FLN > AJU
```

```
https://localhost:5000/Flights/AvailableFlights/CGH/VCP/2019-02-10
    - Returns flight CGH > VCP
    - Returns flight CGH > PMW > VCP (with 2 option companies)
```

```
https://localhost:5000/Flights/AvailableFlights/BSB/AJU/2019-02-12
    - Returns flight BSB > MCZ > AJU
```

## Deployment

Building a Docker container:

```
docker build -t desafiot/backend .
docker run --name desafiot-backend -p 5000:5000 desafiot/backend
```

## Build With

```
* ASP.NET / .NET Core 2.2
* DDD
* Newtonsoft.Json
* Swagger (with Swashbuckle)
```

## Author

* **Angelito Casagrande** - Software Engineer - www.angelito.com.br

## License

This project is licensed under the MIT License - see the LICENSE.md file for details