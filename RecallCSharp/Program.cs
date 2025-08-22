using System.Data.Common;

namespace RecallCSharp;

class Program {
    static void Main() {
        var db = new DB();
        db.InitializeDatabase();
        Console.WriteLine("Enter command (search, new):");
        var op = Console.ReadLine();
        switch (op) {
            case "new":
                var entry = DB.CreateEntry();
                db.RunQuery(db.QueryBuilder("insert", entry));
                break;
            case "search":
                Console.WriteLine("Enter tags to search, separated by commas");
                var tags = Console.ReadLine()?.ToLowerInvariant().Split(",", StringSplitOptions.TrimEntries) ?? [];
                var result = db.RunQuery(db.QueryBuilder("search", tags));
                Console.WriteLine(result);
                foreach (var i in result) {
                    throw new NotImplementedException(); // Implement reading rows
                }
                break;
        }
    }
}