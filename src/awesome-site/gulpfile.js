var gulp = require("gulp");
var less = require('gulp-less');
var sourcemaps = require('gulp-sourcemaps');
var cache = require('gulp-cached');
var path = require('path');

gulp.task('less', function () {
  return gulp.src('./Styles/*.less')
    .pipe(cache('lessCache'))
    .pipe(sourcemaps.init())
    .pipe(less({
      paths: [ path.join(__dirname, 'less', 'includes') ]
    }))
    .pipe(sourcemaps.write())
    .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task('lessWatch', function(){
  gulp.watch('./Styles/*.less', ['less']);
});

gulp.task('default', ['lessWatch','less']);