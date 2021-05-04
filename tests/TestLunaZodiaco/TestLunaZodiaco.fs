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

    // List with reference dates and `MoonDay`.
    let referenceDates = [
        (DateTime (2021, 5, 11), { LunaZodiaco.MoonDay.Phase =  MoonPhase.NewMoon
                                   LunaZodiaco.MoonDay.Zodiac = Zodiac.Taurus } )

        (DateTime (2021, 6, 17), { LunaZodiaco.MoonDay.Phase =  MoonPhase.WaxingCrescent
                                   LunaZodiaco.MoonDay.Zodiac = Zodiac.Virgo } )

        (DateTime (2021, 5, 19), { LunaZodiaco.MoonDay.Phase =  MoonPhase.FirstQuarter
                                   LunaZodiaco.MoonDay.Zodiac = Zodiac.Leo } )

        (DateTime (2021, 6, 19), { LunaZodiaco.MoonDay.Phase =  MoonPhase.WaxingGibbous
                                   LunaZodiaco.MoonDay.Zodiac = Zodiac.Libra } )

        (DateTime (2021, 5, 26), { LunaZodiaco.MoonDay.Phase =  MoonPhase.FullMoon
                                   LunaZodiaco.MoonDay.Zodiac = Zodiac.Scorpius } )

        (DateTime (2021, 6, 20), { LunaZodiaco.MoonDay.Phase =  MoonPhase.WaningGibbous
                                   LunaZodiaco.MoonDay.Zodiac = Zodiac.Libra } )

        (DateTime (2021, 5, 4), { LunaZodiaco.MoonDay.Phase =  MoonPhase.LastQuarter
                                  LunaZodiaco.MoonDay.Zodiac = Zodiac.Aquarius } )

        (DateTime (2021, 6, 5), { LunaZodiaco.MoonDay.Phase =  MoonPhase.WaningCrescent
                                  LunaZodiaco.MoonDay.Zodiac = Zodiac.Aries } )

        (DateTime (2021, 5, 19), { LunaZodiaco.MoonDay.Phase =  MoonPhase.FirstQuarter
                                   LunaZodiaco.MoonDay.Zodiac = Zodiac.Leo } )

        (DateTime (2021, 6, 24), { LunaZodiaco.MoonDay.Phase =  MoonPhase.FullMoon
                                   LunaZodiaco.MoonDay.Zodiac = Zodiac.Sagittarius } )
        ]

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


             testPropertyWithConfig config "toUnicode"
             <| fun i j ->
                    let moonPhase = getMoonPhase <| TestMoonPhase.sanitizeInt i
                    let zodiac = getZodiac <| TestZodiac.sanitizeInt j
                    let moonDay = { LunaZodiaco.MoonDay.Phase = moonPhase
                                    LunaZodiaco.MoonDay.Zodiac = zodiac }
                    test <@ moonDay.ToUnicode () = LunaZodiaco.toUnicode moonDay @>
                    let phaseStr, zodiacStr = LunaZodiaco.toUnicode moonDay
                    test <@ phaseStr = MoonPhase.toUnicode moonDay.Phase @>
                    test <@ zodiacStr = Zodiac.toUnicode moonDay.Zodiac @>

             testPropertyWithConfig config "toIntervals"
             <| fun i j ->
                    let moonPhase = getMoonPhase <| TestMoonPhase.sanitizeInt i
                    let zodiac = getZodiac <| TestZodiac.sanitizeInt j
                    let moonDay = { LunaZodiaco.MoonDay.Phase = moonPhase
                                    LunaZodiaco.MoonDay.Zodiac = zodiac }
                    test <@ moonDay.ToIntervals () = LunaZodiaco.toIntervals moonDay @>
                    let phaseStr, zodiacStr = LunaZodiaco.toIntervals moonDay
                    test <@ phaseStr = MoonPhase.getInterval moonDay.Phase @>
                    test <@ zodiacStr = Zodiac.getInterval moonDay.Zodiac @>
            ]

           testList
            "Reference Dates"
            [ // Test against reference dates===================================

            testCase "Reference Dates"
            <| fun () ->
                referenceDates
                |> List.iter (fun (date, moonDay) ->
                        let result = LunaZodiaco.getMoonDay date
                        test <@ result.Zodiac = moonDay.Zodiac @> )
            ]
        ]
