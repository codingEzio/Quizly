
## Origin

- Apply sayings from [《认知天性》读后感](https://blog.laisky.com/p/make-it-stick/#gsc.tab=0)
- Credit to the original idea of the product [*Quizzio*](https://www.quizzio.app/)

## Design

### data

- user
  - id (non-null, INT, pk)
  - name (non-null, VARCHAR(50))
  - email (null, VARCHAR(500))
  - password (non-null, VARCHAR(1000))

- quiz_config
  - id (non-null, INT, pk)
  - title (non-null, VARCHAR(500))
  - context (non-null)
  - difficulty (non-null, ENUM(most easy, easy, medium, hard, most hard))

- quiz_content
  - quizId (non-null, INT, fk)
  - userId (non-null, INT, fk)
  - lang (non-null, ENUM(zh, en))
  - rawContent (nullable)
  - postProcessedContent (nullable)

### config

- system prompt
- examples for prompt

### backend

- API

    - [x] `auth/register/`
    - [x] `auth/login/`
    - [ ] `auth/logout/`
    - [ ] `quiz/new/`
    - [ ] `quiz/list/`
    - [ ] `quiz/detail/` (list of questions)

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

- 配置化 via [Apollo .NET Client](https://github.com/apolloconfig/apollo.net) <sup>[doc](https://github.com/apolloconfig/apollo/wiki/Apollo%E9%85%8D%E7%BD%AE%E4%B8%AD%E5%BF%83%E4%BB%8B%E7%BB%8D)</sup>
- Get a VPS for real-world deployment
- Apply tons of high-concurrency solutions to this project for the sake of learning.
