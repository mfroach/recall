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
        let entry = Library.createEntry args
        async {
            Library.insertEntry entry
        }
        0