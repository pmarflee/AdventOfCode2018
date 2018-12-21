namespace Day7
    module Day7 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core.Day7

        type Tests () =

            static member PartData
                with get() = [ [| [| "Step C must be finished before step A can begin.";
                                     "Step C must be finished before step F can begin.";
                                     "Step A must be finished before step B can begin.";
                                     "Step A must be finished before step D can begin.";
                                     "Step B must be finished before step E can begin.";
                                     "Step D must be finished before step E can begin.";
                                     "Step F must be finished before step E can begin." |] :> obj;
                                     "CABDFE" :> obj;
                                     15 :> obj; |] ]

            [<Theory>]
            [<MemberData("PartData")>]
            member _verify.``Calculate correct order of steps`` (input : string[], expected : string, _) =
                calculatePart1 input |> should equal expected

            [<Theory>]
            [<MemberData("PartData")>]
            member _verify.``Calculate time required to complete all steps`` 
                (input : string[], _, expected : int) =
                calculatePart2 2 0 input |> should equal expected

