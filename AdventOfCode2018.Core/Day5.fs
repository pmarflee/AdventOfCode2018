namespace AdventOfCode2018.Core

module Day5 =
    open System

    let internal willReact char1 char2 =
        Char.IsUpper char1 <> Char.IsUpper char2 && Char.ToUpper char1 = Char.ToUpper char2

    let calculate part (input : string) =

        let inputList = Seq.toList input

        let rec calculate' result remaining =
            match remaining with
            | [] -> List.rev result
            | first :: second :: tail ->
                if willReact first second then
                    let (result', remaining') = 
                        match result with
                        | [] -> result, tail
                        | _ -> (List.tail result), ((List.head result) :: tail)
                    calculate' result' remaining'
                else
                    calculate' (first :: result) (second :: tail)
            | first :: [] -> calculate' (first :: result) []

        let removeChars charToRemove = 
            let charToRemoveUpper = Char.ToUpper charToRemove
            List.where (fun c -> c <> charToRemove && c <> charToRemoveUpper) inputList

        match part with
        | 1 -> inputList |> calculate' [] |> List.length
        | 2 -> 
            inputList 
            |> List.map Char.ToLower 
            |> List.distinct
            |> List.map (removeChars >> calculate' [])
            |> List.map List.length
            |> List.minBy id
        | _ -> raise <| new ArgumentOutOfRangeException("part")
