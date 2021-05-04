// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  LunaZodiaco
// File:     LunaZodiaco.fs
//
//==============================================================================

/// Namespace containing all moon related libraries.
namespace RC.Moon

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
