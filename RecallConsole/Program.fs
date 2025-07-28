open System
open System.IO
open RecallLib

let editorNote () =
    printfn "Editor notes are not implemented. Launch with an argument."
    failwith "NotImplemented"
    // I think for now I will just have this open edit.exe or nano in linux
    // getting env vars in windows is probably a pain

let insertArgsNote args =
    let note: string = Array.get args 0
    let tags: string = Array.get args 1
    let date: DateTime = System.DateTime.Now
    let entry  = [| note, tags, date.ToString() |]
    File.AppendAllText("testPile.txt", $"note: {note}),({tags}),({date})\n")
    File.AppendAllLines("testPile.txt", entry)
    0

[<EntryPoint>]
let main args =
    if Array.isEmpty(args) then
        editorNote ()
    else
        insertArgsNote args