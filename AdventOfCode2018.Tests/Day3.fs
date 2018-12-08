namespace Day3
    module Day3 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core.Day3
        open Xunit.Sdk

        type Tests () =

            static member ParserData
                with get() = [ [| "#1 @ 1,3: 4x4" :> obj;
                                  { Number = 1; 
                                    LeftOffset = 1; 
                                    TopOffset = 3; 
                                    Width = 4; 
                                    Height = 4 } :> obj |];
                               [| "#2 @ 3,1: 4x4" :> obj;
                                  { Number = 2;
                                    LeftOffset = 3;
                                    TopOffset = 1;
                                    Width = 4;
                                    Height = 4 } :> obj |];
                               [| "#3 @ 5,5: 2x2" :> obj;
                                  { Number = 3;
                                    LeftOffset = 5;
                                    TopOffset = 5;
                                    Width = 2;
                                    Height = 2 } |] ]

            static member Part1Data 
                with get() = [ [| [ "#1 @ 1,3: 4x4"; 
                                    "#2 @ 3,1: 4x4"; 
                                    "#3 @ 5,5: 2x2" ] :> obj;
                                    4 :> obj |] ]

            [<Theory>]
            [<MemberData("ParserData")>]
            member _verify.``Parse claim input`` (input : string, expected : Claim) =
                parse input |> should equal expected

            [<Theory>]
            [<MemberData("Part1Data")>]
            member _verify.``Calculate how many square inches of fabric are within 2 or more claims`` (input : seq<string>, expected : int) =
                calculatePart1 input |> should equal expected

