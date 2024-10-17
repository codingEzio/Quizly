<template>
  <div>
    <h1 class="text-3xl font-bold text-gray-900 mb-6">Quiz Results</h1>
    <div class="bg-white shadow overflow-hidden sm:rounded-lg">
      <div class="px-4 py-5 sm:p-6">
        <h2 class="text-xl font-bold mb-4">You got {{ score }} out of {{ totalQuestions }} correct!</h2>

        <div class="mt-6 space-y-4">
          <h3 class="font-bold text-lg">AI Analysis</h3>
          <p>For Q1, you chose option {{ userAnswers[0] }}. The correct answer was {{ correctAnswers[0] }}.</p>
          <p>For Q2, you chose option {{ userAnswers[1] }}. The correct answer was {{ correctAnswers[1] }}.</p>
          <!-- Add more analysis here -->
        </div>

        <div class="mt-8 flex space-x-4">
          <router-link to="/answer" class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
            Take Another Quiz
          </router-link>
          <router-link to="/" class="inline-flex items-center px-4 py-2 border border-gray-300 shadow-sm text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
            Back to Dashboard
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      score: 3,
      totalQuestions: 5,
      userAnswers: [1, 2],
      correctAnswers: [3, 2]
    };
  }
};
</script>
