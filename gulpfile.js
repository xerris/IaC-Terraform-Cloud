//Project Properties
const gulp = require('gulp');
const zip = require('gulp-zip');
const del = require('del');
const mkdirp = require('mkdirp');
const path = require('path');
const {exec} = require('child_process');
const {clean, restore, build, test, pack, publish, run} = require('gulp-dotnet-cli');
const version = `0.0.` + (process.env.BUILD_NUMBER || '0' + '-prerelease');
const configuration = process.env.BUILD_CONFIGURATION || 'Debug';
const targetProject = 'src/DevOps.Api/DevOps.Api.csproj';
const cdkProject = 'src/DevOps.Cdk';

function _clean() {
    return gulp.src('*.sln', {read: false})
        .pipe(clean());
}

function _restore() {
    return gulp.src('*.sln', {read: false})
        .pipe(restore());
}

function _build() {
    return gulp.src('*.sln', {read: false})
        .pipe(build({configuration: configuration, version: version}));
}

function _test() {
    return gulp.src('**/*Test*.csproj', {read: false})
        .pipe(test())
}

function _distDir() {
    return new Promise((resolve, error) => {
        del(['dist'], {force: true}).then(
            () => {
                mkdirp('dist', resolve);
            });
    });
}

function _publish() {
    return gulp.src(targetProject, {read: false})
        .pipe(publish({
            configuration: configuration, version: version,
            output: path.join(process.cwd(), 'dist'),
        }));
}

function _run() {
    return gulp.src(runProject, {read: false})
        .pipe(run());
}

function _package() {
    return gulp.src('dist/**/*')
        .pipe(zip('LambdaPackage.zip'))
        .pipe(gulp.dest('dist'));
}

function _deploy() {
    return exec('npm run ci-deploy', {cwd: cdkProject},(error, stdout, stderr) => {
        if (error) {
            console.error(`exec error: ${error}`);
            return;
        }
        console.log(`stdout: ${stdout}`);

        if(stderr)
            console.error(`stderr: ${stderr}`);
    });
}

exports.Build = gulp.series(_clean, _restore, _build);
exports.Test = gulp.series(_clean, _restore, _build, _test);
exports.Default = gulp.series(_clean, _restore, _build, _test);
exports.Publish = gulp.series( _distDir, _clean, _build, _publish);
exports.Run = gulp.series(_distDir, _clean, _build, _run);
exports.Package = gulp.series( _distDir, _clean, _build, _publish, _package);
exports.Deploy = gulp.series( _distDir, _clean, _build, _publish, _package, _deploy);

exports.ciBuild = gulp.series( _distDir, _clean, _restore, _build);
exports.ciTest = gulp.series(_test);
exports.ciPackage = gulp.series(_publish, _package);
exports.ciDeploy = gulp.series( _distDir, _clean, _build, _publish, _package, _deploy);
