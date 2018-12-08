namespace AdventOfCode2018.Core

module Day2 =

    open System

    let calculatePart1 (lines : seq<string>) = 
        let checksum (line : string) =
            let counts = line 
                         |> Seq.toList
                         |> Seq.groupBy id
                         |> Seq.map (fun (_, s) -> Seq.length s)
                         |> Seq.toList
            let hasCountOf v = counts |> List.exists ((=) v)
            (hasCountOf 2, hasCountOf 3)

        let (count2, count3) = lines 
                               |> Seq.map checksum 
                               |> Seq.fold (fun (count2, count3) (hasCount2, hasCount3) -> 
                                                let newCount2 = count2 + if hasCount2 then 1 else 0
                                                let newCount3 = count3 + if hasCount3 then 1 else 0
                                                (newCount2, newCount3)) (0, 0)

        count2 * count3

    let calculatePart2 (lines : string[]) =
        let pairs = seq {
           for i = 0 to lines.Length do 
               for j = i + 1 to lines.Length - 1 do
                   yield (Seq.toArray lines.[i], Seq.toArray lines.[j])
        }

        let removeAt (line : char[]) i = 
            match i with
            | 0 -> String.Concat(line.[1..])
            | i when i = line.Length -> String.Concat(line.[0..line.Length - 1])
            | _ -> String.Concat(line.[0..i - 1]) + String.Concat(line.[i + 1..])

        let tryMatch (line1, line2) = 
            let deltas = line1 
                         |> Array.mapi2 (fun i c1 c2 -> (i, c1, c2)) line2
                         |> Array.where (fun (_, c1, c2) -> c1 <> c2)
            match deltas with
                | [| (i, _, _) |] -> Some(removeAt line1 i)
                | _ -> None

        Seq.pick tryMatch pairs
