using Microsoft.Data.Sqlite;

namespace RecallCSharp;

public class DB {
    private readonly string _connectionString = "Data Source=test.sqlite";
    
    public string RunQuery(SqliteCommand query) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        query.Connection = connection;
        return query.ExecuteScalar().ToString();
    }

    public SqliteCommand QueryBuilder(string type, Entry queryData) {
        var query = new SqliteCommand();
        switch (type) {
            case "insert":
                query.CommandText = $"insert into notes (note, tags, date) values (@{queryData.note}, @{queryData.tags}, @{queryData.date});";
                return query;
            case "search":
                break;
        }

        throw new NotImplementedException();
    }

    public record Entry(string note, string[] tags, DateTime date);

}
