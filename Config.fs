namespace Config

open Raylib_cs

type Size = 
  { W : int; H : int}

type Pos = 
  { X : int; Y : int}

type TableConfig (Basic_Font : int)= 
  member this.Basic_Font  = Basic_Font
  member this.Explanation_TxtSize = Basic_Font * 2 / 3
  member this.Turn_Space_Category = Basic_Font / 3
  member this.Yacht_Space_Total = Basic_Font / 3

  member this.Basic_TextSpacing = Basic_Font * 2 / 3
  member this.Turn12_font = Basic_Font * 5 / 3
  member this.CatEntry_Width = this.Turn12_font * 9 / 2
  member this.Entry_Width = this.Basic_Font * 9 / 2
  member this.Subtotal_TextSize = Basic_Font * 4 / 5
  member this.Bonus_TextSize = Basic_Font * 8 / 7
  member this.Total_TextSize = this.Bonus_TextSize

  member this.StartPos = {X = this.CatEntry_Width * 9 / 16 ; Y = Basic_Font * 3 / 2}
  

type DiceConfig (DiceSize : int) = 

  member this.DiceSize = DiceSize
  member this.Dice_Space_Dice = DiceSize / 8



type Text = 
  { 
    txt : string
    font : int
    pos : Pos
    color : Color
  }

  member this.width = 
    Raylib.MeasureText(this.txt, this.font)
  member this.X = this.pos.X
  member this.Y = this.pos.Y

///pos is center coordinate of Button
type Button = 
  { 
    txt : Text
    size : Size
    pos : Pos
    color : Color
  }

  member this.W = this.size.W
  member this.H = this.size.H
  member this.X = this.pos.X
  member this.Y = this.pos.Y
  member this.rect = 
    Rectangle(float32 (this.X - this.W / 2), float32 (this.Y - this.H / 2), float32 this.W, float32 this.H)


type Player = 
  | P1
  | P2
  | P3
  | P4

type ScoreCategory =
    | Ones
    | Twos
    | Threes
    | Fours
    | Fives
    | Sixes
    | Choice
    | FourKind
    | FullHouse
    | SmallStraight
    | LargeStraight
    | Yacht

type Scene =
  | StartMenu
  | Game
  | GamePause
  | GameOver

type State = 
  {
    Nplayers : int
    mutable scene : Scene
    mutable turn : int
    mutable player_turn : int
    mutable Roll_Count : int

    mutable ReturnMenuButton : Rectangle option
    
    windowSize : Size

    StartButton : Rectangle option
    PlusButton : Rectangle option
    MinusButton : Rectangle option

    Tabel_Init : TableConfig
    mutable ScoringTable : Map<Player, Map<ScoreCategory, Rectangle option>>
    mutable Score : Map<Player, Map<ScoreCategory, int>>
    mutable IsTableTouched : Map<Player, Map<ScoreCategory, bool>>

    mutable Dices : Map<int, Rectangle option> // n -> nth dice
    mutable IsDiceSelected : Map<int, bool> 

    dice_pattern : int list

    mutable ShakePower : float32
    mutable AnimTime : float32
    mutable AnimElapsed : float32
    mutable NextRollTime : float32
    mutable IsAnimating : bool
    mutable IsShaking : bool

    MAX_NAME_LENGTH : int
    mutable Player_Names : Map<Player, string>
    mutable Player_NameRects : Map<Player, Rectangle option>
    mutable Getting_NameInput : bool
    mutable EditingPlayer : Player option
    mutable NameInputBuffer : string
  }