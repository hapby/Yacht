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


module Util_Funcs = 
  
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
  
  let UnBox x = 
    match x with
    | Some v -> v
    | _ -> failwith "Some object is not initialized."

type Scene =
  | StartMenu
  | Game

type State = 
  {
    Nplayers : int
    scene : Scene
    
    windowSize : Size

    StartButton : Rectangle option
    PlusButton : Rectangle option
    MinusButton : Rectangle option

  }