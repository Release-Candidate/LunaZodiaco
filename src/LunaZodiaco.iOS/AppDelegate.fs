// SPDX-License-Identifier: MIT
// Copyright 2018 Fabulous contributors.
// Copyright 2021 Roland Csaszar
//
// Project:  LunaZodiaco.iOS
// File:     AppDelegate.fs
//
//==============================================================================

namespace LunaZodiaco.IOS

open System
open UIKit
open Foundation
open Xamarin.Forms
open Xamarin.Forms.Platform.iOS

open LunaZodiacoApp

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit FormsApplicationDelegate ()

    override this.FinishedLaunching (app, options) =
        Forms.Init()
        let appcore = new LunaZodiacoApp.App ()
        this.LoadApplication (appcore)
        base.FinishedLaunching(app, options)

module Main =
    [<EntryPoint>]
    let main args =
        UIApplication.Main(args, null, "AppDelegate")
        0

