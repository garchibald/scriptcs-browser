scriptcs-browser
================

# Browser Script Pack

## What is it?
A ScriptCs script pack to interact with browser

## Getting started with Browser using the pack

* Copy and paste the following block into a file names start.csx

```csharp
Require<Browser>();

// Open web page in all listed browsers
Browser.Open(BrowserType.Chrome | BrowserType.Firefox | BrowserType.InternetExplorer, "http://www.github.com")
```
* Install the dependencies ```scriptcs -install ScriptCs.Script```
* Type ```scriptcs start.csx``` to launch the app.
