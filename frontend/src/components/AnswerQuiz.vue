<template>
  <div>
    <h1 class="text-3xl font-bold text-gray-900 mb-6">Answer Quiz: {{ quiz.title }}</h1>
    <div v-if="loading" class="text-center">
      Loading quiz...
    </div>
    <div v-else-if="quizCompleted" class="bg-white shadow overflow-hidden sm:rounded-lg p-6">
      <h2 class="text-2xl font-bold mb-4">Quiz Results</h2>
      <p class="mb-2">Total Questions: {{ result.questionTotal }}</p>
      <p class="mb-4">Correct Answers: {{ result.correctCount }}</p>
      <h3 class="text-xl font-bold mb-2">Feedback:</h3>
      <div v-html="formattedSummary" class="whitespace-pre-wrap"></div>
    </div>
    <div v-else class="bg-white shadow overflow-hidden sm:rounded-lg">
      <quiz-questions
        v-if="quizQuestions.length"
        :questions="quizQuestions"
        @update-answer="updateAnswer"
        @submit-answers="submitAnswers"
      />
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import QuizQuestions from './QuizQuestions.vue';

export default {
  components: {
    QuizQuestions,
  },
  data() {
    return {
      quiz: {},
      quizQuestions: [],
      answers: {},
      result: {},
      loading: true,
      quizCompleted: false,
    };
  },
  computed: {
    formattedSummary() {
      return this.result.summary ? this.result.summary.replace(/\n/g, '<br>') : '';
    },
  },
  async created() {
    const quizId = this.$route.params.id;
    await this.fetchQuizDetails(quizId);
  },
  methods: {
    async fetchQuizDetails(quizId) {
      axios.defaults.baseURL = 'http://localhost:5041';

      try {
        const response = await axios.get(`/api/quiz/detail/${quizId}`);
        if (response.data.success) {
          this.quiz = response.data.data;
          this.quizQuestions = response.data.data.content;
        } else {
          alert(response.data.message);
        }
      } catch (error) {
        console.error('Error fetching quiz details:', error);
        alert('An error occurred while fetching quiz details. Please try again later.');
      } finally {
        this.loading = false;
      }
    },

    updateAnswer(questionId, selectedOption) {
      this.answers[questionId] = selectedOption;
    },

    async submitAnswers() {
      console.log('Submitting answers:', this.answers);
      console.log('Quiz ID:', this.quiz.metadata.total_q);
      console.log('QuizQuestions:', this.quizQuestions);

      if (Object.keys(this.answers).length !== this.quizQuestions.length) {
        alert('Please answer all questions before submitting.');
        return;
      }

      try {
        const answerDto = {
          userId: this.getUserId(),
          quizId: this.quiz.metadata.total_q,
          answers: this.quizQuestions.map((q, index) => ({
            questionId: index + 1,
            answer: this.answers[index],
          })),
        };

        console.log('Submitting DTO:', answerDto);

        const response = await axios.post('/api/quiz/answer', answerDto);
        if (response.data.success) {
          this.result = response.data.data;
          this.quizCompleted = true;
        } else {
          alert(response.data.message);
        }
      } catch (error) {
        console.error('Error submitting answers:', error);
        alert('An error occurred while submitting your answers. Please try again.');
      }
    },

    getUserId() {
      // Implement logic to fetch the actual user ID
      return 1; // Replace with actual logic
    }
  }
};
</script>

<style scoped>
/* Add styles specific to the AnswerQuiz component if needed */
</style>
