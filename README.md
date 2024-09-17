# Gauchada Travel App

**Gauchada** is an end-to-end application designed to connect passengers with private car drivers for ride-sharing. It follows a traditional three-layer architecture, utilizing Angular for the user interface, .NET with C# in the service layer, and SQL Server for data persistence.

The app uses Entity Framework as ORM, its secured via JWT (JSON Web Tokens) using Bearer Tokens and xUnit for api Tests.
User images are stored in a dedicated folder on the server, and their paths are saved in the database.
Each Trip contains a dedicated real time chat using SignalR to coordinate the Trip.
## Class Diagram

![ClassDiagram (1)](https://github.com/user-attachments/assets/49055b3a-1cae-4e65-a3b4-dd474fc93431)


## Use Cases
### Driver
  - Add Car
  - Create a Trip
  - View created Trips
  - View added Cars
  - Chat in a created Trip

### Passenger
  - Search a Trip between two cities
  - Sign in a Trip
  - View signed Trips
  - Chat in a signed Trip

## A picture of the App User Interface

![Captura de pantalla 2024-09-17 200629](https://github.com/user-attachments/assets/be2ed04a-1f84-49ab-a97a-2c2a91eed4ee)
