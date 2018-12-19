namespace Day6
    module Day6 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core.Day6
        open System

        type Tests () =

            static member PartData
                with get() = [ [| [| "1, 1"; "1, 6"; "8, 3"; "3, 4"; "5, 5"; "8, 9" |] :> obj;
                                     17 :> obj;
                                     16 :> obj |]; ]

            [<Theory>]
            [<MemberData("PartData")>]
            member _verify.``Calculate size of largest area that isn't infinite`` (input : string[], expected : int, _) =
                calculatePart1 input |> should equal expected

            [<Theory>]
            [<MemberData("PartData")>]
            member _verify.``Calculate size of region containing all locations which have a total distance to all given coordinates of less than 32`` (input : string[], _, expected : int) =
                calculatePart2 32 input |> should equal expected

