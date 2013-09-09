scriptcs-browser
================

# Browser Script Pack

## What is it?
A ScriptCs script pack to interact with browser

## Highlights:

* Supports multiple 
* Allows instances of IOwinStartup to plug in as OWIN middleware components

## Getting started with Browser using the pack

* Copy and paste the following block into a file names start.csx

```csharp
public class TestController : ApiController {
	public string Get { 
		return "Hello World";
	}
}

Require<Browser>();

// Launch web page in all listed browsers
Browser.Open(BrowserType.Chrome | BrowserType.Firefox | BrowserType.InternetExplorer, "http://www.githug.com")
```
* Install the dependencies ```scriptcs -install ScriptCs.Script```
* Running as admin type ```scriptcs start.csx``` to launch the app.
