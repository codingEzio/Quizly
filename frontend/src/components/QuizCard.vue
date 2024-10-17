<template>
  <div class="bg-white overflow-hidden shadow rounded-lg">
    <div class="px-4 py-5 sm:p-6">
      <h3 class="text-lg leading-6 font-medium text-gray-900">{{ quiz.title }}</h3>
      <div class="mt-2 max-w-xl text-sm text-gray-500">
        <p>Difficulty: {{ difficultyLabel }}</p>
      </div>
    </div>
    <div class="px-4 py-4 sm:px-6 flex justify-between items-center">
      <router-link
        :to="'/quiz/' + quiz.id"
        class="inline-flex items-center px-3 py-2 border border-transparent text-sm leading-4 font-medium rounded-md shadow-sm text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
      >
        Start Quiz
      </router-link>
    </div>
  </div>
</template>

<script>
import { getDifficultyLabel } from '@/utils/difficultyMapping';

export default {
  props: {
    quiz: {
      type: Object,
      required: true,
    },
  },
  computed: {
    difficultyLabel() {
      return getDifficultyLabel(this.quiz.difficulty);
    }
  },
};
</script>
