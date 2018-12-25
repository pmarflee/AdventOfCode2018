namespace AdventOfCode2018.Core

module Day10 =
    open System.Text.RegularExpressions

    type Velocity = { X : int; Y : int }

    type Position = { X : int; Y : int } with
        member this.Next (velocity : Velocity) =
            { X = this.X + velocity.X; Y = this.Y + velocity.Y }

    type Point = { Position : Position; Velocity : Velocity } with
         member this.NextPosition = 
            { this with Position = this.Position.Next this.Velocity }

    let private _regex = new Regex("position=<\s*?(-?\d+),\s*(-?\d+)>\svelocity=<\s*(-?\d+),\s*(-?\d+)>")

    let parse input = 
        let m = _regex.Match(input)
        { Position = { X = int m.Groups.[1].Value ; Y = int m.Groups.[2].Value };
          Velocity = { X = int m.Groups.[3].Value; Y = int m.Groups.[4].Value } }