#nowarn "3391"
module ReturnMenu_Interaction

open Config
open UI_func
open Utils
open Raylib_cs

let ReturnButton_IC (state) =

    let mousePos =
        Raylib.GetMousePosition()

    let isHover =
        Raylib.CheckCollisionPointRec(
            mousePos,
            UnBox state.ReturnMenuButton
        )

    let isClicked =
        isHover && Raylib.IsMouseButtonPressed(MouseButton.Left)
    
    if isClicked then 
        init ()
    else state

let update (state : State) = 
    
    state
    |> ReturnButton_IC