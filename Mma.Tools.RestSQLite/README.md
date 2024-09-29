# SQLiteREST

SQLiteREST is a .NET tool that automatically creates a RESTful API from your SQLite database. Inspired by projects like PostgREST, SQLiteREST allows you to quickly serve your SQLite database over HTTP, making it easy to prototype applications or build simple backends.

## Features

- Automatic RESTful API generation from SQLite database
- Supports basic CRUD operations (Create, Read, Update, Delete)
- Dynamic routing based on table names
- Easy to install and use as a .NET tool
- Configurable database connection
- Swagger UI for API documentation and testing

## Installation

To install SQLiteREST, you need to have .NET 6.0 SDK or later installed on your machine. Then, you can install the tool globally using the following command:

```
dotnet tool install --global SQLiteREST
```

## Usage

To start the SQLiteREST server, navigate to the directory containing your SQLite database file and run:

```
sqliterest --database your_database.db
```

This will start the server, and you can access your API at `http://localhost:5000`.

### API Endpoints

SQLiteREST automatically generates the following endpoints for each table in your database:

- `GET /db/{table}`: Retrieve all records from the table
- `GET /db/{table}/{id}`: Retrieve a specific record by ID
- `POST /db/{table}`: Create a new record
- `PUT /db/{table}/{id}`: Update an existing record
- `DELETE /db/{table}/{id}`: Delete a record

### Query Parameters

You can use query parameters to filter, sort, and paginate your results:

- `?filter=column:operator:value`: Filter results (e.g., `?filter=age:gt:18`)
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
