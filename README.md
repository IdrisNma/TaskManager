# TaskManager

A comprehensive .NET 8 web application for managing staff daily tasks with ASP.NET Core Identity for user management.

## Features

### User Management
- User registration and authentication
- ASP.NET Core Identity integration
- Secure login/logout functionality
- User profile management

### Task Management
- Create, read, update, and delete tasks
- Assign tasks to users
- Set task priorities (Low, Medium, High, Critical)
- Track task status (Pending, In Progress, Completed, Cancelled)
- Due date management with overdue indicators
- Task notes and descriptions
- Complete tasks with timestamp tracking

### User Interface
- Responsive Bootstrap-based design
- Intuitive navigation
- Color-coded priority and status indicators
- Clean, professional task list and detail views
- Form validation and user feedback

## Technology Stack

- **Framework**: .NET 8.0
- **Web Framework**: ASP.NET Core with Razor Pages
- **Authentication**: ASP.NET Core Identity
- **Database**: Entity Framework Core with SQLite
- **Frontend**: Bootstrap 5, Font Awesome icons
- **Development**: Visual Studio Code compatible

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- A code editor (Visual Studio, VS Code, etc.)

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd TaskManager
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

### First Time Setup

1. Register a new user account
2. Login with your credentials
3. Start creating and managing tasks

## Database

The application uses SQLite by default with Entity Framework Core. The database file (`app.db`) will be created automatically when you run the application for the first time.

### Models

- **TaskItem**: Main task entity with title, description, due date, priority, status, assignment, and tracking fields
- **IdentityUser**: Built-in ASP.NET Core Identity user model

## Usage

### Creating Tasks
1. Navigate to "My Tasks" → "Add New Task"
2. Fill in the task details (title, description, due date, priority)
3. Assign the task to a user
4. Save the task

### Managing Tasks
- **View**: See detailed task information
- **Edit**: Modify task details and status
- **Complete**: Mark tasks as completed
- **Track**: Monitor due dates and priorities

### Task Priorities
- **Low**: Green badge
- **Medium**: Yellow badge  
- **High**: Red badge
- **Critical**: Dark badge

### Task Statuses
- **Pending**: Gray badge
- **In Progress**: Blue badge
- **Completed**: Green badge
- **Cancelled**: Red badge

## Development

### Project Structure
```
TaskManager/
├── Areas/Identity/          # Identity pages
├── Data/                    # Database context and migrations
├── Models/                  # Data models
├── Pages/                   # Razor pages
│   ├── Tasks/              # Task management pages
│   └── Shared/             # Shared layouts and partials
├── wwwroot/                # Static files
└── Program.cs              # Application startup
```

### Adding Features
The application is built with extensibility in mind. You can easily add new features by:
1. Creating new models in the `Models` folder
2. Adding new pages in the `Pages` folder
3. Updating the database context in `Data/ApplicationDbContext.cs`
4. Creating and applying new migrations

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## License

This project is licensed under the MIT License.