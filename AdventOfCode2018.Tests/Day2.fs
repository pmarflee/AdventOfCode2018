namespace Day2
    module Day2 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core

        type Tests () =

            static member Data 
                with get() = [ [| [ "abcdef"; 
                                    "bababc"; 
                                    "abbcde"; 
                                    "abcccd"; 
                                    "aabcdd"; 
                                    "abcdee"; 
                                    "ababab" ] :> obj;
                                    12 :> obj |] ]

            [<Theory>]
            [<MemberData("Data")>]
            member _verify.``Calculate Checksum (Part 1)`` (input: seq<string>, expected: int) =
                Day2.calculatePart1 input |> should equal expected
                    
