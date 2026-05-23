module StartMenu
open Util
open Util_Funcs
open Raylib_cs

let draw_title (state) = 

    let Title = {
        txt = "YACHT GAME"
        font = 50
        pos = { X = state.windowSize.W / 2; Y = state.windowSize.H / 4}
        color = Color.White
    }

    DrawText(Title)

    state

let draw_NPButton (state) = 

    let spacing = state.windowSize.W / 25

    let text_size = 40;

    let Nplayer_text = {
        txt = $"{state.Nplayers}P"
        font = text_size
        pos = { X = state.windowSize.W / 2; Y = state.windowSize.H * 9 / 16}
        color = Color.Black
    }

    let PM_ButtonSize = { W = text_size * 5 / 4; H = text_size * 5 / 4 }
    let M_ButtonPos = { X = Nplayer_text.X - Nplayer_text.width / 2 - spacing - PM_ButtonSize.W / 2; Y = Nplayer_text.Y}
    let MinusButton = {
        txt = {
            txt = "-"
            font = text_size
            pos = M_ButtonPos
            color = Color.Black
        }
        size = PM_ButtonSize
        pos = M_ButtonPos
        color = Color.SkyBlue
    }

    let P_ButtonPos = { X = Nplayer_text.X + Nplayer_text.width / 2 + spacing + PM_ButtonSize.W / 2; Y = Nplayer_text.Y}
    let PlusButton = {
        txt = {
            txt = "+"
            font = text_size
            pos = P_ButtonPos
            color = Color.Black
        }
        size = PM_ButtonSize
        pos = P_ButtonPos
        color = Color.SkyBlue
    }

    DrawText (Nplayer_text)
    DrawButton (MinusButton)
    DrawButton (PlusButton)

    { state with PlusButton = Some PlusButton.rect; MinusButton = Some MinusButton.rect}

let draw_StartButton (state) = 

    let StartButton = {
        txt = {
            txt = "Start"
            font = 30
            pos = { X = state.windowSize.W / 2; Y = state.windowSize.H * 23 / 32}
            color = Color.Black
        }
        size = { W = 200 ; H = 60 }
        pos = { X = state.windowSize.W / 2; Y = state.windowSize.H * 23 / 32}
        color = Color.SkyBlue
    }

    DrawButton (StartButton)

    { state with StartButton = Some StartButton.rect }

let draw (state) =

    Raylib.ClearBackground(Color.DarkBlue)

    state
    |> draw_title
    |> draw_NPButton
    |> draw_StartButton

