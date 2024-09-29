
using Dapper;

using Microsoft.Data.Sqlite;

using System.Data;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var connectionString = args is { Length: > 0 } ? args[0] :
    "Data Source=D:\\workspaces\\playground\\ProductsApi\\ProductsApi\\products.db";

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

// Dynamic routing for all tables
app.MapGet("/db/{table}", async (HttpContext context, string table) =>
{
    using var connection = new SqliteConnection(connectionString);
    var query = $"SELECT * FROM {table}";
    var result = await connection.QueryAsync(query);
    return Results.Ok(result);
});

app.MapGet("/db/{table}/{id}", async (HttpContext context, string table, int id) =>
{
    using var connection = new SqliteConnection(connectionString);
    var query = $"SELECT * FROM {table} where id = @id";
    var result = await connection.QueryAsync(query, new { id });
    return Results.Ok(result.FirstOrDefault());
});

app.MapPost("/db/{table}", async (HttpContext context, string table, JsonDocument body) =>
{
    using var connection = new SqliteConnection(connectionString);
    var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(body);
    string columns = string.Join(", ",dict.Keys);
    var parameters = string.Join(", ",dict.Keys.Select(k => $"@{k.Trim()}"));

    var query = $"INSERT INTO {table} ({columns}) VALUES ({parameters})";
    if(connection.State != ConnectionState.Open) connection.Open();

    var dbArgs = new DynamicParameters();
    foreach (var pair in dict) dbArgs.Add(pair.Key, pair.Value);

    await connection.ExecuteAsync(query, dbArgs);
    return Results.Created($"/db/{table}/1", null);
});

app.MapPut("/db/{table}/{id}", async (HttpContext context, string table, int id, JsonDocument body) =>
{
    using var connection = new SqliteConnection(connectionString);
    var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(body);
    
    var updates = new List<string>();
    var dbArgs = new DynamicParameters();
    foreach (var pair in dict)
    {
        dbArgs.Add(pair.Key, pair.Value.ToString());
        updates.Add($"{pair.Key} = @{pair.Key}");
    }
    dbArgs.Add("id", id);
    var query = $"Update {table} Set {string.Join(", ",updates)} where id = @id";
    if (connection.State != ConnectionState.Open) connection.Open();

    
    await connection.ExecuteAsync(query, dbArgs);
    return Results.NoContent();
});

app.MapDelete("/db/{table}/{id}", async (HttpContext context, string table, int id) =>
{
    using var connection = new SqliteConnection(connectionString);
    var query = $"DELETE FROM {table} where id = @id";
    if (connection.State != ConnectionState.Open) connection.Open();


    await connection.ExecuteAsync(query, new { id});
    return Results.NoContent();
});

app.Run();