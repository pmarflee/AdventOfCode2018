namespace AdventOfCode2018.Core

module Day1 =
    open System

    let calculatePart1 (input : seq<string>) = Seq.sumBy int input
    let calculatePart2 (input : seq<string>) = raise <| new NotImplementedException()