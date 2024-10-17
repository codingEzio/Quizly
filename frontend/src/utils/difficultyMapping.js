// src/utils/difficultyMapping.js

export const difficultyMap = {
  0: 'Very Easy',
  1: 'Easy',
  2: 'Medium',
  3: 'Hard',
  4: 'Very Hard'
};

export function getDifficultyLabel(difficulty) {
  return difficultyMap[difficulty] || difficulty;
}