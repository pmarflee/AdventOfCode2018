open System.IO
open AdventOfCode2018.Core

[<EntryPoint>]
let main _ =
    printfn "Advent of Code 2018 Solutions"
    printfn "============================="
    printfn ""

    let read path = File.ReadAllText path
    let readAllLines path = File.ReadAllLines path

    let run title input func = printfn "%s %O" title (func input)

    run "Day 1 Part 1:" (readAllLines "Day1.txt") (Day1.calculatePart1)
    run "Day 1 Part 2:" (readAllLines "Day1.txt") (Day1.calculatePart2)

    printfn ""
    printfn "Finished"

    0 // Return an integer exit code
