#nowarn "3391"
module Dice_Interaction

open Config
open Raylib_cs
open Utils



let Reset_DiceSelection (state : State) = 
    let lst = List.init 5 (fun x -> x + 1)
    List.iter (fun idx -> state.IsDiceSelected <- Map.add idx false state.IsDiceSelected) lst
    state

let Dice_IC (idx : int) (state) =

    let mousePos =
        Raylib.GetMousePosition()

    let isHover =
        Raylib.CheckCollisionPointRec(
            mousePos,
            UnBox (GetDice idx state)
        )

    let isClicked =
        isHover && Raylib.IsMouseButtonPressed(MouseButton.Left)
    
    if isClicked && not state.IsAnimating && state.Roll_Count > 0 then 
        SelectDice idx state
    else state

let update (state : State) = 

    let lst = List.init 5 (fun x -> x + 1)
    state 
    |> fun state -> List.fold (fun state e -> Dice_IC e state) state lst
