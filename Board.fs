module Board

open Config
open UI_func
open Utils
open Raylib_cs


let DrawPlayers (Tabel_Init : TableConfig) (state : State) =

    let Basic_font = Tabel_Init.Basic_Font
    let Basic_TextSpacing = Tabel_Init.Basic_TextSpacing
    let Entry_Width = Tabel_Init.Entry_Width

    let NameSize = { W = Tabel_Init.Entry_Width; H = Tabel_Init.Basic_Font * 2 + Tabel_Init.Turn12_font + Tabel_Init.Turn_Space_Category + Tabel_Init.Basic_TextSpacing}
    let P1_NameY = (Tabel_Init.StartPos.Y - Tabel_Init.Basic_Font / 2) + NameSize.H / 2
    let P1_NameX = Tabel_Init.StartPos.X + (Tabel_Init.CatEntry_Width + Tabel_Init.Entry_Width) / 2

    let PlayerList = List.init (state.Nplayers) (fun x -> x + 1) 

    let helper state pidx = 
        let shift = Tabel_Init.Entry_Width * (pidx - 1)
        let NameButton = {
            txt = {
                txt = $"P{pidx}"
                font = Basic_font
                pos = { X = P1_NameX + shift; Y = P1_NameY}
                color = Color.White
            }
            size = NameSize
            pos = { X = P1_NameX + shift; Y = P1_NameY}
            color = Color.Black
        }
        DrawRect(NameButton)

        let helper_Numbers (state : State) (idx : int, x : ScoreCategory) = 
            let ScoreButton = {
                txt = {
                    txt = GetScore pidx state x
                    font = Basic_font
                    pos = { X = NameButton.X ; Y = NameButton.Y + (NameButton.H + Basic_font + Basic_TextSpacing) / 2  + (Basic_font + Basic_TextSpacing) * (idx - 1)}
                    color = Color.White
                }
                size = { W = Entry_Width ; H = Basic_font + Basic_TextSpacing }
                pos = { X = NameButton.X ; Y = NameButton.Y + (NameButton.H + Basic_font + Basic_TextSpacing) / 2  + (Basic_font + Basic_TextSpacing) * (idx - 1)}
                color = Color.Black
            }
            DrawRect(ScoreButton)

            AddButton pidx state (ScoreButton.rect) x

        let Nstate = Score_Numbers |> List.fold helper_Numbers state

        let SubtotalRect = {
            txt = {
                txt = ""
                font = Tabel_Init.Subtotal_TextSize
                pos = { X = NameButton.X ; Y = NameButton.Y + NameButton.H / 2 + (Basic_font + Basic_TextSpacing) * 6 + (Tabel_Init.Subtotal_TextSize + Basic_TextSpacing) / 2}
                color = Color.White
            }
            size = { W = Entry_Width ; H = Tabel_Init.Subtotal_TextSize + Basic_TextSpacing }
            pos = { X = NameButton.X ; Y = NameButton.Y + NameButton.H / 2 + (Basic_font + Basic_TextSpacing) * 6 + (Tabel_Init.Subtotal_TextSize + Basic_TextSpacing) / 2}
            color = Color.Black
        }
        DrawRect(SubtotalRect)

        let Bonus35Rect = {
            txt = {
                txt = ""
                font = Tabel_Init.Bonus_TextSize
                pos = { X = NameButton.X ; Y = SubtotalRect.Y + (Tabel_Init.Bonus_TextSize + SubtotalRect.H + Basic_TextSpacing) / 2}
                color = Color.White
            }
            size = { W = Entry_Width ; H = Tabel_Init.Bonus_TextSize + Basic_TextSpacing}
            pos = { X = NameButton.X ; Y = SubtotalRect.Y + (Tabel_Init.Bonus_TextSize + SubtotalRect.H + Basic_TextSpacing) / 2}
            color = Color.Black
        }
        DrawRect(Bonus35Rect)

        let helper_Others (state : State) (idx : int, x : ScoreCategory) = 
            let ScoreButton = {
                txt = {
                    txt = GetScore pidx state x
                    font = Basic_font
                    pos = { X = NameButton.X ; Y = Bonus35Rect.Y + Tabel_Init.Explanation_TxtSize + Bonus35Rect.H / 2 + (Basic_font + Basic_TextSpacing) / 2 + (Basic_font + Basic_TextSpacing) * (idx - 1) }
                    color = Color.White
                }
                size = { W = Entry_Width ; H = Basic_font + Basic_TextSpacing }
                pos = { X = NameButton.X ; Y = Bonus35Rect.Y + Tabel_Init.Explanation_TxtSize + Bonus35Rect.H / 2 + (Basic_font + Basic_TextSpacing) / 2 + (Basic_font + Basic_TextSpacing) * (idx - 1) }
                color = Color.Black
            }
            DrawRect(ScoreButton)

            AddButton pidx state (ScoreButton.rect) x

        let NNstate = Score_Others |> List.fold helper_Others Nstate

        let TotalRect = {
            txt = {
                txt = ""
                font = Tabel_Init.Total_TextSize
                pos = { X = NameButton.X ; Y = Tabel_Init.Yacht_Space_Total + Bonus35Rect.Y + Tabel_Init.Explanation_TxtSize + Bonus35Rect.H / 2 + (Basic_font + Basic_TextSpacing) * 6 + (Tabel_Init.Total_TextSize + Basic_TextSpacing) / 2}
                color = Color.White
            }
            size = { W = Entry_Width ; H = Tabel_Init.Total_TextSize + Basic_TextSpacing }
            pos = { X = NameButton.X ; Y = Tabel_Init.Yacht_Space_Total + Bonus35Rect.Y + Tabel_Init.Explanation_TxtSize + Bonus35Rect.H / 2 + (Basic_font + Basic_TextSpacing) * 6 + (Tabel_Init.Total_TextSize + Basic_TextSpacing) / 2}
            color = Color.Black
        }
        DrawRect(TotalRect)

        NNstate


    PlayerList
    |> List.fold helper state


let DrawCategoty (Tabel_Init : TableConfig) (state) =

    let Basic_font = Tabel_Init.Basic_Font

    let Explanation_TextSize = Tabel_Init.Explanation_TxtSize
    let Turn_Space_Category = Tabel_Init.Turn_Space_Category
    let Yacht_Space_Total = Tabel_Init.Yacht_Space_Total

    let Basic_TextSpacing = Tabel_Init.Basic_TextSpacing
    let Turn12_font = Tabel_Init.Turn12_font
    let CatEntry_Width = Tabel_Init.CatEntry_Width
    let Subtotal_TextSize = Tabel_Init.Subtotal_TextSize
    let Bonus_TextSize = Tabel_Init.Bonus_TextSize
    let Total_TextSize = Tabel_Init.Total_TextSize

    let TurnText = {
        txt = "Turn"
        font = Basic_font
        pos = Tabel_Init.StartPos
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
        size = { W = CatEntry_Width ; H = Basic_font + Basic_TextSpacing }
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

let DrawTable (state) = 

    let Tabel_Init = TableConfig(30)

    state 
    |> DrawCategoty Tabel_Init
    |> DrawPlayers Tabel_Init


let draw (state) = 

    state
    |> DrawTable 
