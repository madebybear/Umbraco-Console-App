# Umbraco-Console-App
A simple console application that makes use of Umbraco services within [Umbraco 7.7.6](https://our.umbraco.org/contribute/releases/776). 

## Use Case
- You have a custom Umbraco website that you've built. Using a windows scheduled task, you want run a umbraco console application and programmatically use the [Umbraco Services](https://our.umbraco.org/documentation/Reference/Management/Services/) to modify your content.

## Prerequisites
- Visual Studio 2015+
- Intermediate knowledge of [Umbraco Cms](https://github.com/umbraco/Umbraco-CMS)

## Getting Started
- Clone the repo to your machine
- Open the .sln in Visual Studio, set the startup project to be `My.Umbraco.Console`
- Build (F5) and ensure nuget packages are downloaded 
- The console should run and you'll see the root contents and children listed

## Notes

`My.Umbraco.Console` - A simple `Program.cs` that will list the root / child nodes of `My.Umbraco.Website`. 

### Postbuild Events
In order for Umbraco to work, there is a a postbuild event that copies the .config files from `My.Umbraco.Website` to the current output folder e.g. _/Debug/bin/config/*.config_. 

### App.config
The app.config is actually a direct copy of the _web.config_ that can be found in the root of  `My.Umbraco.Website` (I manually copied the contents of the web.config _into_ the app.config). 

### DataDirectory
When we override the `GetBootManager` there 2 lines that declare and set a data directory. This is when you're using connection string that includes `|DataDirectory|`. You should be able to remove/comment them out if you're using SQL Server and not an .SDF.

```
protected override IBootManager GetBootManager()
{
	var dataDirectory = new DirectoryInfo("..\\..\\..\\..\\src\\My.Umbraco.Website\\App_Data\\");
	AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory.FullName);

	return new ConsoleBootManager(this);
}
```

### Even more postbuild events...
Finally, since my machine is x64 and this example is using an Sql CE .SDF for the Umbraco Database, I actually needed to copy the _/bin/x86/_ folder (that contains the x86 binaries for SQL Compact Edition)  from `My.Umbraco.Website` so we don't get an error during startup that complains about [File Version Mismatch between ADO.NET versions](https://support.microsoft.com/en-gb/help/974247/fix-you-receive-an-error-message-when-you-run-a-sql-server-compact-3-5)

### My.Umbraco.Website

`My.Umbraco.Website` vanilla Umbraco 7.7.6 with the default starter package installed. To access the back office, open the .sln in Visual Studio and set the startup project to be `My.Umbraco.Website`, you can login with
* URL: http://localhost:64667/umbraco/
* U: `admin@myumbracowebsite.com`
* P: `password123`

## Motivation 
There are a [some](https://github.com/sitereactor/umbraco-console-example) [examples](https://github.com/lars-erik/umbraco-console-example) of how to use the Umbraco services inside a console app. However I needed an up-to-date example using the latest version of Umbraco 7.7.6. I need a quick simple example with *minimal code* to get started. 

## Acknowledgements
* Morten Christensen - [original Umbraco console example](https://github.com/sitereactor/umbraco-console-example) - @siterector https://twitter.com/sitereactor
* Lars-Erik Aabech - [forked Umbraco console example](https://github.com/lars-erik/umbraco-console-example) of Mortens above - @bleedo https://twitter.com/bleedo (please do check out Lars-Eriks site for some great [Umbraco related blog posts](http://blog.aabech.no/))

## Built Using
* [Umbraco 7.7.6](https://our.umbraco.org/contribute/releases/776) 
* [Visual Studio](https://www.visualstudio.com/)

## Authors
* John Bear - Initial Commit - [madebybear](https://twitter.com/madebybear/)

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

