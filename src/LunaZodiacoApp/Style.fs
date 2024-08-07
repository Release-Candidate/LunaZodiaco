﻿// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  LunaZodiacoApp
// File:     Style.fs
//
//==============================================================================

/// The namespace of the IOS and Android LunaZodiaco app.
namespace LunaZodiacoApp

open Fabulous.XamarinForms
open Xamarin.Forms


/// Module holding colors, font sizes and other style related constants and functions.
[<AutoOpen>]
module Style =

    // Global ==================================================================

    let normalFontSize = FontSize.fromNamedSize NamedSize.Medium

    let backgroundBrownDark = Color.FromHex "#BFAB91"

    let backgroundBrownDarkSK = SkiaSharp.SKColor.Parse "#BFAB91"

    let backgroundBrown = Color.FromHex "#F2D8B8"

    let backgroundBrownSK = SkiaSharp.SKColor.Parse "#F2D8B8"

    let backgroundBrownLight = Color.FromHex "#F6DCBC"

    let backgroundBrownLightSK = SkiaSharp.SKColor.Parse "#F6DCBC"

    let backgroundNone = Color.Default

    let backgroundLight = Color.Default

    let backgroundDark = Color.FromHex "#1F1B24"

    let foregroundLight = Color.Black

    let foregroundDark = Color.WhiteSmoke

    let accentDarkRed = Color.FromHex "#8B2A02"

    let accentDarkRedSK = SkiaSharp.SKColor.Parse "#8B2A02"

    let blackSK = SkiaSharp.SKColors.Black

    let linkColorBlue = Color.Blue

    let linkSymbol = "\U0001F517"

    let foregroundColor isDark =
        match isDark with
        | true -> foregroundDark
        | false -> foregroundLight

    let backgroundColor isDark =
        match isDark with
        | true -> backgroundDark
        | false -> backgroundLight


    /// <summary>
    /// Return `StackOrientation` depending on the device's orientation.
    /// </summary>
    /// <param name="isL">Is landscape orientation?</param>
    /// <returns>`Horizontal if landscape, vertical else.</returns>
    let setHorizontalIfLandscape isL =
        if isL then
            StackOrientation.Horizontal
        else
            StackOrientation.Vertical

    /// <summary>
    /// Return `StackOrientation` depending on the device's orientation.
    /// </summary>
    /// <param name="isL">Is landscape orientation?</param>
    /// <returns>`Vertical if landscape, horizontal else.</returns>
    let setVerticalIfLandscape isL =
        if isL then
            StackOrientation.Vertical
        else
            StackOrientation.Horizontal

    // Pages ===================================================================

    let tabBackgroundColor = backgroundBrownDark

    let tabForegroundColor = Color.Black


    /// <summary>
    /// Separator line.
    /// </summary>
    /// <param name="isL">Is landscape orientation?</param>
    /// <param name="isDark">Is dark mode enabled?</param>
    /// <returns>A `BoxView` instance to use as a horizontal or vertical separator.</returns>
    let separator isL isDark =
        match isL with
        | false ->
            View.BoxView (
                color = foregroundColor isDark,
                backgroundColor = foregroundColor isDark,
                height = 0.5,
                horizontalOptions = LayoutOptions.FillAndExpand
            )

        | true ->
            View.BoxView (
                color = foregroundColor isDark,
                backgroundColor = foregroundColor isDark,
                width = 0.5,
                verticalOptions = LayoutOptions.FillAndExpand
            )

    // 9 wave page =============================================================

    let moonInfoTextSize = FontSize.fromValue 25.

    let moonInfoMoonSize = FontSize.fromValue 100.

    let moonInfoZodiacSize = FontSize.fromValue 100.

    let moonInfoLineHeigth = 1.8
