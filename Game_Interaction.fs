module Game_Interaction

open Config


let update (state : State) = 

    state
    |> Table_Interaction.update
    |> Dice_Interaction.update
    |> Shake.update
    