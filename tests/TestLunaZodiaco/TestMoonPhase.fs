// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  TestLunaZodiaco
// File:     TestMoonPhase.fs
// Date:     5/4/2021 3:08:19 PM
//==============================================================================

namespace TestLunaZodiaco

open Expecto
open Swensen.Unquote
open System
open FsCheck
open Expecto.Logging

open RC.Moon

[<AutoOpen>]
module TestMoonPhase=

    /// Only obtain valid ints from the random ones.
    let sanitizeInt = Generics.sanitizeInt 8

    /// Unwrap the `MoonPhase`.
    let getMoonPhase i =
        match MoonPhase.fromInt i with
        | Some m -> m
        | None ->
            test <@ "Should never happen" = "" @>
            MoonPhase.T.FirstQuarter

    // Tests ===================================================================
    [<Tests>]
    let tests =
      testList
        "MoonPhase"
        [
          testList
            "Conversion functions"
            [ // Test conversion functions =====================================

                testPropertyWithConfig config "toStrings"
                <| fun i ->
                    Generics.testToString getMoonPhase MoonPhase.toString (sanitizeInt i)

                testPropertyWithConfig config "toUnicode"
                <| fun i ->
                    Generics.testToUnicode getMoonPhase MoonPhase.toUnicode (sanitizeInt i)

                testPropertyWithConfig config "getInterval"
                <| fun i ->
                    Generics.testToInterval getMoonPhase MoonPhase.getInterval (sanitizeInt i)

                testPropertyWithConfig config "FromInt and ToInt"
                <| fun i ->
                    Generics.testFromToInt getMoonPhase MoonPhase.fromInt (sanitizeInt i)

                testPropertyWithConfig config "FromAngle"
                <| fun i ->
                    Generics.testFromAngle getMoonPhase MoonPhase.fromAngle (sanitizeInt i)

            ]
        ]
