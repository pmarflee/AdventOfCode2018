module Day5

open Xunit
open FsUnit.Xunit
open AdventOfCode2018.Core

[<Theory>]
[<InlineData('a', 'A', true)>]
[<InlineData('a', 'a', false)>]
[<InlineData('a', 'b', false)>]
[<InlineData('A', 'b', false)>]
[<InlineData('A', 'a', true)>]
let ``Should react`` first second expected =
    Day5.willReact first second |> should equal expected

[<Theory>]
[<InlineData("aA", 0)>]
[<InlineData("abBA", 0)>]
[<InlineData("abAB", 4)>]
[<InlineData("aabAAB", 6)>]
[<InlineData("dabAcCaCBAcCcaDA", 10)>]
let ``Calculate Part 1 Result`` input expected =
    Day5.calculate 1 input |> should equal expected 
