namespace Day1
    module Day1 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core

        type Tests () =

            static member Data
                with get() = [ [| [ "+1"; "-2"; "+3"; "+1" ] :> obj; 3 :> obj |];
                               [| [ "+1"; "+1"; "+1" ] :> obj; 3 :> obj |];
                               [| [ "+1"; "+1"; "-2" ] :> obj; 0 :> obj |];
                               [| [ "-1"; "-2"; "-3" ] :> obj; -6 :> obj |] ]

            [<Theory>]
            [<MemberData("Data")>]
            member _verify.``Calculate Frequency (Part 1)`` (input: seq<string>, expected: int) =
                Day1.calculatePart1 input |> should equal expected
                    
