// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  TestLunaZodiaco
// File:     TestZodiac.fs
// Date:     5/4/2021 3:09:30 PM
//==============================================================================

namespace TestLunaZodiaco

open Expecto
open Swensen.Unquote
open System
open FsCheck
open Expecto.Logging

open RC.Moon

[<AutoOpen>]
module TestZodiac=

    /// Only obtain valid ints from the random ones.
    let sanitizeInt = Generics.sanitizeInt 12

    /// Unwrap the `Zodiac`.
    let getZodiac i =
        match Zodiac.fromInt i with
        | Some z -> z
        | None ->
                test <@ "Should never happen" = "" @>
                Zodiac.T.Aquarius

    // Tests ===================================================================
    [<Tests>]
    let tests =
      testList
        "Zodiac"
        [
          testList
            "Conversion functions"
            [ // Test conversion functions =====================================

                testPropertyWithConfig config "toStrings"
                <| fun i ->
                    Generics.testToString getZodiac Zodiac.toString (sanitizeInt i)

                testPropertyWithConfig config "toUnicode"
                <| fun i ->
                    Generics.testToUnicode getZodiac Zodiac.toUnicode (sanitizeInt i)

                testPropertyWithConfig config "getInterval"
                <| fun i ->
                    Generics.testToInterval getZodiac Zodiac.getInterval (sanitizeInt i)

                testPropertyWithConfig config "FromInt and ToInt"
                <| fun i ->
                    Generics.testFromToInt getZodiac Zodiac.fromInt (sanitizeInt i)

                testCase "let FromInt fail"
                <| fun () ->
                    test <@ Zodiac.fromInt 65 = None @>

                testPropertyWithConfig config "FromAngle"
                <| fun i ->
                    Generics.testFromAngle getZodiac Zodiac.fromAngle (sanitizeInt i)
            ]
        ]
