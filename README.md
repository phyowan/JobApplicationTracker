# Job Application Tracker API

A RESTful API built with ASP.NET Core 9.0 for tracking job applications. This application helps you manage and organize your job search process by providing endpoints to create, read, update, and delete job application records.

## 🚀 Features

- **CRUD Operations**: Create, read, update, and delete job applications
- **Status Tracking**: Track application status (Applied, Under Review, Interview, Offer, Rejected, Withdrawn)
- **RESTful API**:  REST endpoints with proper HTTP status codes
- **Swagger Documentation**:  API documentation
- **In-Memory Database**: Quick setup with Entity Framework Core InMemory provider
- **Unit Testing**: Comprehensive test suite with xUnit, Moq, and FluentAssertions
- **Input Validation**: Data validation with attributes and model state

## 🛠️ Technology Stack

- **.NET 9.0**: Latest .NET framework
- **ASP.NET Core**: Web API framework
- **Entity Framework Core**: ORM with InMemory provider
- **Swagger/OpenAPI**: API documentation
- **xUnit**: Testing framework
- **Moq**: Mocking framework for tests
- **FluentAssertions**: Fluent test assertions

## 📋 Prerequisites

Before running the application, ensure you have the following installed:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/) (for cloning the repository)

## 🏃‍♂️ Getting Started

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd JobApplicationTracker
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run the Application

#### Option A: Using .NET CLI
```bash
# Navigate to the API project
cd JobApplicationTracker.Api

# Run the application
dotnet run
```

#### Option B: Using Visual Studio
1. Open `JobApplicationTracker.sln` in Visual Studio
2. Set `JobApplicationTracker.Api` as the startup project
3. Press `F5` or click "Start"

### 5. Access the Application

Once running, the application will be available at:

- **HTTP**: http://localhost:5212
- **HTTPS**: https://localhost:7040
- **Swagger UI**: https://localhost:7040/swagger (for API documentation)

## 📖 API Endpoints

### Job Applications

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/jobapplications` | Get all job applications |
| GET | `/api/jobapplications/{id}` | Get a specific job application by ID |
| POST | `/api/jobapplications` | Create a new job application |
| PUT | `/api/jobapplications/{id}` | Update an existing job application |
| DELETE | `/api/jobapplications/{id}` | Delete a job application |

### Sample API Usage

#### Create a Job Application
```http
POST /api/jobapplications
Content-Type: application/json

{
  "company": "Microsoft",
  "position": "Software Engineer",
  "status": 1,
  "dateApplied": "2024-01-15T00:00:00Z"
}
```

#### Get All Job Applications
```http
GET /api/jobapplications
```

#### Update a Job Application
```http
PUT /api/jobapplications/1
Content-Type: application/json

{
  "company": "Microsoft",
  "position": "Senior Software Engineer",
  "status": 3,
  "dateApplied": "2024-01-15T00:00:00Z"
}
```

### Application Status Values

| Value | Status |
|-------|--------|
| 1 | Applied |
| 2 | Under Review |
| 3 | Interview |
| 4 | Offer |
| 5 | Rejected |
| 6 | Withdrawn |

## 🧪 Running Tests

The project includes a comprehensive test suite with unit tests for the API controllers.

### Run All Tests
```bash
dotnet test
```


##  Project Structure

```
JobApplicationTracker/
├── JobApplicationTracker.Api/           # Main API project
│   ├── Controllers/                     # API controllers
│   │   └── JobApplicationsController.cs
│   ├── Data/                           # Database context
│   │   └── ApplicationDbContext.cs
│   ├── DTOs/                           # Data Transfer Objects
│   │   └── JobApplicationDtos.cs
│   ├── Models/                         # Domain models
│   │   └── JobApplication.cs
│   ├── Repositories/                   # Repository pattern
│   │   ├── IJobApplicationRepository.cs
│   │   └── JobApplicationRepository.cs
│   └── Program.cs                      # Application entry point
├── JobApplicationTracker.Tests/        # Test project
│   ├── Controllers/                    # Controller tests
│   │   └── JobApplicationsControllerTests.cs
│   └── TestHelpers/                    # Test utilities
│       └── TestDataFixtures.cs
└── README.md                           # This file
```

## 🔧 Configuration

### Development Settings

The application uses the following default configuration in `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Environment Variables

You can override settings using environment variables:

- `ASPNETCORE_ENVIRONMENT`: Set to `Development`, `Staging`, or `Production`
- `ASPNETCORE_URLS`: Override the default URLs

## 🚀 Deployment

### Local Deployment

```bash
# Build for release
dotnet build --configuration Release

# Publish the application
dotnet publish --configuration Release --output ./publish

# Run the published application
dotnet ./publish/JobApplicationTracker.Api.dll
```



## 🔍 Development Tips

### Using the API with Tools

1. **Postman**: Import the OpenAPI/Swagger definition from `/swagger/v1/swagger.json`
2. **curl**: Use command line to test endpoints
3. **Browser**: Access Swagger UI for interactive testing

### Sample curl Commands





