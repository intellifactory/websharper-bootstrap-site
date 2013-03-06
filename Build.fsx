#r "packages/FAKE.1.74.127.0/tools/FakeLib.dll"

open Fake

let Root = __SOURCE_DIRECTORY__
let ( +/ ) a b = System.IO.Path.Combine(a, b)

do
    let vars = System.Environment.GetEnvironmentVariables()
    let e = vars.GetEnumerator()
    while e.MoveNext() do
        tracefn "ENV %O -> %O" e.Key e.Value

let Projects =
    !+ (Root +/ "*" +/ "*.csproj")
    ++ (Root +/ "*" +/ "*.fsproj")
    |> Scan

Target "Build" <| fun () ->
    tracefn "Building"
    MSBuildRelease "" "Build" Projects
    |> ignore

Target "Clean" <| fun () ->
    tracefn "Cleaning"
    MSBuildRelease "" "Clean" Projects
    |> ignore

RunTargetOrDefault "Build"
