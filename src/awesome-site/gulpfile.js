var gulp = require("gulp"),
    less = require('gulp-less'),
    sourcemaps = require('gulp-sourcemaps'),
    cache = require('gulp-cached'),
    path = require('path');

var config = {
  lessSrc: './Styles/*.less',
  lessTask: 'less',
  lessWatchTask: 'lessWatch',
  commonMarkSrc: './Cms/**/*.markdown',
  commonMarkTask: 'convertCommonMark',
  commonMarkWatchTask: 'commonMarkWatch'
};

gulp.task(config.lessTask, function () {
  return gulp.src(config.lessSrc)
    .pipe(cache('less'))
    .pipe(sourcemaps.init())
    .pipe(less({
      paths: [ path.join(__dirname, config.lessTask, 'includes') ]
    }))
    .pipe(sourcemaps.write())
    .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task(config.lessWatchTask, function(){
  gulp.watch(config.lessSrc, [config.lessTask]);
});

gulp.task(config.commonMarkTask, function () {
  return gulp.src(config.commonMarkSrc)
    .pipe(cache('commonMark'))
    .pipe(md({preset: 'commonmark'}))
})

gulp.task(config.commonMarkWatchTask, function(){
  gulp.watch(config.commonMarkSrc, [config.commonMarkTask]);
});

gulp.task('dev', [config.lessWatchTask, config.lessTask, config.commonMarkWatchTask, config.commonMarkTask]);

gulp.task('prod-deploy', [config.lessTask, config.commonMarkTask]);