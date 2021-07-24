<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png">
    <Jokes />
  </div>
</template>

<script>
import Jokes from './components/Jokes.vue'

export default {
  name: 'App',
  components: {
    Jokes
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
