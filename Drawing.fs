module Drawing

open Config
open UI_func
open Raylib_cs

let draw (state) = 

    Raylib.ClearBackground(Color(166, 123, 91, 255))

    if { W = Raylib.GetScreenWidth(); H = Raylib.GetScreenHeight()} <> state.windowSize then
        Raylib.SetWindowSize(state.windowSize.W, state.windowSize.H)
        centerWindow ()

    match state.scene with 
    | StartMenu -> StartMenu.draw state
    | Game -> Board.draw state