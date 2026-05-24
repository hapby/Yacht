#nowarn "3391"
module Shake

open Config
open UI_func
open Utils
open Raylib_cs


let RollDices (state : State) = 
    let rnd = System.Random()

    let roll stat e = 
        if GetDiceSelected e state then (List.item (e - 1) state.dice_pattern) :: stat
        else rnd.Next(1, 7) :: stat

    let dice_lst = List.init 5 (fun x -> x + 1) |> List.fold  roll [] |> List.rev
    {state with dice_pattern = dice_lst}

let AnimateRoll (state : State) = 
    if state.AnimElapsed < state.AnimTime then  
        let deltaTime = Raylib.GetFrameTime()
        let rollFrequency = 5f + state.ShakePower

        state.AnimElapsed <- state.AnimElapsed + deltaTime

        state.NextRollTime <- state.NextRollTime - deltaTime

        if state.NextRollTime <= 0f then
            state
            |> RollDices
            |> fun state -> 
                state.NextRollTime <- 1f / rollFrequency
                state
        else state
    else 
        if state.AnimTime > 0f then {state with ShakePower = 0f ; AnimTime = 0f ; AnimElapsed = 0f ; IsAnimating = false}
        else state

let PowerUp (state : State) = 
    if Raylib.IsKeyDown(KeyboardKey.Space) = true && not state.IsAnimating then
        {state with ShakePower = min 10000.0f (state.ShakePower + 0.5f)}
    else state

let ReleasePower (state : State) = 
    if Raylib.IsKeyReleased KeyboardKey.Space = true && not state.IsAnimating then
        {state with AnimTime = 0.6f ; AnimElapsed = 0f ; NextRollTime = 0f ; IsAnimating = true}
    else state

let update (state : State) =

    if state.Getting_NameInput = false then
        state
        |> PowerUp
        |> ReleasePower
        |> AnimateRoll
    else state
    