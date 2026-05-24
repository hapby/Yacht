module GuageBar

open Config
open UI_func
open Utils
open Raylib_cs

let draw (pos : Pos) (state : State) = 
    let maxPower = 100.0f
    let currentPower = state.ShakePower

    let ratio = currentPower / maxPower
    let ease = 1f - state.AnimElapsed / (state.AnimTime + 0.0001f)

    let BG_Bar = {
        txt = {
            txt = ""
            font = 0
            pos = {X = 0; Y = 0}
            color = Color.White
        }
        size = { W = 300 ; H = 30 }
        pos = pos
        color = Color.Gray
    }
    DrawButton(BG_Bar)

    let color =
        Color(
            byte (255f * ratio),
            byte (255f * (1f - ratio)),
            0uy,
            255uy
        )

    let fill_size = int (float32 BG_Bar.W * ratio * ease)
    let Fill_Bar = {
        txt = {
            txt = ""
            font = 0
            pos = {X = 0; Y = 0}
            color = Color.White
        }
        size = { W = fill_size ; H = 30 }
        pos = { X = BG_Bar.X - (BG_Bar.W - fill_size) / 2 ; Y = BG_Bar.Y }
        color = color
    }
    DrawButton(Fill_Bar)
