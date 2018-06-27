var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var gulpConfig = require("./gulp-config.js")();
var clean = require('gulp-clean');
var nugetRestore = require('gulp-nuget-restore');
module.exports.config = gulpConfig;

function cleanProjectFiles(layerName) {
    const filesToDelete = [
        gulpConfig.webRoot + '/bin/Helixbase.' + layerName + '.*',
        gulpConfig.webRoot + '/App_Config/Include/' + layerName
    ];
    console.log("Removing " + layerName + " configs/binaries");
    return gulp.src(filesToDelete, { read: false, allowEmpty: true })
        .pipe(clean({ force: true }));
};

function publishProjects(location, dest) {
    dest = dest || gulpConfig.webRoot;
    var targets = ["Build"];

    console.log("publish to " + dest + " folder");
    return gulp.src([location + "/**/code/*.csproj"])
        .pipe(foreach(function (stream) {
            return stream
                .pipe(debug({ title: "Building project:" }))
                .pipe(msbuild({
                    targets: targets,
                    configuration: gulpConfig.buildConfiguration,
                    logCommand: false,
                    verbosity: "minimal",
                    stdout: true,
                    errorOnFail: true,
                    maxcpucount: 0,
                    toolsVersion: gulpConfig.MSBuildToolsVersion,
                    properties: {
                        DeployOnBuild: "true",
                        DeployDefaultTarget: "WebPublish",
                        WebPublishMethod: "FileSystem",
                        DeleteExistingFiles: "false",
                        publishUrl: dest,
                        _FindDependencies: "false"
                    }
                }));
        }));
};

gulp.task("Build-Solution", function () {
    var targets = ["Build"];

    return gulp.src("./" + gulpConfig.solutionName + ".sln")
        .pipe(debug({ title: "NuGet restore:" }))
        .pipe(nugetRestore())
        .pipe(debug({ title: "Building solution:" }))
        .pipe(msbuild({
            targets: targets,
            configuration: gulpConfig.buildConfiguration,
            logCommand: false,
            verbosity: "minimal",
            stdout: true,
            errorOnFail: true,
            maxcpucount: 0,
            toolsVersion: gulpConfig.MSBuildToolsVersion
        }));
});

gulp.task("Publish-Foundation-Layer", function () {
    return cleanProjectFiles("Foundation"),
        publishProjects("./src/Foundation");
});

gulp.task("Publish-Feature-Layer", function () {
    return cleanProjectFiles("Feature"),
        publishProjects("./src/Feature");
});

gulp.task("Publish-Project-Layer", function () {
    return cleanProjectFiles("Project"),
        publishProjects("./src/Project");
});


gulp.task("Publish-All-Projects", gulp.series("Build-Solution",
    "Publish-Foundation-Layer",
    "Publish-Feature-Layer",
    "Publish-Project-Layer"));