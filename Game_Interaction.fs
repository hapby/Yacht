#nowarn "3391"
module Game_Interaction

open Config
open Raylib_cs
open Utils


let CountDice (dice_pattern : int list) (target_num : int) = //"11122" "2" returns 2
    dice_pattern
    |> List.fold (fun state e -> if e = target_num then state + 1 else state) 0

let ChoicePattern (dice_pattern : int list) = 
    dice_pattern 
    |> List.fold (fun state e -> e + state) 0

let FourKindPattern (dice_pattern : int list) = 
    let lst = List.init 6 (fun x -> x + 1)

    lst
    |> List.fold (fun state e -> if CountDice dice_pattern e >= 4 then max state (e * 4) else state)  0

let FullHousePattern (dice_pattern : int list) = 
    let lst = List.init 6 (fun x -> x + 1) 

    let count_lst = lst |> List.fold (fun state e ->  CountDice dice_pattern e :: state)  [] |> List.rev

    let A = count_lst |> List.tryFindIndex (fun e -> e = 3)
    let B = count_lst |> List.tryFindIndex (fun e -> e = 2)

    if A = None || B = None then 0
    else UnBox A * 3 + unbox B * 2

let SmallStraightPattern (dice_pattern : int list) = 
    let lst = List.init 6 (fun x -> x + 1) 

    let count_lst = lst |> List.fold (fun state e ->  CountDice dice_pattern e :: state)  [] |> List.rev

    let A = count_lst |> List.forall (fun e -> e < 2)
    let B = count_lst |> List.tryFindIndex (fun e -> e = 0)

    if A && B = Some 6 then 30
    else 0

let LargeStraightPattern (dice_pattern : int list) = 
    let lst = List.init 6 (fun x -> x + 1) 

    let count_lst = lst |> List.fold (fun state e ->  CountDice dice_pattern e :: state)  [] |> List.rev

    let A = count_lst |> List.forall (fun e -> e < 2)
    let B = count_lst |> List.tryFindIndex (fun e -> e = 0)

    if A && B = Some 1 then 30
    else 0

let YachtPattern (dice_pattern : int list) = 
    let lst = List.init 6 (fun x -> x + 1) 

    let count_lst = lst |> List.fold (fun state e ->  CountDice dice_pattern e :: state)  [] |> List.rev
    let A = count_lst |> List.tryFindIndex (fun e -> e = 5)

    match A with
    | Some v -> 5 * v
    | _ -> 0


let CalcScore (target_Category : ScoreCategory) (dice_pattern : int list) = 
    match target_Category with 
    | Ones -> 1 * CountDice dice_pattern 1
    | Twos -> 2 * CountDice dice_pattern 2
    | Threes -> 3 * CountDice dice_pattern 3
    | Fours -> 4 * CountDice dice_pattern 4
    | Fives -> 5 * CountDice dice_pattern 5
    | Sixes -> 6 * CountDice dice_pattern 6
    | Choice -> ChoicePattern dice_pattern
    | FourKind -> FourKindPattern dice_pattern
    | FullHouse -> FullHousePattern dice_pattern
    | SmallStraight -> SmallStraightPattern dice_pattern
    | LargeStraight -> LargeStraightPattern dice_pattern
    | Yacht -> YachtPattern dice_pattern

let ScoreButtonIC (plr : Player) (state : State) (x : ScoreCategory) = 
    let mousePos =
        Raylib.GetMousePosition()

    let inner =
        match Map.tryFind plr state.ScoringTable with
        | Some m -> m
        | None -> failwith "Unable to find player in ScoringTable"
    
    let button = 
        match Map.tryFind x inner with
        | Some m -> m
        | None -> failwith "Unable to find player in InnerScoringTable"

    let isHover =
        Raylib.CheckCollisionPointRec(
            mousePos,
            UnBox button
        )

    let isClicked =
        isHover && Raylib.IsMouseButtonPressed(MouseButton.Left)
    
    if isClicked && GetTouch plr state x = false then 
        let Nstate = AddScore plr state (CalcScore x (state.dice_pattern)) x
        SetTouch plr Nstate x
    else state

let update (state : State) = 

    let plst = List.init state.Nplayers (fun x -> x + 1)
    let Plrs = List.map NumToPlayer plst

    state
    |> fun state -> 
        List.fold (
            fun state p -> 
                state
                |> fun state -> List.fold (ScoreButtonIC p) state ScoreCategories
        ) state Plrs

    