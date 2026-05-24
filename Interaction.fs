module Interaction

open Config

let update state =
    
    match state.scene with
    | StartMenu -> StartMenu_Interaction.update state
    | Game -> Game_Interaction.update state
    | GamePause -> GamePause_Interaction.update state
    | GameOver -> ReturnMenu_Interaction.update state