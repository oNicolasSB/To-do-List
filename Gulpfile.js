const gulp = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const uglify = require('gulp-uglify');
const concat = require('gulp-concat');
const wrap = require('gulp-wrap');
const babel = require('gulp-babel');
const { src, dest } = require('gulp');

// compilar sass
gulp.task('sass', function () {
  return gulp.src('wwwroot/css/scss/**/*.scss')
    .pipe(sass({ outputStyle: 'compressed' }))
    .pipe(concat('main.min.css'))
    .pipe(gulp.dest('wwwroot/css'));
});

const bootstrapJsFiles = [
  'node_modules/bootstrap/dist/js/bootstrap.bundle.js'
];

// compila o código do ES6 para o ES5 com o babel
gulp.task('babel', () =>
  src('wwwroot/js/*.js')
    .pipe(babel({
      presets: ['@babel/preset-env']
    }))
    .pipe(concat('main.min.js'))
    .pipe(gulp.dest('wwwroot/js/min'))
);

// concatenar e minificar js
gulp.task('uglify', gulp.series('babel', function () {
  return gulp.src(bootstrapJsFiles.concat('wwwroot/js/*.js'))
    .pipe(babel({
      presets: ['@babel/preset-env']
    }))
    .pipe(concat('main.min.js'))
    .pipe(wrap('{\n<%= contents %>\n}')) // adiciona chaves ao redor do conteúdo do arquivo
    .pipe(concat('main.min.js')) // concatena todos os arquivos em um único arquivo main.min.js
    .pipe(uglify())
    .pipe(gulp.dest('wwwroot/js/min'));
}));

// função para monitorar atualizações nos arquivos
gulp.task('watch', function () {
  gulp.watch('wwwroot/css/scss/**/*.scss', gulp.series('sass'));
  gulp.watch('wwwroot/js/*.js', gulp.series('uglify'));
});

// padrão
gulp.task('default', gulp.parallel('sass', 'uglify', 'watch'));
