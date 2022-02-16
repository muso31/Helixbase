<img src="https://chetcheeto.files.wordpress.com/2022/02/headless.png" height="154px" width="200px" /><br />
<a href="https://www.flaticon.com/free-icons/headless" title="headless icons">Headless icons created by max.icons - Flaticon</a><br />
A Headless Sitecore Helix solution (Headlix) forked from [Helixbase](https://github.com/muso31/Helixbase) which can be used for Greenfield projects. Tackles some common problems when working with the platform.

#### Features include:

* Sitecore Content Serialization (SCS)
* Sitecore 10.2.0 ready
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

1. Install [Sitecore Experience Platform 10.2.0](https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/102/Sitecore_Experience_Platform_102.aspx)
2. Install Sitecore PowerShell Extensions found in the [SXA download](https://dev.sitecore.net/Downloads/Sitecore_Experience_Accelerator/10x/Sitecore_Experience_Accelerator_1020.aspx) page (SXA not required)
3. Install [Sitecore Management Services](https://doc.sitecore.com/xp/en/developers/102/developer-tools/sitecore-management-services.html) Sitecore package
4. Clone the repo and update the 'publishUrl' property in [Local.pubxml](https://github.com/muso31/Helixbase/blob/master/src/Website/website/Properties/PublishProfiles/Local.pubxml#L12) to the target IIS folder
5. Use the 'Local' publish profile in the `Headlixbase.Website` project to publish the solution
6. Install [Sitecore CLI](https://dev.sitecore.net/Downloads/Sitecore_CLI.aspx) and push Sitecore items `dotnet sitecore ser push` - [installation documentation](https://doc.sitecore.com/xp/en/developers/102/developer-tools/install-sitecore-command-line-interface.html)

#### Using Headlix Base:
To change the item version limit edit the rule on the _/sitecore/system/Settings/Rules/Item Saved/Rules/Delete Old Versions_ item. You can also change the rule to recycle or archive old versions. 

In the security editor you can assign non admin Item Unlock permissions.

If you do not require a feature you can easily delete it.

### Renaming Solution / Projects
To rename the Visual Studio Solution, Helix Module Projects and Project references from 'Headlixbase' to a new project name, run [rename.ps1](https://github.com/muso31/Helixbase/blob/master/tools/rename.ps1) -ProjectName [NewProjectName]'. 

## Build

Headlix Base uses [helix-publishing-pipeline](https://github.com/richardszalay/helix-publishing-pipeline) and pre-configures a number of features.

* Content files from all modules are included in the publish
* Sitecore assemblies are excluded from publish, reducing the package filesize

Local publishing:

* Fast publish-on-build of the Local publish profile. This only adds a few seconds and won't recycle your app pool unless you change code. It even runs your debug Web.config transform!
* Old assemblies (Headlixbase.*.dll) are automatically removed

CI/CD publishing:

* Serialization files are automatically included into App_Data\serialization using the 'package' publish profile.

Azure DevOps:

* If you push this repository to Azure DevOps, then in Build Pipelines choose [New build pipeline](https://docs.microsoft.com/en-us/azure/devops/pipelines/create-first-pipeline?view=azure-devops&tabs=tfs-2018-2), it will automatically pick up the included [azure-pipelines.yml](https://github.com/muso31/Helixbase/blob/master/azure-pipelines.yml) file and create an example build pipeline that uses the Package publish profile.

AppVeyor:

* An example [appveyor.xml](https://github.com/muso31/Helixbase/blob/master/appveyor.yml) is included which builds, tests, and packages the solution.

## Legacy Versions
Legacy versions of Helix Base which are no longer updated or maintained can be found below:
  

## Extensions
A repository now exists for Helix Base and Headlixbase compatible modules:

[Helix Base modules](https://github.com/muso31/Helixbase-modules)
