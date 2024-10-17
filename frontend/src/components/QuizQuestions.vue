  <template>
    <div class="max-w-3xl mx-auto p-6 bg-white rounded-lg shadow-md">
      <div v-for="(question, qIndex) in questions" :key="qIndex" class="mb-8 pb-6 border-b border-gray-200 last:border-b-0">
        <h2 class="text-xl font-bold mb-4 text-gray-800">{{ qIndex + 1 }}. {{ question.problem }}</h2>
        <div class="space-y-3">
          <div
            v-for="(option, index) in question.options"
            :key="index"
            class="flex items-center p-3 rounded-lg transition-colors duration-200 ease-in-out"
            :class="{'bg-blue-50': localAnswers[qIndex] === option}"
          >
            <input
              type="radio"
              :id="'option-' + qIndex + '-' + index"
              :name="'question-' + qIndex"
              :value="option"
              :checked="localAnswers[qIndex] === option"
              @change="updateAnswer(qIndex, option)"
              class="form-radio h-5 w-5 text-blue-600 transition duration-150 ease-in-out"
            />
            <label
              :for="'option-' + qIndex + '-' + index"
              class="ml-3 block text-sm font-medium text-gray-700 cursor-pointer flex-grow"
            >
              {{ option }}
            </label>
          </div>
        </div>
      </div>

      <button
        @click="submitAnswers"
        class="mt-6 w-full flex justify-center py-3 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 transition duration-150 ease-in-out"
      >
        Submit Quiz
      </button>
    </div>
  </template>

  <script>
  import { ref } from 'vue'

  export default {
    props: {
      questions: {
        type: Array,
        required: true,
      },
    },
    setup(props, { emit }) {
      const localAnswers = ref({});

      const updateAnswer = (questionIndex, selectedOption) => {
        localAnswers.value[questionIndex] = selectedOption;
        emit('update-answer', questionIndex, selectedOption);
      };

      const submitAnswers = () => {
        if (Object.keys(localAnswers.value).length !== props.questions.length) {
          alert('Please answer all questions before submitting.');
          return;
        }
        emit('submit-answers', localAnswers.value);
      };

      return {
        localAnswers,
        updateAnswer,
        submitAnswers
      };
    }
  };
  </script>

  <style scoped>
  .form-radio {
    appearance: none;
    -webkit-appearance: none;
    -moz-appearance: none;
    width: 1.2em;
    height: 1.2em;
    border: 2px solid #cbd5e0;
    border-radius: 50%;
    outline: none;
    transition: all 0.2s ease-in-out;
  }

  .form-radio:checked {
    border-color: #3b82f6;
    background-color: #3b82f6;
    background-image: url("data:image/svg+xml,%3csvg viewBox='0 0 16 16' fill='white' xmlns='http://www.w3.org/2000/svg'%3e%3ccircle cx='8' cy='8' r='3'/%3e%3c/svg%3e");
    background-size: 100% 100%;
    background-position: center;
    background-repeat: no-repeat;
  }

  .form-radio:focus {
    box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.5);
  }
  </style>
