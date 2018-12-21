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

       let find letter = Map.find letter steps

       let orderGenerator (remaining, (completed : Set<char>)) = 
           if Set.isEmpty remaining then None
           else
               let step =
                   remaining
                   |> Seq.map find
                   |> Seq.where (fun step -> step.DependsUpon |> Set.forall completed.Contains) 
                   |> Seq.sortBy (fun step -> step.Letter)
                   |> Seq.head
               let remaining' = remaining |> Set.remove step.Letter |> Set.union step.Dependents

               Some(step.Letter, (remaining', Set.add step.Letter completed))

       Seq.unfold orderGenerator (lettersWithNoDependents, Set.empty) |> Array.ofSeq |> String