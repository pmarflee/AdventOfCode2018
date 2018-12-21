namespace AdventOfCode2018.Core

module Day7 =
    open System
    open System.Text.RegularExpressions

    type private Step = { Letter : char; Dependents : Set<char>; DependsUpon : Set<char> }

    let private _lineRegex = new Regex("^Step\s([A-Z])\smust\sbe\sfinished\sbefore\sstep\s([A-Z])\scan\sbegin\.$")

    let private parse line =
        let m = _lineRegex.Match(line)
        (m.Groups.[1].Value.[0], m.Groups.[2].Value.[0])

    let private getStep letter dependent steps =
        match Map.tryFind letter steps with
        | Some(step) -> { step with Dependents = step.Dependents.Add dependent }
        | None -> { Letter = letter; 
                    Dependents = Set.singleton dependent; 
                    DependsUpon = Set.empty }

    let private getDependent dependent letter steps =
        match Map.tryFind dependent steps with
        | Some(step) -> { step with DependsUpon = step.DependsUpon.Add letter }
        | None -> { Letter = dependent; 
                    Dependents = Set.empty; 
                    DependsUpon = Set.singleton letter }

    let private createSteps input = 
        let createSteps' steps line =
            let (letter, dependent) = parse line
            let step = getStep letter dependent steps
            let dependentStep = getDependent dependent letter steps
            steps |> Map.add letter step |> Map.add dependent dependentStep
        
        Array.fold createSteps' Map.empty input

    let private getLettersNoDependents (steps : Map<char, Step>) =
        steps
        |> Seq.where (fun (KeyValue (_, step)) -> step.DependsUpon.IsEmpty)
        |> Seq.map (fun (KeyValue (letter, _)) -> letter)
        |> Set.ofSeq

    let private find steps letter = Map.find letter steps

    let calculatePart1 input =
        let steps = createSteps input
        let generateOrders (remaining, (completed : Set<char>)) = 
            if Set.isEmpty remaining then None
            else
                let step =
                    remaining
                    |> Seq.map (find steps)
                    |> Seq.where (fun step -> step.DependsUpon |> Set.forall completed.Contains) 
                    |> Seq.sortBy (fun step -> step.Letter)
                    |> Seq.head
                let remaining' = remaining |> Set.remove step.Letter |> Set.union step.Dependents

                Some(step.Letter, (remaining', Set.add step.Letter completed))

        Seq.unfold generateOrders (getLettersNoDependents steps, Set.empty) |> Array.ofSeq |> String

    type private WorkerState = Idle | Busy of Step * int

    type private Worker = { Number : int;
                            State : WorkerState } with
                            member this.TryGetCompleted time =
                                match this.State with
                                | Idle -> None
                                | Busy(step, completeAt) -> 
                                    if completeAt = time then Some(this.Number, step) else None
                            member this.IsWorkingOnStep letter =
                                match this.State with
                                | Busy(step, _) when step.Letter = letter -> true
                                | _ -> false

    type private State = { Remaining : Set<char>;
                           Completed : Set<char>;
                           Workers : Map<int, Worker>;
                           Time : int }

    let calculatePart2 numberOfWorkers minStepDuration input =
        let steps = createSteps input
        let scores = [ 'A'..'Z' ] 
                     |> List.mapi (fun i c -> (c, i + 1 + minStepDuration))
                     |> Map.ofList
        let score letter = Map.find letter scores
        let rec calculate state =
            // printfn "Time: %i" state.Time
            let workersCompleted = 
                state.Workers
                |> Seq.choose (fun (KeyValue (_, worker)) -> worker.TryGetCompleted state.Time)
                |> Seq.toList
            (* for (number, step) in workersCompleted do
                printfn "Worker %i completed step %c" number step.Letter *)
            let completed = workersCompleted 
                            |> List.map (fun (_, step) -> step.Letter)
                            |> Set.ofList
                            |> Set.union state.Completed
            let workers = workersCompleted
                          |> List.fold (fun workers' (i, _) -> 
                                        Map.add i { Number = i; State = Idle } workers') state.Workers
            let remaining = workersCompleted
                            |> List.collect (fun (_, step) -> List.ofSeq step.Dependents)
                            |> List.where (fun c -> 
                                            let s = find steps c
                                            (not <| Set.contains c completed) && 
                                            (Set.forall (fun c1 -> Set.contains c1 completed) s.DependsUpon) &&
                                            not <| Map.exists (fun _ (w : Worker) -> w.IsWorkingOnStep c) workers)
                            |> Set.ofList
                            |> Set.union state.Remaining
            if remaining.IsEmpty 
                && workers |> Map.forall (fun _ worker -> worker.State = Idle) then
                state.Time
            else
                let allocateWork (workers', remaining') i worker =
                    match worker.State with
                        | Idle when not <| Set.isEmpty remaining' -> 
                            let step = find steps (Set.minElement remaining')
                            let completionTime = state.Time + score step.Letter
                            let worker' = { worker with State = Busy(step, completionTime) }
                            // printfn "Allocating step %c to worker %i with completion time of %i" step.Letter worker.Number completionTime
                            (Map.add i worker' workers', Set.remove step.Letter remaining')
                        | _ -> (workers', remaining')

                let (workers', remaining') = Map.fold allocateWork (workers, remaining) workers

                calculate { Remaining = remaining';
                            Completed = completed; 
                            Workers = workers'; 
                            Time = state.Time + 1 }
         
        calculate { Remaining = getLettersNoDependents steps;
                    Completed = Set.empty;
                    Workers = List.init numberOfWorkers 
                                        (fun i -> i, { Number = i; State = Idle }) 
                              |> Map.ofList;
                    Time = 0 }