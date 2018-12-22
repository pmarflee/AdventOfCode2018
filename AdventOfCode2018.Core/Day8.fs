namespace AdventOfCode2018.Core

module Day8 =

    let parse input = Parser.parseNumbers Parser.splitChars input
                      |> Array.head
                      |> List.ofArray

    let calculatePart1 input = 
        let rec take numberToTake numberTaken taken remaining =
            if numberTaken = numberToTake || List.isEmpty remaining then
                taken, remaining
            else
                take numberToTake (numberTaken + 1) 
                    (List.head remaining :: taken) (List.tail remaining)

        let rec calculate input sum depth maxDepth =
            let quantityOfChildNodes, quantityOfMetadata, remaining = 
                match input with
                | qc :: qm :: tl -> qc, qm, tl
                | _ -> 0, 0, []
            let input, sum = 
                match quantityOfChildNodes with 
                | 0 -> remaining, sum
                | q -> calculate remaining sum 1 q
            let metadata, remaining = take quantityOfMetadata 0 [] input
            let sum = sum + List.sum metadata
            if depth = maxDepth then remaining, sum
            else calculate remaining sum (depth + 1) maxDepth

        calculate input 0 0 1 |> snd