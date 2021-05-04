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

    let private logger = Log.create "LunaZodiaco"

    let private loggerFunc logFunc moduleName name no args =
         logFunc (
            Message.eventX "{module} '{test}' #{no}, generated '{args}'"
            >> Message.setField "module" moduleName
            >> Message.setField "test" name
            >> Message.setField "no" no
            >> Message.setField "args" args )

    let loggerFuncDeb moduleName name no args =
        loggerFunc logger.debugWithBP moduleName name no args

    let loggerFuncInfo moduleName name no args =
        loggerFunc logger.infoWithBP moduleName name no args

    let config = { FsCheckConfig.defaultConfig with
                        maxTest = 10000
                        endSize = 1000000 }

    let configList = { FsCheckConfig.defaultConfig with
                            maxTest = 15
                            endSize = 500 }

    let configFasterThan = { FsCheckConfig.defaultConfig with
                                    maxTest = 100
                                    endSize = 1000000 }

    // Tests ===================================================================
    [<Tests>]
    let tests =
      testList
        "LunaZodiaco"
         [
           testList
            "Reference Dates"
            [ // Test the wavelengths of all waves ==============================================

             testCase "moon phase and zodiac compared"
             <| fun () ->  test <@ true @>
            ]

        ]
