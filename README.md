<img src="https://nshackblog.files.wordpress.com/2017/02/helixbase1.png" height="154px" width="200px" /><br />
A Sitecore Helix based solution which can be used for Greenfield projects. Tackles some common problems when working with the platform.

![ASP.NET CI](https://github.com/muso31/Helixbase/workflows/ASP.NET%20CI/badge.svg) [![Helix Check](https://github.com/muso31/Helixbase/workflows/Helix%20Check/badge.svg)](https://github.com/muso31/Helixbase/actions?query=workflow%3A%22Helix+Check%22) [![GitHub Stars](https://img.shields.io/github/stars/muso31/helixbase?label=GitHub%20Stars)](https://github.com/muso31/Helixbase/stargazers)

#### Features include:

* Glass Mapper v5 - with fluent configuration and automated mapping registration
* Sitecore Content Serialization (SCS)
* Sitecore 10.0 Update-1 ready
* Pre compiled Razor views
* Bootstrap v4
* Native dependency injection with auto controller registration
* A sample hero banner feature and sample site project for demonstration
* Generic content repositories (by Rendering, Item Context, or Glass Content)
* Version trimming rules engine - Items limited to 10 versions by default
* Search Templates computed index field - find all items from an index by any templates they implement
* Non admin Item Unlock
* Auto unlocks items when a user is deleted
* Integration with [helix-publishing-pipeline](https://github.com/richardszalay/helix-publishing-pipeline)
* Fast ([see benchmark](https://github.com/richardszalay/Helixbase-HPP/tree/benchmarks#benchmarks)) publish-on-build (when building inside Visual Studio)
* [_Show Title When Blank_](https://jammykam.wordpress.com/2017/09/20/show-title-when-blank/) patch, the forgotten Sitecore feature!
* [Helix Check](https://github.com/marketplace/actions/helix-check) GitHub Action

## Setup Instructions
*Please Install Visual Studio 2017 Version 15.7 or higher as this project uses PackageReference

1. Install [Sitecore Experience Platform 10.0 Update-1](https://dev.sitecore.net/en/Downloads/Sitecore_Experience_Platform/100/Sitecore_Experience_Platform_100_Update1.aspx)
2. Install [Sitecore Management Services](https://doc.sitecore.com/developers/100/developer-tools/en/sitecore-management-services.html), documentation [here](https://doc.sitecore.com/developers/100/developer-tools/en/sitecore-command-line-interface.html)
3. Clone the repo and update the 'publishUrl' property in [Local.pubxml](https://github.com/muso31/Helixbase/blob/master/src/Website/website/Properties/PublishProfiles/Local.pubxml#L12) to the target IIS folder
4. Use the 'Local' publish profile in the Helixbase.Website project to publish the solution
5. Install [Sitecore CLI](https://dev.sitecore.net/en/Downloads/Sitecore_CLI/2x/Sitecore_CLI_200.aspx) and push Sitecore items `dotnet sitecore ser push`

#### Using Helix Base:
Refer to the [Hero Feature](https://github.com/muso31/Helixbase/tree/master/src/Feature/Hero/website) as an example.

* View [HeroService.cs](https://github.com/muso31/Helixbase/blob/master/src/Feature/Hero/website/Services/HeroService.cs) for examples of retrieving Sitecore items using the content API and the search API.
* View [Register routes](https://github.com/muso31/Helixbase/blob/master/src/Feature/Hero/website/Routes/RegisterRoutes.cs) for an example of how to register a route.

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

* Serialization files are automatically included into App_Data\serialization using the 'package' publish profile.

Azure DevOps:

* If you push this repository to Azure DevOps, then in Build Pipelines choose [New build pipeline](https://docs.microsoft.com/en-us/azure/devops/pipelines/create-first-pipeline?view=azure-devops&tabs=tfs-2018-2), it will automatically pick up the included [azure-pipelines.yml](https://github.com/muso31/Helixbase/blob/master/azure-pipelines.yml) file and create an example build pipeline that uses the Package publish profile.

AppVeyor:

* An example [appveyor.xml](https://github.com/muso31/Helixbase/blob/master/appveyor.yml) is included which builds, tests, and packages the solution.

## Legacy Versions
Legacy versions of Helix Base which are no longer updated or maintained can be found below:

[Helix Base 10.0.0](https://github.com/muso31/Helixbase/tree/feature/10.0.0) (updated until 11/01/2021)  
[Helix Base 10.0.0 None SDK project format](https://github.com/muso31/Helixbase/tree/feature/10.0.0-old-csproj) (updated until 11/01/2021)  
[Helix Base 9.3.0](https://github.com/muso31/Helixbase/tree/feature/9.3.0) (updated until 30/08/2020)  
[Helix Base 9.3.0 None SDK project format](https://github.com/muso31/Helixbase/tree/feature/9.3.0-old-csproj) (updated until 30/08/2020)  
[Helix Base 9.2.0](https://github.com/muso31/Helixbase/tree/feature/9.2.0) (updated until 29/11/2019)  
[Helix Base 9.1.0](https://github.com/muso31/Helixbase/tree/feature/9.1.0) (updated until 08/08/2019)  
[Helix Base 9.0.2](https://github.com/muso31/Helixbase/tree/feature/9.0.2) (updated until 21/12/2018)  

## Extensions
A repository now exists for Helix Base compatible modules:

[Helix Base modules](https://github.com/muso31/Helixbase-modules)


## Collaborators
Special mention to those who collaborate on the project:

[Richard Szalay](https://github.com/richardszalay)  
[Steve McGill](https://github.com/steviemcg)
