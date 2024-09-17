# Gauchada Travel App

**Gauchada** is an end-to-end application designed to connect passengers with private car drivers for ride-sharing. It follows a traditional three-layer architecture, utilizing Angular for the user interface, .NET with C# in the service layer, and SQL Server for data persistence.

The app uses Entity Framework as ORM, its secured via JWT (JSON Web Tokens) using Bearer Tokens and xUnit for api Tests.
User images are stored in a dedicated folder on the server, and their paths are saved in the database.
## Class Diagram


![GauchadaObjetosSinChat](https://github.com/user-attachments/assets/592a2806-e30a-492a-af5c-ffc81c87fb1b)


## Use Cases
### Driver
  - Add Car
  - Delete Car
  - Create a Trip
  - View created Trips
  - View added Cars

### Passenger
  - Search a Trip between two cities
  - Sign in a Trip
  - View signed Trips

