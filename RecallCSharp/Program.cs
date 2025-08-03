using System;
using RecallCSharp;


class Program {
    static void Main() {
        var db = new DB();
        db.InitializeDatabase();
        var entry = CreateEntry();
        db.RunQuery(db.QueryBuilder("insert", entry));
    }

    private static DB.Entry CreateEntry() {
        Console.WriteLine("Enter note:");
        var note = Console.ReadLine();
        Console.WriteLine("Enter tags, comma separated:");
        string[] tags = Console.ReadLine().Trim().ToLowerInvariant().Split(",");
        var date = DateTime.Now;
        return new DB.Entry(note, tags, date);
    }
}