var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var runSequence = require("run-sequence");
var gulpConfig = require("./gulp-config.js")();
module.exports.config = gulpConfig;



gulp.task("Publish-All-Projects", function () {
    return gulp.src("./src/{Feature,Foundation,Project}/**/code/*.csproj")
      .pipe(foreach(function (stream, file) {
          return stream
          .pipe(debug({ title: "Publishing "}))
          .pipe(msbuild({
              targets: ["Build"],
              errorOnFail: true,
              //stdout: true,
              maxcpucount: 0,
              properties: {
                  Configuration: gulpConfig.buildConfiguration,
                  publishUrl: gulpConfig.webRoot,
                  DeployDefaultTarget: "WebPublish",
                  WebPublishMethod: "FileSystem",
                  DeployOnBuild: "true",
                  DeleteExistingFiles: "false",
                  _FindDependencies: "false"
              }
          }));
      }).on('error', function (err) {
          console.log(err);
      }));
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
                toolsVersion: 14.0,
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

    return gulp.src("./Helixbase.sln")
        .pipe(msbuild({
            targets: targets,
            configuration: gulpConfig.buildConfiguration,
            logCommand: false,
            verbosity: "minimal",
            stdout: true,
            errorOnFail: true,
            maxcpucount: 0,
            toolsVersion: 14.0
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
