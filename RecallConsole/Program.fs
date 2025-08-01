open System
open System.IO
open RecallLib

[<EntryPoint>]
let main args =
    if not(File.Exists("testCSV.csv")) then
        File.WriteAllText("testCSV.csv", "NOTE, TAGS, DATE")
        ()
    if Array.isEmpty(args) then
        Library.editorNote ()
        1
    else
        async {
            let entry = Library.createEntry args
            Library.run Library.insertEntry
        }
        0