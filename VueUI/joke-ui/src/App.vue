<template>
  <v-app>
    <v-main>
      <Jokes/>
    </v-main>
  </v-app>
</template>

<script>
import Jokes from './components/Jokes';

export default {
  name: 'App',

  components: {
    Jokes
  },

  data: () => ({
    //
  }),
};
</script>
