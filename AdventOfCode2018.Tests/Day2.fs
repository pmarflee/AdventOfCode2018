namespace Day2
    module Day2 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core

        type Tests () =

            static member Part1Data 
                with get() = [ [| [ "abcdef"; 
                                    "bababc"; 
                                    "abbcde"; 
                                    "abcccd"; 
                                    "aabcdd"; 
                                    "abcdee"; 
                                    "ababab" ] :> obj;
                                    12 :> obj |] ]

            static member Part2Data
                with get() = [ [| [| "abcde";
                                    "fghij";
                                    "klmno";
                                    "pqrst";
                                    "fguij";
                                    "axcye";
                                    "wvxyz" |] :> obj;
                                    "fgij" :> obj |] ]

            [<Theory>]
            [<MemberData("Part1Data")>]
            member _verify.``Calculate Checksum (Part 1)`` (input: seq<string>, expected: int) =
                Day2.calculatePart1 input |> should equal expected
                    
            [<Theory>]
            [<MemberData("Part2Data")>]
            member _verify.``Find Common Letters Between Correct Box IDs`` (input : string[], expected: string) =
                Day2.calculatePart2 input |> should equal expected
