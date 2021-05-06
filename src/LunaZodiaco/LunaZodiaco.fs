// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  LunaZodiaco
// File:     LunaZodiaco.fs
//
//==============================================================================

/// Namespace containing all moon related libraries.
namespace RC.Moon

open System

/// Module holding functions for the calculation of moon phases and zodiacs.
module LunaZodiaco=

    /// Holds the information of the moon phase and the zodiac of a given day.
    type MoonDay = {Phase: MoonPhase.T; Zodiac: Zodiac.T} with

        /// Return the moon phase and zodiac of that day as strings.
        ///
        /// Returns:
        ///          A tuple `(phase, zodiac)` containing the moon phase as a string
        ///          and the zodiac as a string.
        member this.ToStrings () =
            (this.Phase.ToString (), this.Zodiac.ToString ())

        /// Return the moon phase and zodiac of that day as Unicode code-points.
        ///
        /// Returns:
        ///          A tuple `(phase, zodiac)` containing the moon phase and the
        ///          zodiac as Unicode code-points (to display emoticons usinDegg an
        ///          Unicode font).
        member this.ToUnicode () =
            (this.Phase.ToUnicode (), this.Zodiac.ToUnicode ())

        /// Return the moon phase and zodiac of that day as angle intervals of
        /// ecliptic longitude and elongation (between the moon and the sun).
        ///
        /// Returns:
        ///          A tuple `(phase, zodiac)` containing the moon phase and the
        ///          zodiac as angle intervals of ecliptic longitude and
        ///          elongation (between the moon and the sun).
        member this.ToIntervals () =
            (this.Phase.Interval (), this.Zodiac.Interval ())

    /// Return the moon phase and zodiac of that day as strings.
    ///
    /// Params:
    ///         `moonDay` The `MoonDay` instance to get the strings of.
    ///
    /// Returns:
    ///          A tuple `(phase, zodiac)` containing the moon phase as a string
    ///          and the zodiac as a string.
    let toStrings (moonDay: MoonDay) =
        moonDay.ToStrings ()

    /// Return the moon phase and zodiac of that day as Unicode code-points.
    ///
    /// Params:
    ///         `moonDay` The `MoonDay` instance to use.
    ///
    /// Returns:
    ///          A tuple `(phase, zodiac)` containing the moon phase and the
    ///          zodiac as Unicode code-points (to display emoticons usinDegg an
    ///          Unicode font).
    let toUnicode (moonDay: MoonDay) =
        moonDay.ToUnicode ()

    /// Return the moon phase and zodiac of that day as angle intervals of
    /// ecliptic longitude and elongation (between the moon and the sun).
    ///
    /// Params:
    ///         `moonDay` The `MoonDay` instance to use.
    ///
    /// Returns:
    ///          A tuple `(phase, zodiac)` containing the moon phase and the
    ///          zodiac as angle intervals of ecliptic longitude and
    ///          elongation (between the moon and the sun).
    let toIntervals (moonDay: MoonDay) =
        moonDay.ToIntervals ()

    /// Convert a `DateTime`date to a Julian day number - exact for days after
    /// 1582 - the gregorian calendar reform and between 0 and 1582.
    /// To Julian day number: + 2415018.5
    /// To special Julian day number: - 2451543.5
    let inline internal toSpecialJulianDayNumber (date: DateTime) =
        date.ToOADate() - 36525.

    /// Normalize angle to be between 0 and 360, including 0 but excluding 360.
    let inline internal normalize (angle: float<deg>) =
         angle - floor ((float angle) / 360.) * 360.<deg>

    let inline internal sinDeg (angle: float<deg>) =
        sin (angle * Math.PI / 180.<deg>)

    let inline internal cosDeg (angle: float<deg>) =
        cos (angle * Math.PI / 180.<deg>)

    let radToDeg = 180.<deg> / Math.PI

    /// Formula from /Computing planetary positions - a tutorial with worked examples/
    /// http://stjarnhimlen.se/comp/tutorial.html#7
    let internal computeLongitudeJulian julian =
        /// Long ascending node.
        let N = 125.1228<deg> - 0.0529538083<deg> * julian |> normalize
        /// Argument of perigee.
        let w = 318.0634<deg> + 0.1643573223<deg>  * julian |> normalize
        /// Mean anomaly.
        let M = 115.3654<deg> + 13.0649929509<deg> * julian |> normalize
        /// Inclination.
        let i = 5.1454<deg>
        /// Mean distance.
        let a =  60.2666
        /// Eccentricity.
        let e = 0.054900

        /// Iterative computation of eccentric anomaly.
        let E0 =
            M  + radToDeg * e * sinDeg M * (1. + e * cosDeg M) |> normalize

        let rec ecc last =
            let now = last -
                        (last - radToDeg * e * sinDeg last - M) /
                        (1. - e * cosDeg last ) |> normalize
            match abs (now - last) with
            | diff when diff <  0.002<deg> -> now
            | _ -> ecc now

        let E = ecc E0

        /// X and Y coordinates
        let x = a * (cosDeg E - e)
        let y = a * sqrt (1. - e ** 2.) * sinDeg E

        /// Distance in earth radii.
        let r = sqrt( x ** 2. + y ** 2. )
        /// True anomaly
        let v = radToDeg * atan2 y x |> normalize

        /// some often used terms.
        let sinDegN = sinDeg N
        let cosDegN = cosDeg N
        let cosDegvw = cosDeg (v  + w )
        let sinDegvw = sinDeg (v  + w )
        let cosDegi = cosDeg i

        /// Ecliptic coordinates.
        let xeclip  =
            r  * ( cosDegN * cosDegvw - sinDegN  * sinDegvw * cosDegi )

        let yeclip  =
            r  * ( sinDegN * cosDegvw + cosDegN  * sinDegvw * cosDegi )

        /// Ecliptic longitude.
        radToDeg * atan2 yeclip xeclip |> normalize

    /// Compute the ecliptic longitude of the moon at the given date.
    /// Accurate to about 2 degrees, so not good enough for serious calculations,
    /// but to get the zodiacs - it changes over the day anyway.
    ///
    /// Params:
    ///         `date` The `DateTime` object of the day to calculate the
    ///         longitude of.
    ///
    /// Returns:
    ///         The ecliptic longitude of the moon at the given date (more or
    ///         less).
    let computeLongitude = toSpecialJulianDayNumber >> computeLongitudeJulian

    /// Conway's formula for the moon age (number of days after new moon).
    /// Only works for dates in the 20th and 21st century!
    ///
    /// Params:
    ///         `date` The day to calculate the moon phase of.
    ///
    /// Returns:
    ///          The moon phase of the given day as elongation angle.
    let computeMoonPhase (date: DateTime) =
        let year = date.Year
        let month = date.Month
        let day = date.Day

        let moonAge =
            match (year % 100) % 19 with
            | d when d > 9 -> d - 19
            | d -> d
            |> (fun d -> (d * 11) % 30 +
                                    (if month < 3 then month + 2 else month) + day)
            |> (fun d -> float d - if year < 2000 then 4. else 8.3 )
            |> (fun d -> (d + 0.5) % 30.)
            |> (fun d -> if d < 0. then d + 30. else d)

        match moonAge with
        | d when d < 1. -> MoonPhase.getInterval MoonPhase.NewMoon
        | d when d < 8. -> MoonPhase.getInterval MoonPhase.WaxingCrescent
        | d when d < 8.4 -> MoonPhase.getInterval MoonPhase.FirstQuarter
        | d when d < 14. -> MoonPhase.getInterval MoonPhase.WaxingGibbous
        | d when d < 16. -> MoonPhase.getInterval MoonPhase.FullMoon
        | d when d < 22. -> MoonPhase.getInterval MoonPhase.WaningGibbous
        | d when d < 23.5 -> MoonPhase.getInterval MoonPhase.LastQuarter
        | _ -> MoonPhase.getInterval MoonPhase.WaningCrescent

    /// Calculate the moon phase and moon zodiac of the given day.
    ///
    /// Params:
    ///         `date` The day to calculate the zodiac and moon phase.
    ///
    /// Returns:
    ///         The moon phase and moon zodiac of the given day.
    let getMoonDay date =
        let (elong, _) = computeMoonPhase date
        let ecLong = computeLongitude date
        let phase =
            match MoonPhase.fromAngle elong with
            | Some p -> p
            | None -> MoonPhase.FullMoon // should not happen, angles are normalized!
        let zodiac =
            match Zodiac.fromAngle ecLong with
            | Some z -> z
            | None -> Zodiac.Aries // should not happen, angles are normalized!

        { Phase = phase; Zodiac = zodiac }
