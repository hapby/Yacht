module Utils

open Config

let ScoreCategories = 
    [
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

let NumToPlayer idx = 
    match idx with
    | 1 -> P1
    | 2 -> P2
    | 3 -> P3
    | 4 -> P4
    | _ -> failwith "No player over 4 exists"

let AddDice idx state rect =
    state.Dices <- Map.add idx rect state.Dices
    state

let SelectDice idx state = 
    let v = Map.find idx state.IsDiceSelected
    state.IsDiceSelected <- Map.add idx (not v) state.IsDiceSelected
    state

let GetDice idx state = 
    Map.find idx state.Dices 

let GetDiceSelected idx state = 
    Map.find idx state.IsDiceSelected 

let AddButton idx state rect scoreCategory = 
    let playerKey = NumToPlayer idx

    let inner =
        match Map.tryFind playerKey state.ScoringTable with
        | Some m -> m
        | None -> failwith "Unable to find player in ScoringTable"

    state.ScoringTable <-
        state.ScoringTable |> Map.add playerKey (Map.add scoreCategory (Some rect) inner)
    
    state


let AddScore plr state score scoreCategory = 

    let inner =
        match Map.tryFind plr state.Score with
        | Some m -> m
        | None -> failwith "Unable to find player in ScoringTable"

    state.Score <-
        state.Score |> Map.add plr (Map.add scoreCategory score inner)
    
    state

let SetTouch plr state scoreCategory = 

    let inner =
        match Map.tryFind plr state.IsTableTouched with
        | Some m -> m
        | None -> failwith "Unable to find player in ScoringTable"

    state.IsTableTouched <-
        state.IsTableTouched |> Map.add plr (Map.add scoreCategory true inner)
    
    state

let GetTouch plr state scoreCategory =
     
    let inner =
        match Map.tryFind plr state.IsTableTouched with
        | Some m -> m
        | None -> failwith "Unable to find player in ScoringTable"
    
    match Map.tryFind scoreCategory inner with
    | Some m -> m
    | None -> failwith "Some score is not defined"

let GetScore idx state scoreCategory = 
    let playerKey = NumToPlayer idx
    
    let inner =
        match Map.tryFind playerKey state.Score with
        | Some m -> m
        | None -> failwith "Unable to find player in ScoringTable"
    
    match Map.tryFind scoreCategory inner with
    | Some m -> 
        if GetTouch playerKey state scoreCategory then string m
        else ""
    | None -> failwith "Some score is not defined"


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

let UnBox x = 
    match x with
    | Some v -> v
    | _ -> failwith "Some object is not initialized."