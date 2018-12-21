namespace AdventOfCode2018.Core

module Day7 =
    open System
    open System.Text.RegularExpressions

    type private Step = { Letter : char; Dependents : Set<char>; DependsUpon : Set<char> }

    let private _lineRegex = new Regex("^Step\s([A-Z])\smust\sbe\sfinished\sbefore\sstep\s([A-Z])\scan\sbegin\.$")

    let calculatePart1 input =

       let parse line =
            let m = _lineRegex.Match(line)
            (m.Groups.[1].Value.[0], m.Groups.[2].Value.[0])

       let getStep letter dependent steps =
            match Map.tryFind letter steps with
            | Some(step) -> { step with Dependents = step.Dependents.Add dependent }
            | None -> { Letter = letter; 
                        Dependents = Set.singleton dependent; 
                        DependsUpon = Set.empty }

       let getDependent dependent letter steps =
            match Map.tryFind dependent steps with
            | Some(step) -> { step with DependsUpon = step.DependsUpon.Add letter }
            | None -> { Letter = dependent; 
                        Dependents = Set.empty; 
                        DependsUpon = Set.singleton letter }

       let createSteps steps line =
            let (letter, dependent) = parse line
            let step = getStep letter dependent steps
            let dependentStep = getDependent dependent letter steps
            steps |> Map.add letter step |> Map.add dependent dependentStep
            
       let steps = Array.fold createSteps Map.empty input

       let lettersWithNoDependents =
           steps
           |> Seq.where (fun kvp -> kvp.Value.DependsUpon.IsEmpty)
           |> Seq.map (fun kvp -> kvp.Key)
           |> Set.ofSeq

       let orderGenerator (remaining, (completed : Set<char>)) = 
           if Set.isEmpty remaining then None
           else
               let letter =
                   remaining
                   |> Seq.where (fun l -> 
                                    let step' = Map.find l steps
                                    step'.DependsUpon |> Set.forall completed.Contains) 
                   |> Set.ofSeq
                   |> Set.minElement
               let step = Map.find letter steps
               let remaining' = remaining 
                                |> Set.remove letter 
                                |> Set.union step.Dependents

               Some(letter, (remaining', Set.add letter completed))

       Seq.unfold orderGenerator (lettersWithNoDependents, Set.empty) |> Array.ofSeq |> String