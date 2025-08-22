using System.Data;
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

    public List<Dictionary<string, object>> RunQuery(SqliteCommand query) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        query.Connection = connection;
        using var result = query.ExecuteReader();
    
        var data = new List<Dictionary<string, object>>();
        while (result.Read()) {
            var row = new Dictionary<string, object>();
            for (int i = 0; i < result.FieldCount; i++) {
                row[result.GetName(i)] = result.GetValue(i);
            }
            data.Add(row);
        }
        return data;
    }

    public SqliteCommand QueryBuilder(string type, Entry entry) {
        var query = new SqliteCommand();
        if (type == "insert") {
            query.CommandText = "INSERT INTO notes (note, tags, date) VALUES (@note, @tags, @date);";
            query.Parameters.AddWithValue("@note", entry.note);
            query.Parameters.AddWithValue("@tags", string.Join("%", entry.tags));
            query.Parameters.AddWithValue("@date", entry.date.ToString("o"));
        }

        return query;
    }

    public SqliteCommand QueryBuilder(string type, string[] tags) {
        var query = new SqliteCommand();
        if (type == "search") {
            query.CommandText = "SELECT * FROM notes WHERE tags LIKE @tags";
            query.Parameters.AddWithValue("@tags", "%" + string.Join(",", tags) + "%");
        }

        return query;
    }

    public static Entry CreateEntry() {
        Console.WriteLine("Enter note:");
        var note = Console.ReadLine();
        if (!string.IsNullOrEmpty(note)) {
            Console.WriteLine("Enter tags, comma separated:");
            string[] tags = Console.ReadLine()?.ToLowerInvariant().Split(",", StringSplitOptions.TrimEntries) ?? [];
            var date = DateTime.Now;
            return new Entry(note, tags, date);
        } else throw new NullReferenceException("You didn't enter anything.");
    }

    public record Entry(string note, string[] tags, DateTime date);
}