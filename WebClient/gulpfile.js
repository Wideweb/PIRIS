var ts = require('gulp-typescript');
var gulp = require('gulp');
var clean = require('gulp-clean');
var sourcemaps = require('gulp-sourcemaps');
var path = require('path');

var destPath = './wwwroot/libs/';

// Delete the dist directory
gulp.task('clean', function () {
    return gulp.src(['./wwwroot/libs/', './wwwroot/app/'])
        .pipe(clean());
});

gulp.task("scriptsNStyles", () => {
    gulp.src([
            'core-js/client/**',
            'systemjs/dist/system.src.js',
            'reflect-metadata/**',
            'rxjs/**',
            'zone.js/dist/**',
            '@angular/**',
            'jquery/dist/jquery.*js',
            'bootstrap/dist/js/bootstrap.*js',
            'bootstrap/dist/css/bootstrap.*css',
            '@angular2-material/**/*',
    ], {
        cwd: "node_modules/**"
    })
        .pipe(gulp.dest("./wwwroot/libs"));
});

var tsProject = ts.createProject('scripts/tsconfig.json');
gulp.task('ts', function (done) {
    
    var tsResult = gulp
        .src(["scripts/**/*.ts"])
        .pipe(sourcemaps.init())
        .pipe(ts(tsProject), undefined, ts.reporter.fullReporter());

    return tsResult.js
        .pipe(sourcemaps.write('../maps'))
        .pipe(gulp.dest('./wwwroot/app'));
});

gulp.task('templates', function () {
    gulp.src("scripts/**/*.html").pipe(gulp.dest('./wwwroot/app'));
    gulp.src("scripts/**/*.css").pipe(gulp.dest('./wwwroot/app'));
});

gulp.task('watch', ['watch.ts', 'watch.templates']);

gulp.task('watch.ts', ['ts'], function () {
    return gulp.watch('scripts/**/*.ts', ['ts']);
});

gulp.task('watch.templates', ['templates'], function () {
    return gulp.watch(['scripts/**/*.html', 'scripts/**/*.css'], ['templates']);
});

gulp.task('default', ['templates', 'ts', 'watch']);