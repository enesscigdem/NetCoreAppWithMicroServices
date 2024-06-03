# MicroNest

MicroNest is a robust application demonstrating the power of .NET Core with a comprehensive microservices architecture. This project leverages multiple microservices to handle various aspects of product and order management, ensuring high scalability, maintainability, and flexibility.

<div style="display: flex; flex-wrap: wrap; gap: 10px;">
    <img width="400" alt="Ekran Resmi 2024-06-04 01 51 11" src="https://github.com/enesscigdem/NetCoreAppWithMicroServices/assets/55703841/d12d13a5-4c0a-49bd-a935-39b238f6e92f">
    <img width="400" alt="Ekran Resmi 2024-06-04 01 51 31" src="https://github.com/enesscigdem/NetCoreAppWithMicroServices/assets/55703841/cdc2dbac-5ab1-40e9-8033-1182269e0e29">
    <img width="400" alt="Ekran Resmi 2024-06-04 01 51 31" src="https://github.com/enesscigdem/NetCoreAppWithMicroServices/assets/55703841/81becea6-ca71-4488-94c4-8f4f962040be">
    <img width="400" alt="Ekran Resmi 2024-06-04 01 52 24" src="https://github.com/enesscigdem/NetCoreAppWithMicroServices/assets/55703841/0257e3a0-698c-4164-8200-f7e0862cfbb3">
    <img width="400" alt="Ekran Resmi 2024-06-04 01 52 17" src="https://github.com/enesscigdem/NetCoreAppWithMicroServices/assets/55703841/81becea6-ca71-4488-94c4-8f4f962040be">
</div>

## Features

- **Product Management**: Extensive CRUD operations for product data.
- **Order Management**: Real-time tracking and management of orders.
- **API Gateway**: Facilitates secure and efficient communication between services.
- **Responsive Design**: Utilizes Tailwind CSS for a modern and responsive UI.

## Technologies Used

- **ASP.NET Core 7.0**: Backend services and API development.
- **Entity Framework Core**: Efficient data management and ORM.
- **Ocelot API Gateway**: Secure routing between microservices.
- **Tailwind CSS**: Modern, responsive design framework.
- **Node.js**: Additional backend services and utilities.
- **N-Tier Architecture**: Separates concerns across different layers, improving maintainability.

## Microservices Architecture

MicroNest is composed of the following microservices:

- **ProductService**: Handles all operations related to product data.
- **OrderService**: Manages order processing, tracking, and data.
- **API Gateway**: Manages requests between the client and microservices, providing a single entry point.

Each service is independently deployable and scalable, allowing for efficient resource management and easier updates.

## Getting Started

### Prerequisites

- .NET Core SDK
- Node.js
- Docker (optional, for containerization)

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/enesscigdem/NetCoreAppWithMicroServices.git
    cd NetCoreAppWithMicroServices
    ```

2. Restore dependencies and build the services:
    ```bash
    dotnet restore
    dotnet build
    ```

3. Run the services:
    ```bash
    dotnet run --project ProductService
    dotnet run --project OrderService
    dotnet run --project ApiGateway
    ```

### Usage

Access the API Gateway at `https://localhost:5236` to interact with the services. The Swagger UI can be used to test endpoints and view documentation.

### Health Monitoring

MicroNest includes a health check dashboard to monitor the status of each microservice. Access the dashboard at `/dashboard` to see the current status and uptime of each service.

## Contributing

Contributions are welcome! Please fork the repository and submit pull requests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgements

- Inspired by the principles of microservices architecture and modern web development practices.
- Special thanks to the developers and contributors of the libraries and frameworks used in this project.
