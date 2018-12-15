namespace Day4
    module Day4 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2018.Core.Day4
        open System

        type Tests () =

            static member ParserData
                with get() = [ [| "[1518-11-01 00:00] Guard #10 begins shift" :> obj;
                                  { Date = new DateTime(1518, 11, 01);
                                    GuardNumber = 10;
                                    Type = BeginsShift } :> obj |]; ]

            static member Part1Data
                with get() = [ [| [| "[1518-11-01 00:00] Guard #10 begins shift";
                                     "[1518-11-01 00:05] falls asleep";
                                     "[1518-11-01 00:25] wakes up";
                                     "[1518-11-01 00:30] falls asleep";
                                     "[1518-11-01 00:55] wakes up";
                                     "[1518-11-01 23:58] Guard #99 begins shift";
                                     "[1518-11-02 00:40] falls asleep";
                                     "[1518-11-02 00:50] wakes up";
                                     "[1518-11-03 00:05] Guard #10 begins shift";
                                     "[1518-11-03 00:24] falls asleep";
                                     "[1518-11-03 00:29] wakes up";
                                     "[1518-11-04 00:02] Guard #99 begins shift";
                                     "[1518-11-04 00:36] falls asleep";
                                     "[1518-11-04 00:46] wakes up";
                                     "[1518-11-05 00:03] Guard #99 begins shift";
                                     "[1518-11-05 00:45] falls asleep";
                                     "[1518-11-05 00:55] wakes up" |] :> obj;
                                     240 :> obj |] ]

            [<Theory>]
            [<MemberData("ParserData")>]
            member _verify.``Parse event`` (input : string, expected : Event) =
                parseEvent input { Guards = Map.empty; Event = None } |> should equal expected

            [<Theory>]
            [<MemberData("Part1Data")>]
            member _verify.``Find best guard/minute combination`` (input : string[], expected : int) =
                calculatePart1 input |> should equal expected

