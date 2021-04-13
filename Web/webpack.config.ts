import path from "path";
import webpack from "webpack";
const ExtractCssChunks = require('extract-css-chunks-webpack-plugin');

const config: webpack.Configuration = {
    entry: "./src/index.tsx",
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [
                    {
                        loader: ExtractCssChunks.loader,
                    },
                    'css-loader',
                ],
            }
        ],
    },
    plugins: [
        new ExtractCssChunks({
            // Options similar to the same options in webpackOptions.output
            // both options are optional
            filename: '[name].css',
            chunkFilename: '[id].css'
        })
    ]
};

export default config;
