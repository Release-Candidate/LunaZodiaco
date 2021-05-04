// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  LunaZodiaco
// File:     MoonPhase.fs
// Date:     5/4/2021 1:17:43 PM
//==============================================================================

/// Namespace containing all moon related libraries.
namespace RC.Moon

/// Module that contains everything to use `MoonPhase` types.
module MoonPhase=

    /// The Unicode code points of the moon phases.
    let moonPhaseUnicode = [|
        "🌑️"     // U+1F311
        "🌒️"     // U+1F312
        "🌓️"     // U+1F313
        "🌔️"     // U+1F314
        "🌕️"     // U+1F315
        "🌖️"     // U+1F316
        "🌗️"     // U+1F317
        "🌘️"     // U+1F318
    |]

    /// The Spanish names of the moon phases.
    let moonPhaseStrings = [|
        "Luna nueva"
        "Luna creciente"
        "Cuarto creciente"
        "Creciente gibosa"
        "Luna Llena"
        "Menguante gibosa"
        "Cuarto menguante"
        "Luna menguante" |]

    /// The interval [start, end) (`start <= i < end`) of Moon Elongation
    /// of the moon phases.
    let moonPhaseInterval = [|
        (0.<deg>, 45.<deg>)
        (45.<deg>, 90.<deg>)
        (90.<deg>, 135.<deg>)
        (135.<deg>, 180.<deg>)
        (180.<deg>, 225.<deg>)
        (225.<deg>, 270.<deg>)
        (270.<deg>, 315.<deg>)
        (315.<deg>, 360.<deg>) |]

    /// The 8 phases of the moon.
    type T =
        | NewMoon
        | WaxingCrescent
        | FirstQuarter
        | WaxingGibbous
        | FullMoon
        | WaningGibbous
        | LastQuarter
        | WaningCrescent
    with

        /// Return the index of the moon phase to use with the arrays
        /// `moonPhaseUnicode`, `moonPhaseStrings` and `moonPhaseInterval`.
        ///
        /// Returns:
        ///            An int index to use with the arrays `moonPhaseUnicode`,
        ///            `moonPhaseStrings` and `moonPhaseInterval`.
        member this.ToInt () =
            match this with
            | NewMoon -> 0
            | WaxingCrescent -> 1
            | FirstQuarter -> 2
            | WaxingGibbous -> 3
            | FullMoon -> 4
            | WaningGibbous -> 5
            | LastQuarter -> 6
            | WaningCrescent -> 7

        /// Return the Spanish name of the moon phase as string.
        ///
        /// Returns:
        ///         The Spanish name of the moon phase as string.
        override this.ToString () =
            Generics.toString moonPhaseStrings this

        /// Return the Unicode code point of the moon phase.
        ///
        /// Returns:
        ///         The Unicode code point of the moon phase.
        member this.ToUnicode () =
            Generics.toString moonPhaseUnicode this

        /// Return the Moon elongation interval of the moon phase.
        ///
        /// Returns:
        ///         The Moon elongation interval of the moon phase.
        member this.Interval () =
            Generics.getInterval moonPhaseInterval this

        /// Return a `MooonPhase` option from the given index `i`.
        /// That's the inverse of the `ToInt` function.
        ///
        /// Params:
        ///         `i`The index to convert to a `MoonPhase`.
        ///
        /// Returns:
        ///           The `MoonPhase` if a moon phase with the given index exists,
        ///           `None` else.
        static member FromInt i =
            match i with
            | 0-> Some NewMoon
            | 1 -> Some WaxingCrescent
            | 2-> Some FirstQuarter
            | 3 -> Some WaxingGibbous
            | 4-> Some FullMoon
            | 5-> Some WaningGibbous
            | 6-> Some LastQuarter
            | 7-> Some WaningCrescent
            | _ -> None

        /// Return the `MoonPhase` instance for the given elongation angle
        /// `elong`.
        ///
        /// Params:
        ///         `elong` The elongation angle to get the moon phase of.
        ///
        /// Returns:
        ///         The `MoonPhase` instance for the given elongation angle.
        static member FromAngle (elong: float<deg>) =
            Generics.fromAngle moonPhaseInterval T.FromInt elong

    /// Return the Spanish name of the moon phase as string.
    ///
    /// Params:
    ///         `moonPhase` The moon phase instance to use.
    ///
    /// Returns:
    ///         The Spanish name of the moon phase as string.
    let toString moonPhase =
        moonPhase.ToString ()

    /// Return the Unicode code point of the moon phase.
    ///
    /// Params:
    ///         `moonPhase` The moon phase instance to use.
    ///
    /// Returns:
    ///         The Unicode code point of the moon phase.
    let toUnicode (moonPhase: T) =
        moonPhase.ToUnicode ()

    /// Return the moon elongation interval of the moon phase.
    ///
    /// Params:
    ///         `moonPhase` The moon phase instance to use.
    ///
    /// Returns:
    ///         The Moon elongation interval of the moon phase.
    let getInterval (moonPhase: T) =
        moonPhase.Interval ()

    /// Return a `MoonPhase` option from the given index `i`.
    /// That's the inverse of the `ToInt` function.
    ///
    /// Params:
    ///         `i`The index to convert to a `MoonPhase`.
    ///
    /// Returns:
    ///           The `MoonPhase` if a moon phase with the given index exists,
    ///           `None` else.
    let fromInt i =
           T.FromInt i

    /// Return the `MoonPhase` instance for the given elongation angle
    /// `elong`.
    ///
    /// Params:
    ///         `elong` The elongation angle to get the moon phase of.
    ///
    /// Returns:
    ///         The `MoonPhase` instance for the given elongation angle.
    let fromAngle elong =
        T.FromAngle elong
