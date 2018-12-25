module Day10

open Xunit
open FsUnit.Xunit
open AdventOfCode2018.Core.Day10

[<Theory>]
[<InlineData("position=< 9,  1> velocity=< 0,  2>", 9, 1, 0, 2)>]
[<InlineData("position=< 7,  0> velocity=<-1,  0>", 7, 0, -1, 0)>]
[<InlineData("position=< 3, -2> velocity=<-1,  1>", 3, -2, -1, 1)>]
[<InlineData("position=< 6, 10> velocity=<-2, -1>", 6, 10, -2, -1)>]
[<InlineData("position=< 2, -4> velocity=< 2,  2>", 2, -4, 2, 2)>]
[<InlineData("position=<-6, 10> velocity=< 2, -2>", -6, 10, 2, -2)>]
[<InlineData("position=< 1,  8> velocity=< 1, -1>", 1, 8, 1, -1)>]
[<InlineData("position=< 1,  7> velocity=< 1,  0>", 1, 7, 1, 0)>]
[<InlineData("position=<-3, 11> velocity=< 1, -2>", -3, 11, 1, -2)>]
[<InlineData("position=< 7,  6> velocity=<-1, -1>", 7, 6, -1, -1)>]
[<InlineData("position=<-2,  3> velocity=< 1,  0>", -2, 3, 1, 0)>]
let ``Should parse input correctly`` input positionX positionY velocityX velocityY =
    let point = { Position = { X = positionX; Y = positionY }; 
                Velocity = { X = velocityX; Y = velocityY }}
    parse input |> should equal point

[<Fact>]
let ``Position.Next should calculate new point position`` () =
    { X = 9; Y = 1 }.Next { X = 0; Y = 2 } |> should equal { X = 9; Y = 3 }

[<Fact>]
let ``Point.NextPosition should calculate new point position`` () =
    let point = { Position = { X = 9; Y = 1 }; Velocity = { X = 0; Y = 2 } }

    point.NextPosition |> should equal { Position = { X = 9; Y = 3}; Velocity = { X = 0; Y = 2 } }
