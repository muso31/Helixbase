<img src="https://nshackblog.files.wordpress.com/2017/02/helixbase1.png" height="154px" width="200px" /><br />
A Sitecore Helix based solution which can be used for Greenfield projects. Tackles some common problems when working with the platform.

[<img src="https://nshack31.visualstudio.com/_apis/public/build/definitions/8aa245ff-435a-46cb-97a0-3d6850ff680f/1/badge"/>](https://nshack31.visualstudio.com/Helix%20Base/_build/index?definitionId=1)

#### Features include:

* Glass Mapper v5 - with fluent configuration and automated mapping registration
* Unicorn - including user and role sync
* Sitecore 9.0.2 ready
* Bootstrap v4
* Native dependency injection with auto controller registration
* A sample hero banner feature and sample site project for demonstration
* Generic content repositories (by Rendering, Item Context, or Glass Content)
* 301 redirects
* Version trimming rules engine - Items limited to 10 versions by default
* Search Templates computed index field - find all items from an index by any templates they implement
* Non admin Item Unlock
* Auto unlocks items when a user is deleted
* Gulp publish with webroot clean
* <a href="https://jammykam.wordpress.com/2017/09/20/show-title-when-blank/">_Show Title When Blank_</a> patch, the forgotten Sitecore feature!
* A module just for fun - currently adds logos to the Unicorn console

## Setup Instructions
1. Install <a href="https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/90/Sitecore_Experience_Platform_90_Update2.aspx" target="_blank">Sitecore Experience Platform 9.0 rev. 180604 (9.0 Update-2)</a>
	1. _Name your instance 'demo.helixbase'_
2. Clone project to 'C:\Projects\Helix base'
	1. _If you use another path, update the '<a href="https://github.com/muso31/Helixbase/blob/master/gulp-config.js#L4">gulp-config.js</a>'_
3. Install <a href="https://nodejs.org/en/" target="_blank">Node.js</a> and run 'npm-install' in the project root
4. Publish each project in VS, or use <a href="https://github.com/muso31/Helixbase/blob/master/gulpfile.js#L85">gulp tasks</a>
5. Run Unicorn and sync all configurations

#### Using Helix Base:
Refer to the <a href="https://github.com/muso31/Helixbase/tree/master/src/Feature/Hero/code">Hero Feature</a> as an example.

* View <a href="https://github.com/muso31/Helixbase/blob/master/src/Feature/Hero/code/Services/HeroService.cs">HeroService.cs</a> for examples of retrieving Sitecore items using the content API and the search API.
* View <a href="https://github.com/muso31/Helixbase/blob/master/src/Feature/Hero/code/Routes/RegisterRoutes.cs">Register routes</a> for an example of how to register a route.

To add a 301 redirect simply add a redirect item to the _Redirect Items_ folder found in your site _Global_ folder.

To change the item version limit edit the rule on the _/sitecore/system/Settings/Rules/Item Saved/Rules/Delete Old Versions_ item. You can also change the rule to recycle or archive old versions. 

In the security editor you can assign non admin Item Unlock permissions.

If you do not require a feature you can easily delete it.

### Renaming Solution / Projects
To rename the Visual Studio Solution, Helix Module Projects and Project references from 'Helixbase' to a new project name, run '<a href="https://github.com/muso31/Helixbase/blob/master/tools/rename.ps1">rename.ps1</a> -ProjectName [NewProjectName]'. 
