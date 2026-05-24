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
            player_turn = 1
            scene = StartMenu
            windowSize = {W = 800; H = 600}

            dice_pattern = List.init 5 (fun x -> x + 1)
            ShakePower = 0f
            AnimTime = 0f
            AnimElapsed = 0f
            NextRollTime = 0f
            IsAnimating = false

            P1_Name = "P1"
            P2_Name = "P2"
            P3_Name = "P3"
            P4_Name = "P4"

            StartButton = None
            PlusButton = None
            MinusButton = None

            Tabel_Init = TableConfig 30

            ScoringTable = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, None)) |> Map.ofList 
                Players |> List.map (fun x -> (x, emptyPlayerMap)) |> Map.ofList
            
            Score = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, 0)) |> Map.ofList 
                Players |> List.map (fun x -> (x, emptyPlayerMap)) |> Map.ofList
            
            IsTableTouched = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, false)) |> Map.ofList 
                Players |> List.map (fun x -> (x, emptyPlayerMap)) |> Map.ofList

            Dices = List.init 5 id |> List.map (fun e -> (e + 1, None)) |> Map.ofList
            IsDiceSelected = List.init 5 id |> List.map (fun e -> (e + 1, false)) |> Map.ofList
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