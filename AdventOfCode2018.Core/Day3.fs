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

    let parse line = 
        let m = _regex.Match(line)
        { Number = int m.Groups.[1].Value;
          LeftOffset = int m.Groups.[2].Value;
          TopOffset = int m.Groups.[3].Value;
          Width = int m.Groups.[4].Value;
          Height = int m.Groups.[5].Value }

    let calculatePart1 (lines : seq<string>) = 
        let createMap map line =
            let claim = parse line
            let areas = seq {
                for col = claim.LeftOffset to claim.LeftOffset + claim.Width - 1 do
                    for row = claim.TopOffset to claim.TopOffset + claim.Height - 1 do
                        yield (col, row)
            }
            let addAreas map' area = 
                let claims = match Map.tryFind area map' with
                             | Some(claims') -> Set.add claim claims'
                             | None -> Set.singleton claim
                Map.add area claims map'
            areas |> Seq.fold addAreas map
        lines 
        |> Seq.fold createMap Map.empty<(int * int), Set<Claim>>
        |> Map.toSeq
        |> Seq.filter (fun (_, claims) -> claims.Count > 1)
        |> Seq.length
