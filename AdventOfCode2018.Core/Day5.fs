namespace AdventOfCode2018.Core

module Day5 =
    open System

    let internal willReact char1 char2 =
        Char.IsUpper char1 <> Char.IsUpper char2 && Char.ToUpper char1 = Char.ToUpper char2

    let calculate part input =

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

        input |> Seq.toList |> calculate' [] |> List.length
