This project is intended for QxWebDriver-NET development only.

The Wisej.Qooxdoo.WebDriver.Debug runs in Debug configuration only.
It uses a DEBUGJS conditional compiler constant and relies on the web application to load all the scripts. Do not use this project with sites where you can't control the loading of the scripts.

To make the Javascript debugger stop:
1) use "debugger;" in the script to debug
2) insert a breakpoint in or C# code so you can proceed to step 4)
3) debug your tests until Visual Studio hits the breakpoint and pauses
3) go to the browser and open the developer tools

This feature is supported on Firefox and Edge but no on Chrome or Opera.