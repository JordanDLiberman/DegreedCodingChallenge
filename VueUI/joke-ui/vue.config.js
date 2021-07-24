module.exports = {
  transpileDependencies: [
    'vuetify'
  ],
  devServer: {
    proxy: {
      '^/api': {
        target: 'https://localhost:44332',
        changeOrigin: true
      },
    }
  },
  outputDir: '../wwwroot/app'
}
