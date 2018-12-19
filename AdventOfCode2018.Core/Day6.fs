namespace AdventOfCode2018.Core

module Day6 =

    let calculate part input =

        let parse i (line : string) = 
            let elements = line.Split(',')
            let parse' (s : string) = int(s.Trim())

            i, parse' elements.[0], parse' elements.[1]

        let coordinates = Array.mapi parse input

        let getDimensions (top : int, bottom : int, left : int, right : int) (_, x : int, y : int) =
            let get vOld vNew compare = if vOld = -1 || compare vNew vOld then vNew else vOld
            (get top y (<), get bottom y (>), get left x (<), get right x (>))

        let (top, bottom, left, right) = coordinates |> Array.fold getDimensions (-1, -1, -1, -1)

        let grid = 

            let manhattanDistance lX lY (_, cX, cY) = abs (lX - cX) + abs (lY - cY)

            let closestCoordinate x y = 
                let (_, closest) = coordinates
                                   |> Array.groupBy (manhattanDistance x y)
                                   |> Array.minBy fst

                if closest.Length > 1 then None
                else 
                    let (id, _, _) = closest.[0]
                    Some(id)

            seq { for x = left to right do
                    for y = top to bottom do
                        let closest = closestCoordinate x y
                        if closest.IsSome then
                            yield (x, y), closest.Value } |> Seq.toArray

        let isNotInfinite (_, x, y) = x > left && x < right && y > top && y < bottom

        grid 
        |> Array.countBy snd
        |> Array.where (fst >> Array.get coordinates >> isNotInfinite)
        |> Array.maxBy snd
        |> snd
