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
    run "Day 2 Part 1:" (readAllLines "Day2.txt") (Day2.calculatePart1)
    run "Day 2 Part 2:" (readAllLines "Day2.txt") (Day2.calculatePart2)
    run "Day 3 Part 1:" (readAllLines "Day3.txt") (Day3.calculate 1)
    run "Day 3 Part 2:" (readAllLines "Day3.txt") (Day3.calculate 2)
    run "Day 4 Part 1:" (readAllLines "Day4.txt") (Day4.calculate 1)
    run "Day 4 Part 2:" (readAllLines "Day4.txt") (Day4.calculate 2)

    printfn ""
    printfn "Finished"

    0 // Return an integer exit code
