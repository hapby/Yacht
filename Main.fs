#nowarn "3391"

open Raylib_cs
open Util
open StartMenu
open Interaction

[<EntryPoint>]
let main argv =

    let windowSize = {W = 800; H = 600}

    let mutable state = 
        {
            Nplayers = 2
            scene = StartMenu
            windowSize = windowSize

            StartButton = None
            PlusButton = None
            MinusButton = None
        }

    Raylib.InitWindow(windowSize.W, windowSize.H, "Yacht")

    Raylib.SetTargetFPS(60)

    while Raylib.WindowShouldClose() = false do

        Raylib.BeginDrawing()

        state
        |> draw
        |> update
        |> fun st -> state <- st

        Raylib.EndDrawing()

    Raylib.CloseWindow()

    0