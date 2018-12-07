namespace AdventOfCode2018.Core

module Day1 =

    let calculatePart1 (input : seq<string>) = Seq.sumBy int input

    let calculatePart2 (input : seq<string>) = 
        let inputList = Seq.toList input
        let rec calculate (inputList' : string list) frequencies currentFrequency =
            let number = inputList' |> List.head |> int
            let newFrequency = currentFrequency + number
            if Set.contains newFrequency frequencies then 
                newFrequency
            else
                let newInput = match List.tail inputList' with
                               | [] -> inputList
                               | tail -> tail
                calculate newInput (Set.add newFrequency frequencies) newFrequency
        calculate inputList (Set.singleton 0) 0
