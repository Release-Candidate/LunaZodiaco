// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  LunaZodiaco
// File:     Zodiac.fs
// Date:     5/4/2021 12:56:20 PM
//==============================================================================

/// Namespace containing all moon related libraries.
namespace RC.Moon

/// Holds all types and function to use with `Zodiac` types.
module Zodiac=

    /// The Unicode code points of the 12 zodiacs.
    let zodiacsUnicode = [|
        "♈"     // U+2648
        "♉"     // U+2649
        "♊"     // U+264A
        "♋"     // U+264B
        "♌"     // U+264C
        "♍"     // U+264D
        "♎"     // U+264E
        "♏"     // U+264F
        "♐"     // U+2650
        "♑"     // U+2651
        "♒"     // U+2652
        "♓"     // U+2653
    |]

    /// The Spanish names of the 12 zodiacs.
    let zodiacsStrings = [|
        "Aries"
        "Tauro"
        "Géminis"
        "Cáncer"
        "Leo"
        "Virgo"
        "Libra"
        "Escorpio"
        "Sagitario"
        "Capricornio"
        "Acuario"
        "Piscis" |]

    /// The interval [start, end) ( `start <= i < end` ) of the Moon's ecliptic
    /// longitude in each zodiac.
    let zodiacInterval = [|
        (0.<deg>, 30.<deg>)
        (30.<deg>, 60.<deg>)
        (60.<deg>, 90.<deg>)
        (90.<deg>, 120.<deg>)
        (120.<deg>, 150.<deg>)
        (150.<deg>, 180.<deg>)
        (180.<deg>, 210.<deg>)
        (210.<deg>, 240.<deg>)
        (240.<deg>, 270.<deg>)
        (270.<deg>, 300.<deg>)
        (300.<deg>, 330.<deg>)
        (330.<deg>, 360.<deg>) |]

    /// The 12 zodiacs.
    type T =
        | Aries
        | Taurus
        | Gemini
        | Cancer
        | Leo
        | Virgo
        | Libra
        | Scorpius
        | Sagittarius
        | Capricorn
        | Aquarius
        | Pisces
    with

        /// Return the index of the given zodiac to use in the interval, Unicode and
        /// string arrays.
        ///
        /// Returns:
        ///         An index to use in the `zodiacsUnicode`, `zodiacsStrings` and
        ///         `zodiacInterval` arrays.
        member this.ToInt () =
            match this with
            | Aries -> 0
            | Taurus -> 1
            | Gemini -> 2
            | Cancer -> 3
            | Leo -> 4
            | Virgo -> 5
            | Libra -> 6
            | Scorpius -> 7
            | Sagittarius -> 8
            | Capricorn -> 9
            | Aquarius -> 10
            | Pisces -> 11

        /// Return the Spanish name of the zodiac as string.
        ///
        /// Returns:
        ///          The Spanish name of the zodiac.
        override this.ToString () =
            Generics.toString zodiacsStrings this

        /// Return the Unicode code point of the zodiac.
        ///
        /// Returns:
        ///         The Unicode code point of the zodiac.
        member this.ToUnicode () =
            Generics.toString zodiacsUnicode this

        /// Return the interval of ecliptic longitude the zodiac is located in.
        ///
        /// Returns:
        ///         The interval of ecliptic longitude the zodiac is located in.
        member this.Interval () =
            Generics.getInterval zodiacInterval this

        /// Return a `Zodiac` option from the given index `i`.
        /// That's the inverse of the `ToInt` function.
        ///
        /// Params:
        ///         `i`The index to convert to a `Zodiac`.
        ///
        /// Returns:
        ///           The `Zodiac` if a zodiac with the given index exists,
        ///           `None` else.
        static member FromInt i =
            match i with
            | 0 -> Some Aries
            | 1 -> Some Taurus
            | 2 -> Some Gemini
            | 3 -> Some Cancer
            | 4 -> Some Leo
            | 5 -> Some Virgo
            | 6 -> Some Libra
            | 7 -> Some Scorpius
            | 8 -> Some Sagittarius
            | 9 -> Some Capricorn
            | 10 -> Some Aquarius
            | 11 -> Some Pisces
            | _ -> None

        /// Return the `Zodiac` instance for the given ecliptic longitude angle
        /// `eclong`.
        ///
        /// Params:
        ///         `eclong` The ecliptic longitude angle to get the zodiac of.
        ///
        /// Returns:
        ///         The `Zodiac` instance for the given ecliptic longitude angle.
        static member FromAngle (eclong: float<deg>) =
            Generics.fromAngle zodiacInterval T.FromInt eclong

    /// Return the Spanish name of the zodiac as a string.
    ///
    /// Params:
    ///         `zodiac` The zodiac to get the name as string of.
    ///
    /// Returns:
    ///         The Spanish name of the zodiac as string.
    let toString zodiac =
        zodiac.ToString ()

    /// Return the Unicode code point of the zodiac.
    ///
    /// Params:
    ///         ´zodiac` The `Zodiac` instance to use.
    ///
    /// Returns:
    ///         The Unicode code point of the zodiac.
    let toUnicode (zodiac: T) =
        zodiac.ToUnicode ()

    /// Return the interval of ecliptic longitude the zodiac is located in.
    ///
    /// Params:
    ///         ´zodiac` The `Zodiac` instance to use.
    ///
    /// Returns:
    ///         The interval of ecliptic longitude the zodiac is located in.
    let getInterval (zodiac: T) =
        zodiac.Interval ()

    /// Return a `Zodiac` option from the given index `i`.
    /// That's the inverse of the `ToInt` function.
    ///
    /// Params:
    ///         `i`The index to convert to a `Zodiac`.
    ///
    /// Returns:
    ///           The `Zodiac` if a zodiac with the given index exists,
    ///           `None` else.
    let fromInt i =
        T.FromInt i

    /// Return the `Zodiac` instance for the given ecliptic longitude angle
    /// `eclong`.
    ///
    /// Params:
    ///         `eclong` The ecliptic longitude angle to get the zodiac of.
    ///
    /// Returns:
    ///         The `Zodiac` instance for the given ecliptic longitude angle.
    let fromAngle eclong =
        T.FromAngle eclong
