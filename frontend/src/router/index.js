import { createRouter, createWebHistory } from 'vue-router';
import Dashboard from '../components/DashboardPage.vue';
import EndPage from '../components/EndPage.vue';
import CreateQuiz from '../components/CreateQuiz.vue';
import AnswerQuiz from '../components/AnswerQuiz.vue';
import Settings from '../components/SettingsPage.vue';

const routes = [
  { path: '/', component: Dashboard },
  { path: '/end', component: EndPage },
  { path: '/create', component: CreateQuiz },
  { path: '/answer/:id', component: AnswerQuiz },
  { path: '/settings', component: Settings },
  {
    path: '/quiz/:id',
    name: 'AnswerQuiz',
    component: AnswerQuiz
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
