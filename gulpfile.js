var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var runSequence = require("run-sequence");
var gulpConfig = require("./gulp-config.js")();
var clean = require('gulp-clean');
var nugetRestore = require('gulp-nuget-restore');
module.exports.config = gulpConfig;

gulp.task("Publish-All-Projects", function (callback) {
    return runSequence(
        "Build-Solution",
        "Publish-Foundation-Projects",
        "Publish-Feature-Projects",
        "Publish-Project-Projects", callback);
});

var cleanProjectFiles = function (layerName) {
    const filesToDelete = [
        gulpConfig.webRoot + '/bin/Helixbase.' + layerName + '.*',
        gulpConfig.webRoot + '/App_Config/Include/' + layerName
    ];
    console.log("Removing " + layerName + " configs/binaries");
    return gulp.src(filesToDelete, { read: false })
        .pipe(clean({ force: true }));
};

var publishProjects = function (location, dest) {
    dest = dest || gulpConfig.webRoot;
    var targets = ["Build"];

    console.log("publish to " + dest + " folder");
    return gulp.src([location + "/**/code/*.csproj"])
        .pipe(foreach(function (stream, file) {
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

gulp.task("Publish-Foundation-Projects", function () {
    cleanProjectFiles("Foundation"),
        publishProjects("./src/Foundation");
});

gulp.task("Publish-Feature-Projects", function () {
    cleanProjectFiles("Feature"),
        publishProjects("./src/Feature");
});

gulp.task("Publish-Project-Projects", function () {
    cleanProjectFiles("Project"),
        publishProjects("./src/Project");
});

gulp.task("Clean-Transform-Configs", function () {
    cleanProjectFiles("Foundation"),
        cleanProjectFiles("Feature"),
              cleanProjectFiles("Project");
});

//// Note: intended to be called after publishing
//gulp.task("Clean-Transform-Configs", function (layerName) {
//    const filesToDelete = [
//        gulpConfig.webRoot + '/App_Config/Include/' + layerName + '/CD',
//        gulpConfig.webRoot + '/App_Config/Include/' + layerName + '/CM'
//    ];
//    console.log("Removing transform configs");
//    return gulp.src(filesToDelete, { read: false })
//        .pipe(clean({ force: true }));
//});
