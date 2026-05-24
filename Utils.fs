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

let PlayerToString (p : Player) = 
    match p with 
    | P1 -> "P1"
    | P2 -> "P2"
    | P3 -> "P3"
    | P4 -> "P3"

let GetPlayerName (p : Player) (state : State) = 
    Map.find p state.Player_Names

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

let IsAllNumberScored (p : Player) (state : State) = 
    let TouchedMap = Map.find p state.IsTableTouched
    List.fold (fun state e -> (Map.find (snd e) TouchedMap) && state) true Score_Numbers

let AllNumberSum  (p : Player) (state : State) = 
    let ScoreMap = Map.find p state.Score
    List.fold (fun state e -> (Map.find (snd e) ScoreMap) + state) 0 Score_Numbers

let IsAllScored (p : Player) (state : State) = 
    let TouchedMap = Map.find p state.IsTableTouched
    List.fold (fun state e -> (Map.find e TouchedMap) && state) true ScoreCategories

let TotalScoreSum (p : Player) (state : State) = 
    let ScoreMap = Map.find p state.Score
    let bonus = if AllNumberSum p state > 63 then 35 else 0
    bonus + List.fold (fun state e -> (Map.find e ScoreMap) + state) 0 ScoreCategories


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

let FindWinner (state : State) =
    let currentPlayers =
        List.init state.Nplayers (fun x -> x + 1)
        |> List.map NumToPlayer

    let scores =
        currentPlayers
        |> List.map (fun p -> TotalScoreSum p state)

    let ma = List.max scores

    let winnerIdx =
        scores
        |> List.findIndex (fun x -> x = ma)

    (NumToPlayer (winnerIdx + 1), ma)

let init () = 
    let mutable state = 
        {
            Nplayers = 2
            turn = 12
            player_turn = 2
            Roll_Count = 0

            scene = StartMenu
            windowSize = {W = 800; H = 600}

            dice_pattern = List.init 5 (fun x -> x + 1)
            ShakePower = 0f
            AnimTime = 0f
            AnimElapsed = 0f
            NextRollTime = 0f
            IsAnimating = false

            MAX_NAME_LENGTH = 6
            Player_Names = Players |> List.map (fun e -> (e, PlayerToString e)) |> Map.ofList
            Player_NameRects = Players |> List.map (fun e -> (e, None)) |> Map.ofList
            Getting_NameInput = false
            EditingPlayer = None
            NameInputBuffer = ""

            ReturnMenuButton = None

            StartButton = None
            PlusButton = None
            MinusButton = None

            Tabel_Init = TableConfig 30

            ScoringTable = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, None)) |> Map.ofList 
                Players |> List.map (fun x -> (x, emptyPlayerMap)) |> Map.ofList
            
            Score = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, 0)) |> Map.ofList 
                Players |> List.map (fun x -> (x, emptyPlayerMap)) |> Map.ofList
            
            IsTableTouched = 
                let emptyPlayerMap = ScoreCategories |> List.map (fun x -> (x, false)) |> Map.ofList 
                Players |> List.map (fun x -> (x, emptyPlayerMap)) |> Map.ofList

            Dices = List.init 5 id |> List.map (fun e -> (e + 1, None)) |> Map.ofList
            IsDiceSelected = List.init 5 id |> List.map (fun e -> (e + 1, false)) |> Map.ofList
        }
    state