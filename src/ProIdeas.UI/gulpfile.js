/// <binding AfterBuild='build-pages' />
var gulp = require("gulp"),
    glob = require('glob'),
    fs = require("fs"),
    sass = require("gulp-sass"),
    sourcemaps = require('gulp-sourcemaps'),
    babel = require('rollup-plugin-babel'),
    rollup = require('rollup-stream'),
    source = require('vinyl-source-stream'),
    buffer = require('vinyl-buffer'),
    rename = require('gulp-rename'),    
    es = require('event-stream');

// other content removed

gulp.task("sass", function () {
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





gulp.task('build-pages', function (done) {


    glob('./Scripts/pages/**/*.js', function (err, files) {
        if (err) done(err);

        console.log(files);

        var tasks = files.map(function (entry) {

            return rollup({
                entry: entry,
                sourceMap: true,
                format: 'amd',                
                plugins: [
                    babel({
                        exclude: 'node_modules/**',
                        presets: ['es2015-rollup'],
                    })                    
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
    });

});