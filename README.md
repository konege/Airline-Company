# Airline Company Project

## Description
The Airline Company project is a web API developed using .NET, designed to manage airline operations. The API interfaces with a  database and adheres to a layered architecture, separating data access, business logic, and entity layers for clarity and maintainability.

## Architecture
- **Data Layer**: Implemented using Entity Framework for efficient database interactions. Includes a generalized abstraction for basic CRUD operations applicable across all repositories.
- **Business Layer**: Contains all the business logic for the application, ensuring that the API's operations conform to business rules and logic.
- **Entity Layer**: Defines the data models used across the application.

Although each layer is conceptually distinct, they are organized into separate folders within the same project for simplicity, given the project's manageable size.

## Installation
1. Clone the repository to your local machine.
2. Ensure that .NET SDK is installed.
3. Set up a SQL Server database and configure the connection string in `appsettings.json`.
4. Run the project using your IDE or the .NET CLI.

## Usage
The API is designed to be tested and interacted with using Postman. 
- To start, run the application.
- Use Postman to send requests to the API endpoints for various operations like creating, reading, updating, and deleting airline data.

## Contributing
Contributions are welcome. Please adhere to the following guidelines:
1. Fork the repository and create your branch from `master`.
2. Write clear and concise commit messages.
3. Ensure any install or build dependencies are removed before the end of the layer when doing a build.
4. Submit a pull request.

## Acknowledgments
- Special thanks to all contributors and users of this project.

