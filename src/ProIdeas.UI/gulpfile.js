/// <binding />
var gulp = require("gulp"),
    glob = require('glob'),
    fs = require("fs"),
    sass = require("gulp-sass"),
    //cached = require('gulp-cached'),
    sourcemaps = require('gulp-sourcemaps'),
    babel = require('rollup-plugin-babel'),
    rollup = require('rollup-stream'),
    source = require('vinyl-source-stream'),
    buffer = require('vinyl-buffer'),
    rename = require('gulp-rename'),
    es = require('event-stream'),
    bowerResolve = require('rollup-plugin-bower-resolve'),
    commonjs = require('rollup-plugin-commonjs'),
    npmResolve = require('rollup-plugin-node-resolve'),
    stringPlugin = require('rollup-plugin-string');

// other content removed

gulp.task("build-sass", function () {
    return gulp.src('Styles/main.scss')
        .pipe(sass())
        .pipe(gulp.dest('wwwroot/css'));
});



//gulp.task('build-pages', function () {
//    gulp.src([
//        './scripts/pages/**/*.js',
//    ]).pipe(sourcemaps.init())
//        .pipe(rollup({

//        }))
//        .pipe(sourcemaps.write('.'))
//        .pipe(gulp.dest('./wwwroot/'));
//})


function build(files, done, err) {
    var tasks = files.map(function (entry) {

        return rollup({
            entry: entry,
            sourceMap: true,
            format: 'iife',
            moduleName: entry,
            plugins: [
                bowerResolve({
                    // Use "module" field for ES6 module if possible, default is `true`.
                    // See: https://github.com/rollup/rollup/wiki/pkg.module
                    module: true,

                    // Use "jsnext:main" field for ES6 module if possible, default is `true`.
                    // This field should not be used, use `module` entry instead, but it is `true`
                    // by default because of legacy packages.
                    // See: https://github.com/rollup/rollup/wiki/jsnext:main
                    jsnext: true,

                    // if there's something your bundle requires that you DON'T
                    // want to include, add it to 'skip'
                    skip: [],  // Default: []

                    // Override path to main file (relative to the module directory).
                    override: {
                        //lodash: 'dist/lodash.js'                            
                    }
                }),
                npmResolve(),
                commonjs(),
                stringPlugin({
                    include: '**/*.html'
                }),
                babel({
                    exclude: 'node_modules/**',
                    presets: ['es2015-rollup'],
                }),


            ]
        })
            // point to the entry file.
            .pipe(source(entry))

            // buffer the output. most gulp plugins, including gulp-sourcemaps, don't support streams.
            .pipe(buffer())

            // tell gulp-sourcemaps to load the inline sourcemap produced by rollup-stream.
            .pipe(sourcemaps.init({ loadMaps: true }))

            // transform the code further here.

            // if you want to output with a different name from the input file, use gulp-rename here.
            .pipe(rename({
                extname: '.bundle.js'
            }))

            // write the sourcemap alongside the output file.
            .pipe(sourcemaps.write('.'))

            // and output to ./dist/main.js as normal.
            .pipe(gulp.dest('./wwwroot/'));


    });
    es.merge(tasks).on('end', done);
}


gulp.task('build-lib', function (done) {
    glob('./Scripts/lib/index.js', function (err, files) {
        build(files, done, err);
    });
});

gulp.task('build-pages', function (done) {
    glob('./Scripts/pages/**/*.js', function (err, files) {
        build(files, done, err);
    });

});

gulp.task('build', ['build-sass','build-lib', 'build-pages']);