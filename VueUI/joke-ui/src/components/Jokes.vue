<template>
  <v-container>
    <v-row>
      <v-col cols="6">
        <v-btn elevation="2" rounded color="primary" dark @click="randomClicked">Random Joke</v-btn>
      </v-col>
      <v-col cols="3">
        <v-text-field label="Enter Search Text Here..." solo v-model="text"></v-text-field>
      </v-col>
      <v-col cols="3">
        <v-btn elevation="2" rounded color="primary" dark @click="searchClicked">Search</v-btn>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="8">
        <v-text-area name="jokes" label="Jokes" placeholder="Jokes!  Dad Jokes for all!" solo v-if="buttonClicked === 'random'">{{jokes}}</v-text-area>
		<div v-if="buttonClicked === 'search'">
			<ul>
				<li v-for="(item, name) in jokeDict">
					<strong>{{name}}</strong>
					<ul>
						<li v-for="j in item">
							{{j}}
						</li>
					</ul>
				</li>
			</ul>
		</div>
      </v-col>
    </v-row>
    
  </v-container>
</template>

<script>
  import axios from 'axios';

  export default {
    name: 'Jokes',

    data: () => ({
      text: "",
      jokes: "",
	  jokeDict: {},
	  buttonClicked: ""
    }),

    methods: {
      randomClicked: function() {
	  this.buttonClicked = 'random';
        axios 
          .get('/Joke')
          .then(response => { this.jokes = response.data; })
          .catch(err => { this.jokes = err; });
      },
      searchClicked: function() {
	  this.buttonClicked = 'search';
        axios
          .get('/Joke/' + this.text)
          .then(response => {
			this.jokeDict = response.data;		  
		  })
          .catch(err => this.jokes = err);
      }
    }
  }
</script>
