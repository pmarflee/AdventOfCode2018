namespace AdventOfCode2018.Core

module Day3 =
    open System
    open System.Text.RegularExpressions

    type Claim = { Number : int;
                   LeftOffset : int;
                   TopOffset : int;
                   Width : int;
                   Height : int }

    let private _regex = new Regex("^#([0-9]+)\s@\s([0-9]+),([0-9]+):\s([0-9]+)x([0-9]+)$")

    let parse input = 
        let m = _regex.Match(input)
        { Number = int m.Groups.[1].Value;
          LeftOffset = int m.Groups.[2].Value;
          TopOffset = int m.Groups.[3].Value;
          Width = int m.Groups.[4].Value;
          Height = int m.Groups.[5].Value }

    let calculatePart1 (lines : seq<string>) = raise <| new NotImplementedException()
