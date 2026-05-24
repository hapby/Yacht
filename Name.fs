#nowarn "3391"
module Name

open Config
open Raylib_cs
open Utils

let rec GetInput (state : State) = 
    let c = Raylib.GetCharPressed()

    if c > 0 then
        state.NameInputBuffer <- state.NameInputBuffer + string (char c)
        if String.length state.NameInputBuffer > state.MAX_NAME_LENGTH then 
            state.NameInputBuffer <- state.NameInputBuffer.Substring( state.NameInputBuffer.Length - state.MAX_NAME_LENGTH, state.NameInputBuffer.Length - 1)
        GetInput state
    else state

let BackSpace (state : State) = 
    if Raylib.IsKeyPressed KeyboardKey.Backspace = true then
        if state.NameInputBuffer.Length > 0 then
            state.NameInputBuffer <- state.NameInputBuffer.Substring(0, state.NameInputBuffer.Length - 1)
            state
        else state
    else state

let Confirm (state : State) = 
    if Raylib.IsKeyPressed KeyboardKey.Enter = true then
        match state.EditingPlayer with
        | Some plr ->
            state.Player_Names <- Map.add plr state.NameInputBuffer state.Player_Names
            state.EditingPlayer <- None
            state.NameInputBuffer <- ""
            state.Getting_NameInput <- false
            state
        | None -> state
    else state

let update (state : State) = 

    if state.Getting_NameInput then
        state
        |> GetInput
        |> BackSpace
        |> Confirm
    else state