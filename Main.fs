#nowarn "3391"

open Raylib_cs
open Config
open Utils

[<EntryPoint>]
let main argv =

    let mutable state = init ()

    Raylib.InitWindow(state.windowSize.W, state.windowSize.H, "Yacht")

    Raylib.SetTargetFPS(60)

    while Raylib.WindowShouldClose() = false do

        Raylib.BeginDrawing()

        state
        |> Drawing.draw
        |> Interaction.update
        |> fun st -> state <- st

        Raylib.EndDrawing()

    Raylib.CloseWindow()

    0