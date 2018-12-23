module Day9

open Xunit
open FsUnit.Xunit
open AdventOfCode2018.Core

[<Theory>]
[<InlineData("10 players; last marble is worth 1618 points", 10, 1618)>]
[<InlineData("13 players; last marble is worth 7999 points", 13, 7999)>]
[<InlineData("17 players; last marble is worth 1104 points", 17, 1104)>]
[<InlineData("21 players; last marble is worth 6111 points", 21, 6111)>]
[<InlineData("30 players; last marble is worth 5807 points", 30, 5807)>]
let ``Should parse input correctly`` input expectedPlayers expectedMarbles =
    Day9.parse input |> should equal (expectedPlayers, expectedMarbles)

[<Theory>]
[<InlineData(9, 25, 32)>]
[<InlineData(10, 1618, 8317)>]
[<InlineData(13, 7999, 146373)>]
[<InlineData(17, 1104, 2764)>]
[<InlineData(21, 6111, 54718)>]
[<InlineData(30, 5807, 37305)>]
let ``Should calculate highest score correctly`` players marbles expected =
    Day9.calculatePart1 (players, marbles) |> should equal expected

