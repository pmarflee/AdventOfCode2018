namespace AdventOfCode2018.Core

module Day7 =
    open System
    open System.Text.RegularExpressions

    type private Step = { Letter : char; Dependents : char list; DependsUpon : char list }

    let private _lineRegex = new Regex("^Step\s([A-Z])\smust\sbe\sfinished\sbefore\sstep\s([A-Z])\scan\sbegin\.$")

    let calculatePart1 input =

       let parse line =
            let m = _lineRegex.Match(line)
            (m.Groups.[1].Value.[0], m.Groups.[2].Value.[0])

       let getStep letter dependent steps =
            match Map.tryFind letter steps with
            | Some(step) -> { step with Dependents = dependent :: step.Dependents }
            | None -> { Letter = letter; Dependents = [ dependent ]; DependsUpon = [] }

       let getDependent dependent letter steps =
            match Map.tryFind dependent steps with
            | Some(step) -> { step with DependsUpon = letter :: step.DependsUpon }
            | None -> { Letter = dependent; Dependents = []; DependsUpon = [ letter ] }

       let createSteps steps line =
            let (letter, dependent) = parse line
            let step = getStep letter dependent steps
            let dependentStep = getDependent dependent letter steps
            steps |> Map.add letter step |> Map.add dependent dependentStep
            
       let steps = Array.fold createSteps Map.empty input

       let lettersWithNoDependents =
           let noDependents key _ =
                not <| Map.exists (fun _ step -> List.contains key step.Dependents) steps
           steps
           |> Map.filter noDependents
           |> Map.toSeq
           |> Seq.map fst
           |> Seq.toList

       let orderGenerator (remaining, (completed : Set<char>)) = 
           if Set.isEmpty remaining then None
           else
               let letter =
                   remaining
                   |> Seq.where (fun l -> 
                                    let step' = Map.find l steps
                                    step'.DependsUpon |> List.forall completed.Contains) 
                   |> Set.ofSeq
                   |> Set.minElement
               let step = Map.find letter steps
               let remaining' = remaining 
                                |> Set.remove letter 
                                |> Set.union (Set.ofList step.Dependents)

               Some(letter, (remaining', Set.add letter completed))

       Seq.unfold orderGenerator (Set.ofList lettersWithNoDependents, Set.empty)
       |> Array.ofSeq 
       |> String