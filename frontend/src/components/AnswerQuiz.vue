<template>
  <div>
    <h1 class="text-3xl font-bold text-gray-900 mb-6">Answer Quiz</h1>
    <div class="bg-white shadow overflow-hidden sm:rounded-lg">
      <div class="px-4 py-5 sm:p-6">
        <h2 class="text-xl font-bold mb-4">{{ currentQuestion.text }}</h2>

        <div class="space-y-4">
          <div v-for="(option, index) in currentQuestion.options" :key="index" class="flex items-center">
            <input :id="'option-' + index" name="quiz-option" type="radio" :value="index" v-model="selectedOption" class="focus:ring-indigo-500 h-4 w-4 text-indigo-600 border-gray-300">
            <label :for="'option-' + index" class="ml-3 block text-sm font-medium text-gray-700">
              {{ option }}
            </label>
          </div>
        </div>

        <div class="mt-6 flex space-x-4">
          <button @click="nextQuestion" class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
            Next Question
          </button>
          <button @click="finishQuiz" class="inline-flex items-center px-4 py-2 border border-gray-300 shadow-sm text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
            Finish Quiz
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      currentQuestionIndex: 0,
      selectedOption: null,
      questions: [
        {
          text: "What is the capital of France?",
          options: ["London", "Berlin", "Paris", "Madrid"]
        },
        // Add more questions here
      ]
    };
  },
  computed: {
    currentQuestion() {
      return this.questions[this.currentQuestionIndex];
    }
  },
  methods: {
    nextQuestion() {
      if (this.currentQuestionIndex < this.questions.length - 1) {
        this.currentQuestionIndex++;
        this.selectedOption = null;
      } else {
        this.finishQuiz();
      }
    },
    finishQuiz() {
      // Here you would typically submit the answers and redirect to the results page
      this.$router.push('/end');
    }
  }
};
</script>
