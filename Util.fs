namespace Util

open Raylib_cs

type Size = 
  { W : int; H : int}

type Pos = 
  { X : int; Y : int}

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

type State = 
  {
    Nplayers : int
    scene : Scene
    turn : int
    
    windowSize : Size

    StartButton : Rectangle option
    PlusButton : Rectangle option
    MinusButton : Rectangle option

    ScoringTable : Map<Player, Map<ScoreCategory, Rectangle option>>

  }


module Util_Funcs = 

  let ScoreCategories = [
      Ones
      Twos
      Threes
      Fours
      Fives
      Sixes
      Choice
      FourKind
      FullHouse
      SmallStraight
      LargeStraight
      Yacht
  ]

  let CategoryToString cat= 
    match cat with
    | Ones -> "Ones"
    | Twos -> "Twos"
    | Threes -> "Threes"
    | Fours -> "Fours"
    | Fives -> "Fives"
    | Sixes -> "Sixes"
    | Choice -> "Choice"
    | FourKind -> "FourKind"
    | FullHouse -> "FullHouse"
    | SmallStraight -> "SmallStraight"
    | LargeStraight -> "LargeStraight"
    | Yacht -> "Yacht"

  let Score_Numbers = [
      (1, Ones)
      (2, Twos)
      (3, Threes)
      (4, Fours)
      (5, Fives)
      (6, Sixes)
  ]

  let Score_Others = [
      (1, Choice)
      (2, FourKind)
      (3, FullHouse)
      (4, SmallStraight)
      (5, LargeStraight)
      (6, Yacht)
  ]

  let Players = [
      P1; P2; P3; P4
  ]

  let centerWindow () =

    let monitorWidth = Raylib.GetMonitorWidth(0)
    let monitorHeight = Raylib.GetMonitorHeight(0)

    let windowWidth = Raylib.GetScreenWidth()
    let windowHeight = Raylib.GetScreenHeight()

    let x = (monitorWidth - windowWidth) / 2
    let y = (monitorHeight - windowHeight) / 2

    Raylib.SetWindowPosition(x, y)
  
  let DrawText (T : Text) = 
    Raylib.DrawText(
        T.txt,
        T.pos.X - (T.width) / 2,
        T.pos.Y - (T.font) / 2,
        T.font,
        T.color
    )

  let DrawButton (B : Button) = 
    Raylib.DrawRectangle(
        B.X - B.W / 2,
        B.Y - B.H / 2,
        B.W,
        B.H,
        B.color
    )
    DrawText (B.txt)

  let DrawRect (B : Button) = 
    Raylib.DrawRectangleLinesEx(
        B.rect,
        3.0f,
        B.color
    )
    DrawText (B.txt)
  
  let UnBox x = 
    match x with
    | Some v -> v
    | _ -> failwith "Some object is not initialized."