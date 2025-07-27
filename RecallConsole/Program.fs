open System
open RecallLib

let editorNote =
    0 // open editor, ask for tags,

let insertArgsNote args =
    let note: string = args[0]
    let tags = args[1]
    0

[<EntryPoint>]
let main args =
    if String.IsNullOrEmpty(args[0]) then
        editorNote
    else
        insertArgsNote args
        
    0