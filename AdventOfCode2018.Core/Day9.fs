namespace AdventOfCode2018.Core

module Day9 =
    open System.Text.RegularExpressions
    open System.Collections.Generic
    open System.Numerics

    let parse input =
        let regex = new Regex("(\d+)\splayers;\slast\smarble\sis\sworth\s(\d+)\spoints")
        let m = regex.Match(input)
        int m.Groups.[1].Value, int m.Groups.[2].Value

    let calculate (totalPlayers, totalMarbles) =
        
        let rec play players (marbles : LinkedList<int>) 
                    (current : LinkedListNode<int>) (number : int) =
            let next (node : LinkedListNode<int>) = 
                match node.Next with
                | null -> marbles.First
                | _ -> node.Next
            let previous (node : LinkedListNode<int>) = 
                match node.Previous with
                | null -> marbles.Last
                | _ -> node.Previous

            let player = (number - 1) % totalPlayers
            let score = Array.get players player

            let getMarble offset =
                let rec getPrevious (current' : LinkedListNode<int>) count =
                    if count = offset then current'
                    else getPrevious (previous current') (count + 1)
                getPrevious current 0
            let scoreMarble () =
                let marbleToRemove = getMarble 7
                let nextMarble = next marbleToRemove
                marbles.Remove(marbleToRemove)
                nextMarble, score + bigint number + bigint marbleToRemove.Value
            let addMarble () =
                let marbleToAddAfter =
                    if marbles.Count = 1 then current
                    else next current
                marbles.AddAfter(marbleToAddAfter, number), score
            let current', score' = match number with
                                    | n when n > 0 && n % 23 = 0 -> scoreMarble() 
                                    | _ -> addMarble()

            Array.set players player score'

            match number with
            | n when n = totalMarbles -> Array.max players
            | n -> play players marbles current' (number + 1)

        let marbles = new LinkedList<int>(Seq.singleton 0)

        play (Array.init totalPlayers (fun _ -> bigint 0)) marbles marbles.First 1
