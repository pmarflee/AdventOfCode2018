namespace AdventOfCode2018.Core

module Parser =

   let splitChars = [|' ';'\t'|]  

   let splitLines (input : string) = input.Split "\r\n"

   let parse (input : string) wordParser splitChars =  
        input
            |> splitLines
            |> Array.map (fun line -> line.Split splitChars |> Array.map wordParser)

   let parseWords splitChars input = parse input id splitChars
   
   let parseNumbers splitChars input = parse input int splitChars

   let asSingleColumn (input : 'a [][]) = input |> Array.map (fun line -> line.[0])

