// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  TestLunaZodiaco
// File:     TestLunaZodiaco.fs
//
//==============================================================================


namespace TestLunaZodiaco

open Expecto
open Swensen.Unquote
open System
open FsCheck
open Expecto.Logging

open RC.Moon

[<AutoOpen>]
module TestLunaZodiaco=

    // Tests ===================================================================
    [<Tests>]
    let tests =
      testList
        "LunaZodiaco"
         [
           testList
            "Conversion functions"
            [ // Test conversion functions =====================================

             testPropertyWithConfig config "toStrings"
             <| fun i j ->
                    let moonPhase = getMoonPhase <| TestMoonPhase.sanitizeInt i
                    let zodiac = getZodiac <| TestZodiac.sanitizeInt j
                    let moonDay = { LunaZodiaco.MoonDay.Phase = moonPhase
                                    LunaZodiaco.MoonDay.Zodiac = zodiac }
                    test <@ moonDay.ToStrings () = LunaZodiaco.toStrings moonDay @>
                    let phaseStr, zodiacStr = LunaZodiaco.toStrings moonDay
                    test <@ phaseStr = MoonPhase.toString moonDay.Phase @>
                    test <@ zodiacStr = Zodiac.toString moonDay.Zodiac @>
            ]

        ]
