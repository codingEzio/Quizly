
## Origin

- Apply sayings from [《认知天性》读后感](https://blog.laisky.com/p/make-it-stick/#gsc.tab=0)
- Credit to the original idea of the product [*Quizzio*](https://www.quizzio.app/)

## Design

### data

- user
  - id
  - name
  - email
  - password

- quiz
  - id
  - title
  - context
  - difficulty

### config

- system prompt
- examples for prompt

### backend

- API

    - `auth/register/`
    - `auth/login/`
    - `auth/logout/`
    - `quiz/new/`
    - `quiz/list/`
    - `quiz/detail/` (list of questions)

- Util

    - LLM helper
    - DB helper

### frontend

- page settings

    - user information
    - actions (logout)

- page quizzes

    - list of quizzes (quiz info)

- page dashboard

    - create new quiz
    - quiz statistics
    - recent quizzes

- interaction

    - answering the quiz
    - quiz statistics (n/m correct answers)
    - quiz AI feedback
    - quiz question/answer pair demonstration (wrong/right)

-----

## Roadmap

- Get a VPS for real-world deployment
- Apply tons of high-concurrency solutions to this project for the sake of learning.
