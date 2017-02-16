# Helix Base
A Sitecore Helix based solution which can be used for greenfield projects.

#### Features include:

* Glass Mapper (using fluent configuration)
* Unicorn
* Sitecore 8.2 Update-2 ready
* Bootstrap
* Target .net framework 4.5.2 for backwards compatibility
* Sitecore Dependency Injection
* A sample hero banner feature and sample site project for demonstration
* Pre configured generic item repository

#### Coming soon:

* User and role Unicorn sync
* Unicorn remote module
* Generic repository for search API

## Setup Instructions
1. Install Sitecore Experience Platform 8.2 rev. 161221 (8.2 Update-2) 
	1. _Name your instance 'Helixbase'_
2. Clone project to 'C:\Projects\Helix base'
	2. _If you use another path, update the 'z.Helixbase.Settings.config' file and the 'gulp-config.js'_
3. Install node.js and run 'npm-install' in the project root
4. Perform a Nuget restore
5. Publish each project in VS (or view gulp tasks)
6. Run Unicorn and sync all configurations
