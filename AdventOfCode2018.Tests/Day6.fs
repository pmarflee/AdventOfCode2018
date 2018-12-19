namespace Day6
    module Day6 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core.Day6
        open System

        type Tests () =

            static member PartData
                with get() = [ [| [| "1, 1"; "1, 6"; "8, 3"; "3, 4"; "5, 5"; "8, 9" |] :> obj;
                                     17 :> obj |]; ]
            [<Theory>]
            [<MemberData("PartData")>]
            member _verify.``Calculate size of largest area that isn't infinite`` (input : string[], expected : int) =
                calculate 1 input |> should equal expected

