#nowarn "3391"

open Raylib_cs
open Util
open Util_Funcs

[<EntryPoint>]
let main argv =

    let mutable state = 
        {
            Nplayers = 2
            turn = 1
            scene = StartMenu
            windowSize = {W = 800; H = 600}

            StartButton = None
            PlusButton = None
            MinusButton = None

            ScoringTable = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, None)) |> Map.ofList 
                Players |> List.map (fun x -> (x, emptyPlayerMap)) |> Map.ofList
        }

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