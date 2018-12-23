namespace AdventOfCode2018.Core

module Day9 =
    open System.Text.RegularExpressions

    let parse input =
        let regex = new Regex("(\d+)\splayers;\slast\smarble\sis\sworth\s(\d+)\spoints")
        let m = regex.Match(input)
        int m.Groups.[1].Value, int m.Groups.[2].Value

    let calculatePart1 (totalPlayers, totalMarbles) =
        
        let rec play players (marbles : ResizeArray<int>) current number =
            let player = (number - 1) % totalPlayers
            let score = Array.get players player
            let getIndex increment = (current + increment + marbles.Count) % marbles.Count
            let scoreMarble () =
                let indexToRemoveAt = getIndex -7
                let marbleToRemove = marbles.[indexToRemoveAt]
                marbles.RemoveAt(indexToRemoveAt)
                indexToRemoveAt, score + number + marbleToRemove
            let addMarble () =
                let insertAt = match number with
                                | 1 -> 1
                                | _ -> (getIndex 1) + 1
                marbles.Insert(insertAt, number)
                insertAt, score
            let current', score' = match number with
                                    | n when n > 0 && n % 23 = 0 -> scoreMarble() 
                                    | _ -> addMarble()

            Array.set players player score'

            match number with
            | n when n = totalMarbles -> Array.max players
            | n -> play players marbles current' (number + 1)

        play 
            (Array.init totalPlayers (fun _ -> 0)) 
            (new ResizeArray<int>(Array.singleton 0 |> Array.toSeq)) 
            0 1
