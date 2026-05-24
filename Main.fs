#nowarn "3391"

open Raylib_cs
open Config
open Utils

[<EntryPoint>]
let main argv =

    let mutable state = 
        {
            Nplayers = 2
            turn = 1
            scene = StartMenu
            windowSize = {W = 800; H = 600}

            dice_pattern = List.init 5 (fun x -> x + 1)

            P1_Name = "P1"
            P2_Name = "P2"
            P3_Name = "P3"
            P4_Name = "P4"

            StartButton = None
            PlusButton = None
            MinusButton = None

            ScoringTable = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, None)) |> Map.ofList 
                Players |> List.map (fun x -> (x, emptyPlayerMap)) |> Map.ofList
            
            Score = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, 0)) |> Map.ofList 
                Players |> List.map (fun x -> (x, emptyPlayerMap)) |> Map.ofList
            
            IsTableTouched = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, false)) |> Map.ofList 
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