module Board

open Util
open Util_Funcs
open Raylib_cs

let draw (state) =

    let Basic_font = 30
    let Basic_TextSpacing = Basic_font * 2 / 3
    let Turn12_font = Basic_font * 5 / 3
    let Entry_Width = Turn12_font * 9 / 2
    let Subtotal_TextSize = Basic_font * 4 / 5
    let Bonus_TextSize = Basic_font * 8 / 7
    let Explanation_TextSize = Basic_font * 2 / 3
    let Total_TextSize = Bonus_TextSize

    let Turn_Space_Category = Basic_font / 2
    let Yacht_Space_Total = Basic_font / 3

    let TurnText = {
        txt = "Turn"
        font = Basic_font
        pos = { X = Entry_Width * 9 / 16 ; Y = Basic_font}
        color = Color.Black
    }

    DrawText(TurnText)

    let TurnText12 = {
        txt = $"{state.turn}/12"
        font = Turn12_font
        pos = { X = TurnText.X ; Y = TurnText.Y + (Basic_font + Turn12_font) / 2}
        color = Color.Black
    }

    DrawText(TurnText12)

    let CategoryRect = {
        txt = {
            txt = "Categories"
            font = Basic_font
            pos = { X = TurnText.X ; Y = TurnText12.Y + (Turn12_font + Basic_font + Basic_TextSpacing) / 2 + Turn_Space_Category}
            color = Color.White
        }
        size = { W = Entry_Width ; H = Basic_font + Basic_TextSpacing }
        pos = { X = TurnText.X ; Y = TurnText12.Y + (Turn12_font + Basic_font + Basic_TextSpacing) / 2 + Turn_Space_Category}
        color = Color.Black
    }

    DrawRect(CategoryRect)

    let helper_Numbers (idx : int, x : ScoreCategory) = 
        let ScoreRect = {
            txt = {
                txt = CategoryToString x
                font = Basic_font
                pos = { X = TurnText.X ; Y = CategoryRect.Y + CategoryRect.H * idx}
                color = Color.White
            }
            size = { W = CategoryRect.W ; H = CategoryRect.H }
            pos = { X = TurnText.X ; Y = CategoryRect.Y + CategoryRect.H * idx}
            color = Color.Black
        }
        DrawRect(ScoreRect)

    Score_Numbers
    |> List.iter helper_Numbers


    let SubtotalRect = {
        txt = {
            txt = "Subtotal"
            font = Subtotal_TextSize
            pos = { X = TurnText.X ; Y = CategoryRect.Y + CategoryRect.H * 6 + (CategoryRect.H + Subtotal_TextSize + Basic_TextSpacing) / 2}
            color = Color.White
        }
        size = { W = CategoryRect.W ; H = Subtotal_TextSize + Basic_TextSpacing }
        pos = { X = TurnText.X ; Y = CategoryRect.Y + CategoryRect.H * 6 + (CategoryRect.H + Subtotal_TextSize + Basic_TextSpacing) / 2}
        color = Color.Black
    }
    DrawRect(SubtotalRect)

    let Bonus35Rect = {
        txt = {
            txt = "+35 Bonus"
            font = Bonus_TextSize
            pos = { X = TurnText.X ; Y = SubtotalRect.Y + (Bonus_TextSize + SubtotalRect.H + Basic_TextSpacing) / 2}
            color = Color.White
        }
        size = { W = CategoryRect.W ; H = Bonus_TextSize + Basic_TextSpacing}
        pos = { X = TurnText.X ; Y = SubtotalRect.Y + (Bonus_TextSize + SubtotalRect.H + Basic_TextSpacing) / 2}
        color = Color.Black
    }
    DrawRect(Bonus35Rect)

    let ExplanationText = {
        txt = "Bonus if Ones - Sixes are over 63 points"
        font = Explanation_TextSize
        pos = { X = TurnText.X ; Y = Bonus35Rect.Y + (Bonus35Rect.H + Explanation_TextSize) / 2}
        color = Color.Black
    }
    DrawText({ExplanationText with pos = {ExplanationText.pos with X = ExplanationText.width / 2 + Basic_TextSpacing}})

    let helper_Others (idx : int, x : ScoreCategory) = 
        let ScoreRect = {
            txt = {
                txt = CategoryToString x
                font = Basic_font
                pos = { X = TurnText.X ; Y = ExplanationText.Y + (ExplanationText.font + CategoryRect.H) / 2 + CategoryRect.H * (idx - 1)}
                color = Color.White
            }
            size = { W = CategoryRect.W ; H = CategoryRect.H }
            pos = { X = TurnText.X ; Y = ExplanationText.Y + (ExplanationText.font + CategoryRect.H) / 2 + CategoryRect.H * (idx - 1)}
            color = Color.Black
        }
        DrawRect(ScoreRect)

    Score_Others
    |> List.iter helper_Others

    let TotalRect = {
        txt = {
            txt = "Total"
            font = Total_TextSize
            pos = { X = TurnText.X ; Y = Yacht_Space_Total + ExplanationText.Y + ExplanationText.font / 2 + CategoryRect.H * 6 + (Total_TextSize + Basic_TextSpacing) / 2}
            color = Color.White
        }
        size = { W = CategoryRect.W ; H = Total_TextSize + Basic_TextSpacing }
        pos = { X = TurnText.X ; Y = Yacht_Space_Total + ExplanationText.Y + ExplanationText.font / 2 + CategoryRect.H * 6 + (Total_TextSize + Basic_TextSpacing) / 2}
        color = Color.Black
    }
    DrawRect(TotalRect)

    state
