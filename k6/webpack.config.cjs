/* eslint-disable @typescript-eslint/no-var-requires */
const path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const GlobEntries = require('webpack-glob-entries');
const webpack = require('webpack');

/** @type {import('webpack').Configuration} */
module.exports = [
    {
        name: 'test',
        mode: 'production',
        entry: GlobEntries('./test/*.ts'), // Generates multiple entry for each test
        output: {
            path: path.join(__dirname, 'dist'),
            libraryTarget: 'commonjs',
            filename: '[name].js',
            publicPath: '.',
            devtoolModuleFilenameTemplate: (info) => {
                return `file://.${info.resourcePath}`;
            },
        },
        resolve: {
            extensions: ['.ts', '.js'],
        },
        module: {
            rules: [
                {
                    test: /\.ts$/,
                    use: 'babel-loader',
                    exclude: /node_modules/,
                },
            ],
        },
        target: 'web',
        externals: /^(k6|https?:\/\/)(\/.*)?/,
        // Generate map files for compiled scripts
        devtool: 'source-map',
        stats: {
            colors: true,
        },
        plugins: [
            new CleanWebpackPlugin(),
            // Copy assets to the destination folder
            // see `src/post-file-test.ts` for an test example using an asset
            new CopyPlugin({
                patterns: [
                    {
                        from: path.resolve(__dirname, 'assets'),
                        noErrorOnMissing: true,
                    },
                ],
            }),
            new webpack.NormalModuleReplacementPlugin(/^\..+\.js$/, (resource) => {
                resource.request = resource.request.replace(/\.js$/, '');
            }),
        ],
        performance: {
            hints: false,
            maxEntrypointSize: 512000,
            maxAssetSize: 512000,
        },
        optimization: {
            // Don't minimize, as it's not used in the browser
            minimize: false,
        },
        watchOptions: {
            ignored: /node_modules|assets|dist/,
        },
    }
];