#nowarn "3391"
module Interaction

open Util
open Util_Funcs
open Raylib_cs

let StartButton_IC (state) =

    let mousePos =
            Raylib.GetMousePosition()

    let isHover =
        Raylib.CheckCollisionPointRec(
            mousePos,
            UnBox state.StartButton
        )

    let isClicked =
        isHover && Raylib.IsMouseButtonPressed(MouseButton.Left)
    
    if isClicked then { state with scene = Game}
    else state

let PlusButton_IC (state) = 

    let mousePos =
            Raylib.GetMousePosition()

    let isHover =
        Raylib.CheckCollisionPointRec(
            mousePos,
            UnBox state.PlusButton
        )

    let isClicked =
        isHover && Raylib.IsMouseButtonPressed(MouseButton.Left)
    
    if isClicked then { state with Nplayers = min 4 (state.Nplayers + 1)}
    else state

let MinusButton_IC (state) = 

    let mousePos =
            Raylib.GetMousePosition()

    let isHover =
        Raylib.CheckCollisionPointRec(
            mousePos,
            UnBox state.MinusButton
        )

    let isClicked =
        isHover && Raylib.IsMouseButtonPressed(MouseButton.Left)
    
    if isClicked then { state with Nplayers = max 2 (state.Nplayers - 1)}
    else state

let update (state) = 

    state
    |> StartButton_IC
    |> PlusButton_IC
    |> MinusButton_IC
    
