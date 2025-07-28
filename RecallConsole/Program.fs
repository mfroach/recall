open System
open System.IO
open RecallLib

[<EntryPoint>]
let main args =
    if not(File.Exists("testCSV.csv")) then
        File.WriteAllText("testCSV.csv", "NOTE, TAGS, DATE")
        ()
    if Array.isEmpty(args) then
        RecallLib.Library.editorNote ()
        1
    else
        let entry = RecallLib.Library.createEntry args
        RecallLib.Library.insertEntry entry
        0