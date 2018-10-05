var gulp = require("gulp"),
  less = require('gulp-less'),
  sourcemaps = require('gulp-sourcemaps'),
  cache = require('gulp-cached'),
  path = require('path');

var config = {
  lessSrc: './wwwroot/css',
  lessTask: 'less'
};

gulp.task(config.lessTask, function () {
  return gulp.src(config.lessSrc)
    .pipe(cache(config.lessTask))
    .pipe(sourcemaps.init())
    .pipe(less({
      paths: [ path.join(__dirname, config.lessTask, 'includes') ]
    }))
    .pipe(sourcemaps.write())
    .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task('prod-deploy', gulp.parallel(config.lessTask));