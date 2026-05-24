module DiceUI

open Config
open UI_func
open Utils
open Raylib_cs


let ShakePos (state : State) = 
    let rnd = System.Random()
    let range min max = min + (float32 (rnd.NextDouble()) * (max - min))
    let ease = 1f - state.AnimElapsed / (state.AnimTime + 0.0001f)
    (state.ShakePower * ease * range 0f 1f, state.ShakePower * ease * range 0f 1f)

let DrawDice (idx : int) (StartPos : Pos) (Dice_Init : DiceConfig) (state : State) = 

    let dice_v = List.item (idx - 1) state.dice_pattern

    let IsSelected = GetDiceSelected idx state

    let shake_vec = if IsSelected then (0f, 0f) else ShakePos state

    let dice_pos = { X = StartPos.X + (idx - 1) * (Dice_Init.DiceSize + Dice_Init.Dice_Space_Dice) + int (fst shake_vec) ; Y = StartPos.Y + int (snd shake_vec)}

    let DiceRect = {
        txt = { 
            txt = string dice_v
            font = Dice_Init.DiceSize * 2 / 3
            pos = dice_pos
            color = Color.Black
        }
        size = { W = Dice_Init.DiceSize ; H = Dice_Init.DiceSize }
        pos = dice_pos
        color = 
            if IsSelected then Color.Red 
            elif state.IsAnimating then Color.Blue
            else Color.Black
    }
    DrawThickRect(DiceRect)

    AddDice idx state (Some (DiceRect.rect))

let MISC (pos : Pos) = 

    let HelpText = { 
        txt = "Press Enter to See the Result"
        font = 40
        pos = pos
        color = Color.DarkBlue
    }
    DrawText(HelpText)

let draw (state : State) = 

    let Dice_Init = DiceConfig 100
    let Tabel_Init = state.Tabel_Init

    let TabelOccupyingX = Tabel_Init.StartPos.X + Tabel_Init.CatEntry_Width / 2 + Tabel_Init.Entry_Width * state.Nplayers
    let midX = (state.windowSize.W - TabelOccupyingX) / 2 + TabelOccupyingX
    let midY = state.windowSize.H / 2

    let StartX = midX - 2 * (Dice_Init.DiceSize + Dice_Init.Dice_Space_Dice)
    let StartY = midY
    let StartPos = {X = StartX ; Y = StartY}

    let HelpPos = {X = midX ; Y = state.windowSize.H - 150}
    if state.scene = GamePause then MISC HelpPos

    let GuagePos = {X = midX ; Y = 150}
    GuageBar.draw GuagePos state

    let lst = List.init 5 (fun x -> x + 1)
    state 
    |> fun state -> List.fold (fun state e -> DrawDice e  StartPos Dice_Init state) state lst
    
