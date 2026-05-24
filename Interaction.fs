module Interaction

open Config

let update state =
    
    match state.scene with
    | StartMenu -> StartMenu_Interaction.update state
    | Game -> Game_Interaction.update state