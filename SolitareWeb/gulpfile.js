const { parallel, src, dest } = require("gulp");
const postcss = require('gulp-postcss');
const cleanCss = require('gulp-clean-css');
const purgeCss = require('gulp-purgecss');
const sass = require('gulp-sass')(require('sass'));
const rename = require('gulp-rename');
const cssmin = require('gulp-cssmin');

//Style paths
var cssDest = './wwwroot/css/';

function sassTask() {
    return src('./Styles/**/*.scss')
        .pipe(sass({ outputStyle: 'compressed' }).on('error', sass.logError))
        .pipe(rename('sf.css'))
        .pipe(dest(cssDest));

}
function cssDevTask() {
    const postcss = require('gulp-postcss');

    return src('./Styles/*.css')
        .pipe(postcss([
            require('precsss'),
            require('postcss-nested'),
            require('tailwindcss'),
            require('autoprefixer')
        ]))
        //.pipe(cssmin())
        .pipe(rename({ suffix: '.min' }))
        .pipe(dest('./wwwroot/css/'));
}

function cssProdTask() {
    const sourcemaps = require('gulp-sourcemaps');

    return src('./Styles/site.css',)
        .pipe(postcss([
            require('precsss'),
            require('tailwindcss'),
            require('autoprefixer')
        ]))
        .pipe(purgeCss({ content: ['**/*.razor'] }))
        .pipe(cleanCss({ level: 2 }))
        .pipe(cssmin())
        .pipe(rename({ suffix: '.min' }))
        //.pipe(sourcemaps.write('.'))
        .pipe(dest('./wwwroot/css/'));
}

exports.buildDev = parallel(cssDevTask, sassTask);
exports.buildProd = parallel(cssProdTask, sassTask);