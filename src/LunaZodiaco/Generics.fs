// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  LunaZodiaco
// File:     Generics.fs
// Date:     5/4/2021 12:57:29 PM
//==============================================================================

/// Namespace containing all moon related libraries.
namespace RC.Moon

/// Holds functions to use with `Zodiac` and `MoonPhase` types.
[<AutoOpen>]
module Generics=

    /// Angle in degrees.
    [<Measure>]
    type deg

    /// Return the name or Unicode string of the `Zodiac` or `MoonPhase` `instance`.
    let inline internal toString
        (stringArr: string array)
        (instance: ^T)
        =
        stringArr.[(^T: (member ToInt : unit -> int) instance)]

    /// Return the longitude/elongation interval of the `Zodiac` or `MoonPhase` `instance`.
    let inline internal getInterval
        (intervalArr: ^a array)
        (instance: ^T)
        =
        intervalArr.[(^T: (member ToInt : unit -> int) instance)]

    /// Return the moon phase or zodiac of the interval the angle is in.
    let inline internal fromAngle angleArr fromInt angle =
        let rec find list counter =
            match list with
            | (s, e) :: rest -> if s <= angle && angle < e then
                                    counter
                                else
                                    find rest (counter + 1)
            | [] -> -1

        let idx = find (List.ofArray angleArr) 0
        fromInt idx

