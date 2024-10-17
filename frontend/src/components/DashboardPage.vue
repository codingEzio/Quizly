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

export default {
  name: 'DashboardPage',
  components: {
    QuizCard,
  },
  data() {
    return {
      quizzes: [
        { id: 1, name: 'Quiz 1', created: '2023-10-01' },
        { id: 2, name: 'Quiz 2', created: '2023-10-02' },
        { id: 3, name: 'Quiz 3', created: '2023-10-03' },
        { id: 4, name: 'Quiz 4', created: '2023-10-04' },
        { id: 5, name: 'Quiz 5', created: '2023-10-05' },
      ],
    };
  },
};
</script>
