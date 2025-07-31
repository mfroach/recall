namespace RecallLib

module Library =
    open System
    open System.Data
    open System.Data.SQLite
    open DbFun.Core
    open DbFun.Core.Builders
    open DbFun.Core.Sqlite

    let createConnection (): IDbConnection = new SQLiteConnection(@"test.sqlite")
    let defaultConfig: QueryConfig<unit> = QueryConfig.Default(createConnection)
    let query = QueryBuilder(defaultConfig)
    let run f = DbCall.Run(createConnection, f)

    type Entry = {
        note: string
        tags: string array
        date: DateTime
    }

    let editorNote () =
        printfn "Editor notes are not implemented. Launch with two arguments."
        failwith "NotImplemented"
    // I think for now I will just have this open edit.exe or nano in linux
    // getting env vars in windows is probably a pain

    let createEntry args =
        let note: string = Array.get args 0
        let tags: string = Array.get args 1
        let date: DateTime = System.DateTime.Now
        1


    let insertEntry (entry) =
        query.Sql<int, Entry>(
            "insert into notes
            (note, tags, date)
            values (@note, @tags, @date);"
        )