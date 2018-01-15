var gulp = require("gulp");
var less = require('gulp-less');
var sourcemaps = require('gulp-sourcemaps');
var cache = require('gulp-cached');
var path = require('path');

var config = {
  lessSrc: './Styles/*.less',
  lessTask: 'less'
};

gulp.task(config.lessTask, function () {
  return gulp.src(config.lessSrc)
    .pipe(cache('lessCache'))
    .pipe(sourcemaps.init())
    .pipe(less({
      paths: [ path.join(__dirname, config.lessTask, 'includes') ]
    }))
    .pipe(sourcemaps.write())
    .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task('lessWatch', function(){
  gulp.watch(config.lessSrc, [config.lessTask]);
});

gulp.task('default', ['lessWatch',config.lessTask]);