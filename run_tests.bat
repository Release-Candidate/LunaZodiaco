:: SPDX-License-Identifier: MIT
:: Copyright (C) 2021 Roland Csaszar
::
:: Project:  LunaZodiaco
:: File:     run_tests.bat
:: Date:     25.Apr.2021
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

dotnet fake run build.fsx target publish
.\bin\LunaZodiaco.exe --summary --nunit-summary .\test_results\nresult.xml --junit-summary .\test_results\jresult.xml
