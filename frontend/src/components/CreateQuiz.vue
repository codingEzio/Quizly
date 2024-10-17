<template>
  <div>
    <h1 class="text-3xl font-bold text-gray-900 mb-6">Create Quiz</h1>
    <div class="bg-white shadow overflow-hidden sm:rounded-lg">
      <div class="px-4 py-5 sm:p-6">
        <div class="mb-4">
          <label for="quiz-name" class="block text-sm font-medium text-gray-700">Quiz Name</label>
          <input type="text" name="quiz-name" id="quiz-name" class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md" v-model="quizName" />
        </div>

        <div class="mb-4">
          <label for="context" class="block text-sm font-medium text-gray-700">Context</label>
          <textarea id="context" name="context" rows="3" class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md" v-model="context"></textarea>
        </div>

        <div class="mb-4">
          <label for="difficulty" class="block text-sm font-medium text-gray-700">Difficulty Level</label>
          <input type="range" min="1" max="10" class="w-full" v-model="difficulty" />
          <div class="text-sm text-gray-500 mt-1">Level: {{ difficulty }}</div>
        </div>

        <button @click="createQuiz" class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
          Create Quiz
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      quizName: '',
      context: '',
      difficulty: 5,
    };
  },
  methods: {
    createQuiz() {
      console.log('Quiz created:', this.quizName, this.context, this.difficulty);
      // Here you would typically send the data to your backend
      // Then redirect to the dashboard or the newly created quiz
      this.$router.push('/');
    },
  },
};
</script>
