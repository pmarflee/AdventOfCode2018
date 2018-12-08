namespace AdventOfCode2018.Core

module Day2 =

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

