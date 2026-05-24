module UI_func

open Config
open Raylib_cs

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
        2.0f,
        B.color
    )
    DrawText (B.txt)


  let DrawThickRect (B : Button) = 
    Raylib.DrawRectangleLinesEx(
        B.rect,
        5.0f,
        B.color
    )
    DrawText (B.txt)