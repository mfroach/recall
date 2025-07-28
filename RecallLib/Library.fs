namespace RecallLib

module Library =
    open System
    open System.IO
    open FSharp.Data

    type Entry = CsvProvider<Schema="NOTE (string), TAGS, DATE (date)", HasHeaders=false>

    let editorNote () =
        printfn "Editor notes are not implemented. Launch with two arguments."
        failwith "NotImplemented"
    // I think for now I will just have this open edit.exe or nano in linux
    // getting env vars in windows is probably a pain

    let createEntry args =
        let note: string = Array.get args 0
        let tags: string = Array.get args 1
        let date: DateTime = System.DateTime.Now
        new Entry([ Entry.Row(note, tags, date) ])


    let insertEntry (entry:Entry) =
        let csv = Entry.Load("testCSV.csv").Append(entry.Rows) // csvprovider fails to load csv if no rows, create with headers fucks up csv.Save...
        csv.Save("testCSV.csv")