<img src="https://nshackblog.files.wordpress.com/2017/02/helixbase1.png" height="154px" width="200px" /><br />
A Sitecore Helix based solution which can be used for Greenfield projects. Tackles some common problems when working with the platform.

<img src="https://nshack31.visualstudio.com/Helix Base/_apis/build/status/Helix%20Base%20CI?branchName=master"/>

#### Features include:

* Glass Mapper v5 - with fluent configuration and automated mapping registration
* Unicorn - including user and role sync
* Sitecore 9.1 ready
* Bootstrap v4
* Native dependency injection with auto controller registration
* A sample hero banner feature and sample site project for demonstration
* Generic content repositories (by Rendering, Item Context, or Glass Content)
* 301 redirects
* Version trimming rules engine - Items limited to 10 versions by default
* Search Templates computed index field - find all items from an index by any templates they implement
* Non admin Item Unlock
* Auto unlocks items when a user is deleted
* Integration with [helix-publishing-pipeline](https://github.com/richardszalay/helix-publishing-pipeline)
* Fast ([see?](https://github.com/richardszalay/Helixbase-HPP/tree/benchmarks#benchmarks)) publish-on-build (when building inside Visual Studio)
* [_Show Title When Blank_](https://jammykam.wordpress.com/2017/09/20/show-title-when-blank/) patch, the forgotten Sitecore feature!
* A module just for fun - currently adds logos to the Unicorn console

## Setup Instructions
*Please Install Visual Studio 2017 Version 15.7 or higher as this project uses PackageReference

1. Clone project to 'C:\Projects\Helixbase' or wherever you like, the link with Unicorn is handled automatically
2. Adjust `C:\Projects\Helixbase\build\deploy-local.ps1` where necessary (for example: SQL credentials)
3. Open *Developer Command Prompt for VS 2017* as an administrator
4. Run `powershell -file C:\Projects\Helixbase\build\deploy-local.ps1`

The script handles the following steps:

* Builds the solution
* Installs Sitecore and its prerequisites if necessary using [Simple Install Scripts](https://github.com/ParTech/ParTech.SimpleInstallScripts)
* Adjusts the local.xml publishing profile to point to the correct location
* Adds Unicorn config to App_Config/Environment/ with the shared secret and a link back to the correct sourceFolder
* Deploys the solution using [helix-publishing-pipeline](https://github.com/richardszalay/helix-publishing-pipeline)
* Syncs the Unicorn serialized items to Sitecore
* Executes a Smart Publish
* Rebuilds the Link Databases
* Rebuilds the Search Indexes
* Deploys the Marketing Definitions

#### Using Helix Base:
Refer to the [Hero Feature](https://github.com/muso31/Helixbase/tree/master/src/Feature/Hero/code) as an example.

* View [HeroService.cs](https://github.com/muso31/Helixbase/blob/master/src/Feature/Hero/code/Services/HeroService.cs) for examples of retrieving Sitecore items using the content API and the search API.
* View [Register routes](https://github.com/muso31/Helixbase/blob/master/src/Feature/Hero/code/Routes/RegisterRoutes.cs) for an example of how to register a route.

To add a 301 redirect simply add a redirect item to the _Redirect Items_ folder found in your site _Global_ folder.

To change the item version limit edit the rule on the _/sitecore/system/Settings/Rules/Item Saved/Rules/Delete Old Versions_ item. You can also change the rule to recycle or archive old versions. 

In the security editor you can assign non admin Item Unlock permissions.

If you do not require a feature you can easily delete it.

### Renaming Solution / Projects
To rename the Visual Studio Solution, Helix Module Projects and Project references from 'Helixbase' to a new project name, run [rename.ps1](https://github.com/muso31/Helixbase/blob/master/tools/rename.ps1) -ProjectName [NewProjectName]'. 

## Build

Helix Base uses [helix-publishing-pipeline](https://github.com/richardszalay/helix-publishing-pipeline) and pre-configures a number of features.

* Content files from all modules are included in the publish
* Sitecore assemblies are excluded from publish, reducing the package filesize

Local publishing:

* Fast publish-on-build of the Local publish profile. This only adds a few seconds and won't recycle your app pool unless you change code. It even runs your debug Web.config transform!
* Old assemblies (Helixbase.*.dll) are automatically removed

CI/CD publishing:

* Using [helix-publishing-pipeline](https://github.com/richardszalay/helix-publishing-pipeline), a single Web Deploy Package is generated of all files in the solution which are configured as *Content*. It also ensures all Unicorn serialization files are collected together in one folder allowing for a single "Items" artifact. Both of these artifacts are placed in the **Output** folder.

Azure DevOps:

* If you push this repository to Azure DevOps, then in Build Pipelines choose [New build pipeline](https://docs.microsoft.com/en-us/azure/devops/pipelines/create-first-pipeline?view=azure-devops&tabs=tfs-2018-2), it will automatically pick up the included [azure-pipelines.yml](https://github.com/muso31/Helixbase/blob/master/azure-pipelines.yml) file and create an example build pipeline that uses the Package publish profile.

AppVeyor:

* If you connect this repository with AppVeyor, then it will automatically pick up the included [appveyor.yml](https://github.com/muso31/Helixbase/blob/master/appveyor.yml) file. The only remaining step is to add the `DownloadBase` [Environment Variable](https://www.appveyor.com/docs/environment-variables/) which links to where your WDPs, Config files and license are stored.

## Legacy Versions
Legacy versions of Helix Base which are no longer updated or maintained can be found below:

[Helix Base 9.0.2](https://github.com/muso31/Helixbase/tree/feature/9.0.2) (updated until 21/12/2018)

## Collaborators
Special mention to those who collaborate on the project:

[Richard Szalay](https://github.com/richardszalay)
