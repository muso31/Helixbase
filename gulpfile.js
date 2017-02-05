var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var gulpConfig = require("./gulp-config.js")();
module.exports.config = gulpConfig;


gulp.task("Publish-All-Projects", function () {
    return gulp.src("./{Feature,Foundation,Project}/**/**/*.csproj")
      .pipe(foreach(function (stream, file) {
          return stream
          .pipe(debug({ title: "Publishing " }))
          .pipe(msbuild({
              targets: ["Build"],
              gulpConfiguation: gulpConfig.buildConfiguration,
              properties: {
                  publishUrl: gulpConfig.webRoot,
                  DeployDefaultTarget: "WebPublish",
                  WebPublishMethod: "FileSystem",
                  DeployOnBuild: "true",
                  DeleteExistingFiles: "false",
                  _FindDependencies: "false"
              }
          }));
      }));
});
