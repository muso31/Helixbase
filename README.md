<img src="https://nshackblog.files.wordpress.com/2017/02/helixbase1.png" height="154px" width="200px" /><br />
A Sitecore Helix based solution which can be used for Greenfield projects. Tackles some common problems when working with the platform.

[<img src="https://nshack31.visualstudio.com/_apis/public/build/definitions/8aa245ff-435a-46cb-97a0-3d6850ff680f/1/badge"/>](https://nshack31.visualstudio.com/Helix%20Base/_build/index?definitionId=1)

#### Features include:

* Glass Mapper - with fluent configuration and automated mapping registration
* Unicorn - including user and role sync
* Sitecore 9.0 ready
* Bootstrap
* Native dependency injection with auto controller registration
* A sample hero banner feature and sample site project for demonstration
* Generic item repository
* 301 redirects
* Version trimming rules engine - Items limited to 10 versions by default
* Search Templates computed index field - find all items from an index by any templates they implement
* Non admin Item Unlock
* Gulp publish with webroot clean
* <a href="https://jammykam.wordpress.com/2017/09/20/show-title-when-blank/">_Show Title When Blank_</a> patch, the forgotten Sitecore feature!

## Setup Instructions
1. Install <a href="https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/90/Sitecore_Experience_Platform_90_Initial_Release.aspx" target="_blank">Sitecore Experience Platform 9.0 rev. 171002 (9.0 Initial Release)</a>
	1. _Name your instance 'demo.helixbase'_
2. Clone project to 'C:\Projects\Helix base'
	1. _If you use another path, update the '<a href="https://github.com/muso31/Helixbase/blob/master/src/Project/Helixbase/code/App_Config/Include/Project/z.Helixbase.DevSettings.config">z.Helixbase.DevSettings.config</a>' file and the '<a href="https://github.com/muso31/Helixbase/blob/master/gulp-config.js">gulp-config.js</a>'_
3. Install <a href="https://nodejs.org/en/" target="_blank">Node.js</a> and run 'npm-install' in the project root
4. Perform a NuGet restore
5. Publish each project in VS, or view gulp tasks - you may need to update the 'MSBuildToolsVersion' in the gulp-config.js
6. Run Unicorn and sync all configurations

#### Using Helix Base:
Refer to the <a href="https://github.com/muso31/Helixbase/tree/master/src/Feature/Hero/code">Hero Feature</a> as an example.

* View <a href="https://github.com/muso31/Helixbase/blob/master/src/Feature/Hero/code/Services/HeroService.cs">HeroService.cs</a> for examples of how to get an item from Sitecore using the content API and the search API.
* View <a href="https://github.com/muso31/Helixbase/blob/master/src/Feature/Hero/code/Routes/RegisterRoutes.cs">Register routes</a> for an example of how to register a route.

To add a 301 redirect simply add a redirect item to the _Redirect Items_ folder found in your site _Global_ folder.

To change the item version limit edit the rule on the _/sitecore/system/Settings/Rules/Item Saved/Rules/Delete Old Versions_ item. You can also change the rule to recycle or archive old versions. 

In the security editor you can assign non admin Item Unlock permissions.

If you do not require a feature you can easily delete it.