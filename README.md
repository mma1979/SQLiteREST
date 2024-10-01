# SQLiteREST

SQLiteREST is a .NET tool that automatically creates a RESTful API from your SQLite database. Inspired by projects like PostgREST, SQLiteREST allows you to quickly serve your SQLite database over HTTP, making it easy to prototype applications or build simple backends.

## Features

- Automatic RESTful API generation from SQLite database
- Supports basic CRUD operations (Create, Read, Update, Delete)
- Dynamic routing based on table names
- Easy to install and use as a .NET tool
- Configurable database connection
- Swagger UI for API documentation and testing

## Prerequisites

Before you can use SQLiteREST, you need to set up your environment. Follow these steps:

### 1. Install .NET SDK

1. Visit the official .NET download page: https://dotnet.microsoft.com/download
2. Download and install the .NET 8.0 SDK or later for your operating system.
3. After installation, open a command prompt or terminal and verify the installation by running:
   ```
   dotnet --version
   ```
   This should display the version of .NET SDK you installed.

### 2. Install SQLite

1. Visit the SQLite download page: https://www.sqlite.org/download.html
2. Download the appropriate version for your operating system.
3. Follow the installation instructions for your OS.

### 3. Create a Sample SQLite Database

1. Open a command prompt or terminal.
2. Navigate to the directory where you want to create your database.
3. Run the following command to create a new SQLite database:
   ```
   sqlite3 sample.db
   ```
4. You're now in the SQLite prompt. Create a sample table:
   ```sql
   CREATE TABLE users (id INTEGER PRIMARY KEY, name TEXT, email TEXT);
   ```
5. Insert some sample data:
   ```sql
   INSERT INTO users (name, email) VALUES ('John Doe', 'john@example.com');
   INSERT INTO users (name, email) VALUES ('Jane Smith', 'jane@example.com');
   ```
6. Exit the SQLite prompt by typing `.quit` and pressing Enter.

## Installation

After setting up your environment and creating a sample database, you can install SQLiteREST:

```
dotnet tool install --global SQLiteREST
```

If you're using an older version of .NET, you might need to add the `--version` flag to specify the correct version of SQLiteREST that matches your .NET SDK version.

## Usage

To start the SQLiteREST server, navigate to the directory containing your SQLite database file and run:

```
sqliterest --database sample.db
```

This will start the server, and you can access your API at `http://localhost:5000`.

### API Endpoints

SQLiteREST automatically generates the following endpoints for each table in your database:

- `GET /db/{table}`: Retrieve all records from the table
- `GET /db/{table}/{id}`: Retrieve a specific record by ID
- `POST /db/{table}`: Create a new record
- `PUT /db/{table}/{id}`: Update an existing record
- `DELETE /db/{table}/{id}`: Delete a record

For example, with our sample database:

- `GET /db/users`: Retrieve all users
- `GET /db/users/1`: Retrieve the user with ID 1
- `POST /db/users`: Create a new user
- `PUT /db/users/1`: Update the user with ID 1
- `DELETE /db/users/1`: Delete the user with ID 1

### Query Parameters

You can use query parameters to filter, sort, and paginate your results:

- `?filter=column:operator:value`: Filter results (e.g., `?filter=name:eq:John Doe`)
- `?sort=column`: Sort results (use `-column` for descending order)
- `?limit=n`: Limit the number of returned results
- `?offset=n`: Offset the results for pagination

## Configuration

You can configure SQLiteREST using command-line options or a configuration file. Run `sqliterest --help` to see all available options.

## Security

By default, SQLiteREST does not include authentication or authorization. It's recommended to use this tool only for development or in secure environments. For production use, consider implementing proper security measures.

## Contributing

Contributions to SQLiteREST are welcome! Please feel free to submit a Pull Request.

## License

SQLiteREST is open-source software licensed under the MIT license.

## Troubleshooting

If you encounter any issues during setup or usage:

1. Ensure your .NET SDK version is compatible with SQLiteREST.
2. Check that SQLite is correctly installed and accessible from the command line.
3. Verify that your database file exists and is in the correct location.
4. Make sure you have the necessary permissions to read/write to the database file.

If problems persist, please open an issue on the GitHub repository with details about your environment and the error you're encountering.
