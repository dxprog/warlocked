const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = (env, argv) => {
  const isDev = argv.mode !== 'production';

  return {
    module: {
      rules: [
        {
          test: /\.(js|jsx)$/,
          exclude: /node_modules/,
          use: {
            loader: "babel-loader"
          }
        },
        {
          test: /\.s[ac]ss$/i,
          use: [
            // Creates `style` nodes from JS strings
            isDev ? 'style-loader' : MiniCssExtractPlugin.loader,
            // Translates CSS into CommonJS
            'css-loader',
            // Compiles Sass to CSS
            'sass-loader',
          ],
        },
      ]
    },
    entry: './app/index.js',
    output: {
      filename: 'index.js',
      path: path.resolve(__dirname, 'static/app')
    },
    plugins: [
      new MiniCssExtractPlugin({
        filename: 'index.css',
        path: path.resolve(__dirname, 'static/app')
      })
    ]
  };
};
