module options
open System

type Style = Call | Put
  
let cnd x =
   let a1 =  0.31938153
   let a2 = -0.356563782
   let a3 =  1.781477937
   let a4 = -1.821255978
   let a5 =  1.330274429
   let l  = abs x
   let k  = 1.0 / (1.0 + 0.2316419 * l)
   let w  = (1.0-1.0 / sqrt(2.0 * Math.PI) * 
                exp(-l * l / 2.0) * (a1 * k+a2 * k * k+a3 * 
                    (pown k 3)+a4 * (pown k 4)+a5 * (pown k 5)))
   if x < 0.0 then 1.0 - w
   else w

let blackscholes style s x t r v =
    let d1=(log(s / x) + (r+v*v/2.0)*t)/(v*sqrt(t))
    let d2=d1-v*sqrt(t)
    match style with
    | Call -> s*cnd(d1)-x*exp(-r*t)*cnd(d2)
    | Put ->  x*exp(-r*t)*cnd(-d2)-s*cnd(-d1)

let sw = System.Diagnostics.Stopwatch.StartNew()
let forSconesold = Array.init 50000000  (fun i -> blackscholes Call 60.0 65.0 0.25 0.08 0.3)
sw.Stop()
let r1 = sw.ElapsedTicks
printfn "50M black scholes: %A" sw.Elapsed
Console.WriteLine("Output = {0}", blackscholes Call 60.0 65.0 0.25 0.08 0.3)
//let time = GetAverageTime 10 (fun ()-> Array.init 20000000  (fun i -> blackscholes Call 60.0 65.0 0.25 0.08 0.3) |> ignore)
//printfn "ave time for 10: %A" time
Console.ReadKey() |> ignore 
