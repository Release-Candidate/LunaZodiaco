// SPDX-License-Identifier: MIT
// Copyright (C) 2021 Roland Csaszar
//
// Project:  LunaZodiacoApp
// File:     MoonCalendar.fs
//
//==============================================================================
/// The namespace of the IOS and Android LunaZodiaco app.
namespace LunaZodiacoApp

open Fabulous.XamarinForms
open Xamarin.Forms
open System

open RC.Moon

/// The page of the app showing the date cards with information about all 9 waves.
[<AutoOpen>]
module NineWavePage=

    /// <summary>
    /// Show a date picker to select the date to display.
    /// </summary>
    /// <param name="model">The MVU model.</param>
    /// <param name="dispatch">The message dispatch function</param>
    /// <param name="date">The date to display information about</param>
    /// <returns>The `Frame` containing the `DatePicker`.</returns>
    let dateSelector model dispatch date =
        View.Frame (
            backgroundColor = backgroundBrownLight,
            borderColor = backgroundBrownDark,
            hasShadow = true,
            padding = Thickness (5., 5.),
            content =
                View.DatePicker (
                    minimumDate = DateTime (1900, 1, 1),
                    maximumDate = DateTime (2100, 12, 31),
                    date = date,
                    format = localeFormat,
                    dateSelected = (fun args -> SetDate args.NewDate |> dispatch),
                    width = 105.0,
                    verticalOptions = LayoutOptions.Fill,
                    textColor = Style.foregroundLight,
                    backgroundColor = Style.backgroundNone,
                    fontSize = Style.normalFontSize,
                    horizontalOptions = LayoutOptions.EndAndExpand
                )
            )

    /// <summary>
    /// Format the moon's information of one day.
    /// </summary>
    /// <param name="date">The date instance to display the moon information of.</param>
    /// <returns></returns>
    let formatMoonDay date =
       let { LunaZodiaco.MoonDay.Zodiac = zodiac
             LunaZodiaco.MoonDay.Phase = phase} = LunaZodiaco.getMoonDay date
       [ View.Span ( text = (sprintf "%s" (MoonPhase.toUnicode phase)),
                        fontAttributes = FontAttributes.Bold,
                        fontSize = Style.moonInfoMoonSize,
                        textColor = Style.foregroundLight
         )
         View.Span ( text = (sprintf "%s\n\n" (MoonPhase.toString phase)),
                        fontAttributes = FontAttributes.Bold,
                        fontSize = Style.moonInfoTextSize,
                        textColor = Style.foregroundLight
         )
         View.Span ( text = (sprintf "%s" (Zodiac.toUnicode zodiac)),
                        fontAttributes = FontAttributes.Bold,
                        fontSize = Style.moonInfoZodiacSize,
                        textColor = Style.foregroundLight
         )
         View.Span ( text = (sprintf "%s\n" (Zodiac.toString zodiac)),
                        fontAttributes = FontAttributes.Bold,
                        fontSize = Style.moonInfoTextSize,
                        textColor = Style.foregroundLight
         )
         View.Span ( text = "\n",
                         fontAttributes = FontAttributes.Bold,
                         fontSize = Style.moonInfoTextSize,
                         textColor = Style.foregroundLight
         )]

    /// <summary>
    /// Format the information of all 9 waves of the current date.
    /// </summary>
    /// <param name="date">The date to display the waves information of.</param>
    /// <param name="dispatch">The message dispatch function</param>
    /// <returns>A list of `Span`, usable to display in a label.</returns>
    let formatWaveDayDescriptions date dispatch =
        View.FormattedString (formatMoonDay date)

    /// <summary>
    /// Display a `Frame` containing a button to switch to the graph page, a
    /// date picker to sect the day to display and information about all 9 waves.
    /// </summary>
    /// <param name="model">The MVU model.</param>
    /// <param name="dispatch">The message dispatch function</param>
    /// <param name="date">The date to display information about.</param>
    /// <returns>A `StackLayout` holding a button to switch to the graph page,
    /// the `DatePicker` to select the date to show information of and
    /// a `Label` with information about all nine waves.</returns>
    let tzolkinCard model dispatch date =
        View.StackLayout (
            horizontalOptions = LayoutOptions.Center,
            padding = Thickness 5.,
            children =
                [ View.Frame (
                      backgroundColor = backgroundBrown,
                      cornerRadius = 20.,
                      padding = Thickness (0., 10., 0., 10.),
                      hasShadow = true,
                      content =
                          View.StackLayout (
                              padding = Thickness 10.,
                              orientation = setHorizontalIfLandscape model.IsLandscape,
                              horizontalOptions = LayoutOptions.Center,
                              verticalOptions = LayoutOptions.Center,
                              backgroundColor = Style.backgroundBrown,
                              children =
                                  [ View.StackLayout (
                                      orientation = setVerticalIfLandscape model.IsLandscape,
                                      backgroundColor = backgroundBrown,
                                      padding = Thickness (5., 10., 10., 0.),
                                      children = [dateSelector model dispatch model.Date ]
                                    )

                                    //  separator model.IsLandscape model.IsDarkMode

                                    View.Label (
                                        formattedText =
                                            formatWaveDayDescriptions model.Date dispatch,
                                        lineBreakMode = LineBreakMode.WordWrap,
                                        horizontalOptions = LayoutOptions.Center) ]
                          )
                  ) ]
        )

    /// Display the date view page, the information about the nine waves at a
    /// date in a carousel view of consecutive days.
    ///
    /// <param name="model">The MVU model.</param>
    /// <param name="dispatch">The message dispatch function</param>
    /// <returns>A list containing the date view page.</returns>
    let moonCalendarPage model dispatch =
        [ View.CarouselView (
            peekAreaInsets = Thickness 20.,
            loop = true,
            position = 1,
            backgroundColor = Style.backgroundBrownDark,
            verticalOptions = LayoutOptions.Center,
            horizontalOptions = LayoutOptions.Center,
            positionChanged = (fun args -> dispatch <| CarouselChanged args),
            items =
                [ tzolkinCard model dispatch model.Date
                  tzolkinCard model dispatch model.Date
                  tzolkinCard model dispatch model.Date ]
            )
         ]
