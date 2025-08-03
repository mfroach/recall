using Microsoft.Data.Sqlite;

namespace RecallCSharp;

public class DB {
    private const string _connectionString = "Data Source=test.sqlite";

    public void InitializeDatabase() {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
                CREATE TABLE IF NOT EXISTS notes (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    note TEXT NOT NULL,
                    tags TEXT,
                    date TEXT NOT NULL
                );
            ";
        command.ExecuteNonQuery();
    }
    
    public string RunQuery(SqliteCommand query) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        query.Connection = connection;
        return query.ExecuteScalar().ToString();
    }

    public SqliteCommand? QueryBuilder(string type, Entry queryData) {
        var query = new SqliteCommand();
        switch (type) {
            case "insert":
                query.CommandText = "INSERT INTO notes (note, tags, date) VALUES (@note, @tags, @date);";
                query.Parameters.AddWithValue("@note", queryData.note);
                query.Parameters.AddWithValue("@tags", string.Join(",", queryData.tags));
                query.Parameters.AddWithValue("@date", queryData.date.ToString("o"));
                return query;
            case "search":
                throw new NotImplementedException();
        }

        return null;
    }

    public record Entry(string note, string[] tags, DateTime date);

}