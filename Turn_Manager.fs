module Turn_Manager

open Config
open Raylib_cs
open Utils

let update (state : State) = 
    state.player_turn <- state.player_turn + 1
    if state.player_turn > state.Nplayers then 
        state.player_turn <- 1
        state.turn <- state.turn + 1
        if state.turn > 12 then 
            state.turn <- 12
            state.scene <- GamePause
    state.Roll_Count <- 0
    state
    |> Dice_Interaction.Reset_DiceSelection