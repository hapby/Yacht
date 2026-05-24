#nowarn "3391"
module GamePause_Interaction

open Config
open Raylib_cs
open Utils

let update (state : State) = 
    if Raylib.IsKeyPressed KeyboardKey.Enter = true then
        state.scene <- GameOver
        state
    else state