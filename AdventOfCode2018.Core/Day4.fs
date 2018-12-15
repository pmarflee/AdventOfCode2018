namespace AdventOfCode2018.Core

open System

module Day4 =
    open System.Text.RegularExpressions

    let private _regexRecord = new Regex("^\[(\d{4})-(\d{2})-(\d{2})\s(\d{2}):(\d{2})\]\s(.+)$")
    let private _regexBeginsShift = new Regex("^Guard\s#([0-9]+)\sbegins\sshift$")
    let private _regexFallsAsleep = new Regex("^falls asleep$")
    let private _regexWakesUp = new Regex("^wakes up$")

    type EventType = BeginsShift | FallsAsleep | WakesUp

    type Event = { Date : DateTime; GuardNumber : int; Type : EventType }

    type Guard = { Number : int; 
                   TotalMinutesAsleep : int ; 
                   MinutesWhenAsleep : Map<int, int> } with 
                   static member get event state =
                           match Map.tryFind event.GuardNumber state.Guards with
                           | Some(guard) -> guard
                           | None -> { Number = event.GuardNumber; 
                                       TotalMinutesAsleep = 0; 
                                       MinutesWhenAsleep = Map.empty }
                   static member update (current : Event) (state : State) guard =
                           match current.Type with
                           | BeginsShift | FallsAsleep -> guard
                           | WakesUp -> 
                                let dateFellAsleep = state.Event.Value.Date
                                let timeAsleep = current.Date - dateFellAsleep
                                let totalMinutesAsleep = int timeAsleep.TotalMinutes
                                let recordMinutesWhenAsleep minutesWhenAsleep minute =
                                    let total = match Map.tryFind minute minutesWhenAsleep with
                                                | Some(total') -> total' + 1
                                                | None -> 1
                                    Map.add minute total minutesWhenAsleep
                                let minutes = seq {
                                    for m in 0..totalMinutesAsleep - 1 do
                                        yield dateFellAsleep.AddMinutes(float m).Minute
                                } 
                                let minutesWhenAsleep = 
                                    Seq.fold recordMinutesWhenAsleep guard.MinutesWhenAsleep minutes
                                { guard with 
                                        TotalMinutesAsleep = guard.TotalMinutesAsleep + totalMinutesAsleep;
                                        MinutesWhenAsleep = minutesWhenAsleep }

    and State = { Guards : Map<int, Guard>; Event : Event option }

    let parseEvent line state = 
        let m = _regexRecord.Match(line)
        let item (i : int) = int m.Groups.[i].Value
        let date = new DateTime(item 1, item 2, item 3, item 4, item 5, 0)
        let eventString = m.Groups.[6].Value
        let (eventType, guard) =
            let matchBeginsShift = _regexBeginsShift.Match(eventString)
            if matchBeginsShift.Success then 
                (BeginsShift, int matchBeginsShift.Groups.[1].Value)
            elif _regexFallsAsleep.IsMatch(eventString) then
                (FallsAsleep, state.Event.Value.GuardNumber)
            elif _regexWakesUp.IsMatch(eventString) then
                (WakesUp, state.Event.Value.GuardNumber)
            else
                failwith "Invalid state"
        { Date = date; GuardNumber = guard; Type = eventType }
    
    let calculatePart1 input =
        Array.sortInPlace input
        let folder state line =
            let event = parseEvent line state
            let guard = Guard.get event state |> Guard.update event state
            let guards = Map.add event.GuardNumber guard state.Guards
            { Guards = guards; Event = Some(event) }

        let guards = (Array.fold folder { Guards = Map.empty; Event = None } input).Guards
        let guard = guards |> Map.toSeq |> Seq.maxBy (fun (_, g) -> g.TotalMinutesAsleep) |> snd
        let minute = guard.MinutesWhenAsleep |> Map.toSeq |> Seq.maxBy snd |> fst

        guard.Number * minute

