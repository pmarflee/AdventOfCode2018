namespace AdventOfCode2018.Core

module Day3 =
    open System
    open System.Text.RegularExpressions

    type Claim = { Number : int;
                   LeftOffset : int;
                   TopOffset : int;
                   Width : int;
                   Height : int }
                   member this.Areas = seq {
                       for col = this.LeftOffset to this.LeftOffset + this.Width - 1 do
                           for row = this.TopOffset to this.TopOffset + this.Height - 1 do
                               yield (col, row)
                   }

    let private _regex = new Regex("^#([0-9]+)\s@\s([0-9]+),([0-9]+):\s([0-9]+)x([0-9]+)$")

    let parse line = 
        let m = _regex.Match(line)
        { Number = int m.Groups.[1].Value;
          LeftOffset = int m.Groups.[2].Value;
          TopOffset = int m.Groups.[3].Value;
          Width = int m.Groups.[4].Value;
          Height = int m.Groups.[5].Value }

    let calculate part lines = 
        let create (map, claims) line =
            let claim = parse line
            let addAreas map' area = 
                let claims = match Map.tryFind area map' with
                             | Some(claims') -> Set.add claim.Number claims'
                             | None -> Set.singleton claim.Number
                Map.add area claims map'
            (claim.Areas |> Seq.fold addAreas map, claim :: claims)
        let (areas, claims) = lines |> Seq.fold create (Map.empty, [])
        let areasList = Map.toList areas
        let hasNoOverlappingAreas claim = 
            areasList 
            |> Seq.where (fun (_, claims') -> Set.contains claim.Number claims')
            |> Seq.forall (fun (_, claims') -> Set.count claims' = 1)
        match part with
        | 1 -> areasList 
                |> List.filter (fun (_, claims) -> claims.Count > 1) 
                |> List.length
        | 2 -> (List.find hasNoOverlappingAreas claims).Number
        | _ -> raise <| new ArgumentOutOfRangeException("part")
