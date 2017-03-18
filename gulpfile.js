var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var runSequence = require("run-sequence");
var gulpConfig = require("./gulp-config.js")();
module.exports.config = gulpConfig;

// From Habitat
gulp.task("Publish-All-Projects", function (callback) {
    return runSequence(
        "Build-Solution",
        "Publish-Foundation-Projects",
        "Publish-Feature-Projects",
        "Publish-Project-Projects", callback);
});

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
    return publishProjects("./src/Foundation");
});

gulp.task("Publish-Feature-Projects", function () {
    return publishProjects("./src/Feature");
});

gulp.task("Publish-Project-Projects", function () {
    return publishProjects("./src/Project");
});
