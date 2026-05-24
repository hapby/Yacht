module BoardUI

let draw (state) = 

    state
    |> TableUI.draw 
    |> DiceUI.draw
