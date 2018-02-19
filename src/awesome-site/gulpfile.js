var gulp = require("gulp"),
  less = require('gulp-less'),
  sourcemaps = require('gulp-sourcemaps'),
  cache = require('gulp-cached'),
  path = require('path'),
  markdown = require('gulp-markdown');

var config = {
  lessSrc: './wwwroot/css',
  lessTask: 'less',
  commonMarkSrc: './Cms/**/*.markdown',
  commonMarkTask: 'convertCommonMark'
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

gulp.task(config.commonMarkTask, function () {
  return gulp.src(config.commonMarkSrc, { base: 'client' })
    .pipe(cache(config.commonMarkTask))
    .pipe(markdown())
    .pipe(gulp.dest(function (file) {
      return file.base;
  }));
});

gulp.task('prod-deploy', gulp.parallel(config.lessTask, config.commonMarkTask));