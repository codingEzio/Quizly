<template>
  <div class="max-w-2xl mx-auto px-4 py-8">
    <h1 class="text-3xl font-bold text-gray-900 mb-6 text-center">Create Quiz</h1>
    <div class="bg-white shadow overflow-hidden sm:rounded-lg">
      <div class="px-6 py-8">
        <div class="mb-6">
          <label for="quiz-name" class="block text-sm font-medium text-gray-700 mb-1">Quiz Name</label>
          <input type="text" name="quiz-name" id="quiz-name" class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md" v-model="quizName" />
        </div>

        <div class="mb-6">
          <label for="context" class="block text-sm font-medium text-gray-700 mb-1">Context</label>
          <textarea id="context" name="context" rows="4" class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md" v-model="context"></textarea>
        </div>

        <div class="mb-8">
          <label for="difficulty" class="block text-sm font-medium text-gray-700 mb-1">Difficulty Level</label>
          <input type="range" min="1" max="5" class="w-full" v-model="difficulty" />
          <div class="text-sm text-gray-500 mt-2 text-center">Level: {{ difficulty }}</div>
        </div>

        <div class="flex justify-center">
          <button @click="createQuiz" class="inline-flex items-center px-6 py-3 border border-transparent text-base font-medium rounded-md shadow-sm text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 transition duration-150 ease-in-out">
            Create Quiz
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      quizName: '',
      context: '',
      difficulty: 3,
    };
  },
  methods: {
    async createQuiz() {
      axios.defaults.baseURL = 'http://localhost:5041';

      try {
        const response = await axios.post('/api/Quiz/new', {
          title: this.quizName,
          context: this.context,
          difficulty: 3,
          language: this.language,
          userId: this.getUserId()
        });

        if (response.data.success) {
          console.log('Quiz created successfully');
          this.$router.push('/');
        } else {
          alert(response.data.message);
        }
      } catch (error) {
        console.error('Error creating quiz:', error);
        alert('Failed to create quiz. Please try again.');
      }
    },
    getUserId() {
      return 1; // Replace with real user ID fetching logic
    },
  },
};
</script>