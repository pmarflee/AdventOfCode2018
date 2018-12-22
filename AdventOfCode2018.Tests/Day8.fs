namespace Day8
    module Day8 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core.Day8

        type Tests () =

            static member PartData
                with get() = [ [| "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2" :> obj;
                                  [ 2; 3; 0; 3; 10; 11; 12; 1; 1; 0; 1; 99; 2; 1; 1; 2 ] :> obj; |] ]

            [<Theory>]
            [<MemberData("PartData")>]
            member _verify.``Should parse input correctly`` (input : string) (expected : int list) =
                parse input |> should equal expected
