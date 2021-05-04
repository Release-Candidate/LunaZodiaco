// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  TestLunaZodiaco
// File:     Generics.fs
// Date:     5/4/2021 3:10:03 PM
//==============================================================================

namespace TestLunaZodiaco

open Expecto
open Swensen.Unquote
open System
open FsCheck
open Expecto.Logging

open RC.Moon.Generics

[<AutoOpen>]
module Generics=

    let private logger = Log.create "LunaZodiaco"

    let private loggerFunc logFunc moduleName name no args =
            logFunc (
                Message.eventX "{module} '{test}' #{no}, generated '{args}'"
                >> Message.setField "module" moduleName
                >> Message.setField "test" name
                >> Message.setField "no" no
                >> Message.setField "args" args )

    let loggerFuncDeb moduleName name no args =
        loggerFunc logger.debugWithBP moduleName name no args

    let loggerFuncInfo moduleName name no args =
        loggerFunc logger.infoWithBP moduleName name no args

    let config = { FsCheckConfig.defaultConfig with
                        maxTest = 10000
                        endSize = 1000000 }

    let configList = { FsCheckConfig.defaultConfig with
                            maxTest = 15
                            endSize = 500 }

    let configFasterThan = { FsCheckConfig.defaultConfig with
                                    maxTest = 100
                                    endSize = 1000000 }

    // Generic test functions ==================================================

    /// Only obtain valid ints from the random ones.
    let sanitizeInt modulo i = abs i % modulo

    /// Test the `toString` and `ToString` functions.
    let inline internal testToString fromI toString i =
        let instance = fromI i
        test <@ instance.ToString () = toString instance @>
        test <@ toString instance <> "" @>

    /// Test the `toUnicode` and `ToUnicode` functions.
    let inline internal testToUnicode fromI toUnicode i =
        let (instance: ^T) = fromI i
        let unicodeStr = (^T: ( member ToUnicode : unit -> string ) instance)
        test <@ unicodeStr = toUnicode instance @>
        test <@ toUnicode instance <> "" @>

    /// Test the `getInterval` and `Interval` functions.
    let inline internal testToInterval fromI getInterval i =
        let (instance: ^T) = fromI i
        let interval =
            (^T: ( member Interval : unit -> (float<deg> * float<deg>) ) instance)
        test <@ interval = getInterval instance @>

    /// Test the `fromInt` and `toInt` functions
    let inline internal testFromToInt fromI1 fromI2 i =
        let instance: ^T = fromI1 i
        let instance2: ^T =
            match fromI2 i with
            | Some t -> t
            | None ->
                test <@ "Should never happen" = "" @>
                instance

        let instance3: ^T =
            match (^T: (static member FromInt : int -> ^T option) i) with
            | Some t -> t
            | None ->
                test <@ "Should never happen" = "" @>
                instance

        test <@ instance = instance2 @>
        test <@ instance = instance3 @>
        let int1 = (^T: (member ToInt : unit -> int) instance)
        let int2 = (^T: (member ToInt : unit -> int) instance2)
        let int3 = (^T: (member ToInt : unit -> int) instance3)
        test <@ int1 = int2 @>
        test <@ int2 = i @>
        test <@ int3 = i @>

    /// Test the `fromAngle` and `FromAngle` functions.
    let inline internal testFromAngle fromI fromAngle i =
        let instance = fromI i
        let angle =
            (^T: ( member Interval : unit -> (float<deg> * float<deg>) ) instance)
            |> fun (s, e) -> Gen.choose ((int s), (int e - 1)) |> Gen.sample 0 1
            |> List.head
            |> float
            |> ( * ) 1.<deg>

        let instance1 =
            match (^T: (static member FromAngle : float<deg> -> ^T option) angle) with
            | Some t -> t
            | None ->
                test <@ "Should never happen" = "" @>
                instance

        let instance2 =
            match fromAngle angle with
            | Some t -> t
            | None ->
                test <@ "Should never happen" = "" @>
                instance

        test <@ instance1 = instance2 @>
        test <@ instance1 = instance @>
