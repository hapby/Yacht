module ReturnMenuUI

open Config
open UI_func
open Utils
open Raylib_cs

let draw (state : State) = 

    let ReturnMenuRect = {
        txt = { 
            txt = "Return To Menu"
            font = 50
            pos = { X = state.windowSize.W / 2 ; Y = state.windowSize.H / 2 }
            color = Color.Black
        }
        size = { W = 500; H = 200 }
        pos = { X = state.windowSize.W / 2 ; Y = state.windowSize.H / 2 }
        color = Color.White
    }
    DrawButton(ReturnMenuRect)
    state.ReturnMenuButton <- Some ReturnMenuRect.rect

    let (Winner, WinScore) = FindWinner state
    let WinnerName = Map.find Winner state.Player_Names
    let WinnerText = {
        txt = $"Winner is {WinnerName} with score {WinScore}"
        font = 60
        pos = { X = state.windowSize.W / 2 ; Y = ReturnMenuRect.Y + ReturnMenuRect.H / 2 + 100}
        color = Color.Black
    }
    DrawText(WinnerText)

    state