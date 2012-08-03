module PerformanceTesting
    open System
    open System.Diagnostics

    let Time func =
        let stopwatch = new Stopwatch()
        stopwatch.Start()
        func()
        stopwatch.Stop()
        stopwatch.Elapsed.TotalMilliseconds
 
    let GetAverageTime timesToRun func =
        Seq.initInfinite (fun _ -> (Time func))
        |> Seq.take timesToRun
        |> Seq.average
 
    let TimeOperation timesToRun =
        GC.Collect()
        GetAverageTime timesToRun
 
    let TimeOperations funcsWithName =
        let randomizer = new Random(int DateTime.Now.Ticks)
        funcsWithName
        |> Seq.sortBy (fun _ -> randomizer.Next())
        |> Seq.map (fun (name, func) -> name, (TimeOperation 100000 func))
 
    let TimeOperationsAFewTimes funcsWithName =
        Seq.initInfinite (fun _ -> (TimeOperations funcsWithName))
        |> Seq.take 50
        |> Seq.concat
        |> Seq.groupBy fst
        |> Seq.map (fun (name, individualResults) -> name, (individualResults |> Seq.map snd |> Seq.average))