CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `quiz_config` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
    `context` longtext CHARACTER SET utf8mb4 NOT NULL,
    `difficulty` int NOT NULL,
    CONSTRAINT `PK_quiz_config` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `user` (
    `id` int NOT NULL AUTO_INCREMENT,
    `name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `email` varchar(500) CHARACTER SET utf8mb4 NULL,
    `password` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_user` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `quiz_content` (
    `quizId` int NOT NULL,
    `userId` int NOT NULL,
    `lang` int NOT NULL,
    `rawContent` longtext CHARACTER SET utf8mb4 NULL,
    `postProcessedContent` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_quiz_content` PRIMARY KEY (`quizId`, `userId`),
    CONSTRAINT `FK_quiz_content_quiz_config_quizId` FOREIGN KEY (`quizId`) REFERENCES `quiz_config` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_quiz_content_user_userId` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_quiz_content_userId` ON `quiz_content` (`userId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241015104354_InitWithUserQuizConfigNContent', '8.0.0');

COMMIT;