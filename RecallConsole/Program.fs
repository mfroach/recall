open RecallLib

[<EntryPoint>]
let main args =
    if Array.isEmpty(args) then
        Library.editorNote ()
        0
    else
        let workflow = async {
            let entry = Library.createEntry args
            let! result = Library.insertEntry entry |> Library.run
            return 0
        }

        Async.RunSynchronously workflow