<template>
  <div>
    <h1 class="text-3xl font-bold text-gray-900 mb-6">Dashboard</h1>
    <div class="mb-6">
      <router-link to="/create" class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
        Create Another Quiz
      </router-link>
    </div>
    <div class="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
      <QuizCard v-for="quiz in quizzes" :key="quiz.id" :quiz="quiz" />
    </div>
  </div>
</template>

<script>
import QuizCard from './QuizCard.vue';
import axios from 'axios';

export default {
  name: 'DashboardPage',
  components: {
    QuizCard,
  },
  data() {
    return {
      quizzes: [],
    };
  },
  created() {
    this.fetchQuizzes();
  },
  methods: {
    async fetchQuizzes() {
      // set base API URL to localhost:5041
      axios.defaults.baseURL = 'http://localhost:5041';

      try {
        const response = await axios.get('/api/Quiz/list', {
          params: { userId: this.getUserId() }
        });
        if (response.data.success) {
          this.quizzes = response.data.data;
        } else {
          console.error(response.data.message);
        }
      } catch (error) {
        console.error('Error fetching quizzes:', error);
      }
    },
    getUserId() {
      // Assume a method to retrieve the user ID of the currently logged in user
      return 1; // Replace with real user ID fetching logic
    },
  },
};
</script>
