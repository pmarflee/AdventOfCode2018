namespace Day1
    module Day1 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core

        type Tests () =

            static member Part1Data
                with get() = [ [| [ "+1"; "-2"; "+3"; "+1" ] :> obj; 3 :> obj |];
                               [| [ "+1"; "+1"; "+1" ] :> obj; 3 :> obj |];
                               [| [ "+1"; "+1"; "-2" ] :> obj; 0 :> obj |];
                               [| [ "-1"; "-2"; "-3" ] :> obj; -6 :> obj |] ]

            static member Part2Data
                with get() = [ [| [ "+1"; "-2"; "+3"; "+1" ] :> obj; 2 :> obj |];
                               [| [ "+1"; "-1" ] :> obj; 0 :> obj |] ;
                               [| [ "+3"; "+3"; "+4"; "-2"; "-4" ] :> obj; 10 :> obj |];
                               [| [ "-6"; "+3"; "+8"; "+5"; "-6" ] :> obj; 5 :> obj |];
                               [| [ "+7"; "+7"; "-2"; "-7"; "-4" ] :> obj; 14 :> obj |] ]

            [<Theory>]
            [<MemberData("Part1Data")>]
            member _verify.``Calculate Frequency (Part 1)`` (input: seq<string>, expected: int) =
                Day1.calculatePart1 input |> should equal expected

            [<Theory>]
            [<MemberData("Part2Data")>]
            member _verify.``Calculate First Frequency Reached Twice (Part 2)`` (input: seq<string>, expected: int) =
                Day1.calculatePart2 input |> should equal expected
                    
