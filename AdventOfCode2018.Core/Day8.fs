namespace AdventOfCode2018.Core

module Day8 =
    open System

    let parse input = Parser.parseNumbers Parser.splitChars input
                      |> Array.head
                      |> List.ofArray

    let calculatePart1 input = raise <| new NotImplementedException()
