-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1
-- Время создания: Апр 14 2021 г., 12:00
-- Версия сервера: 10.4.11-MariaDB
-- Версия PHP: 7.4.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `po`
--

-- --------------------------------------------------------

--
-- Структура таблицы `conclusion`
--

CREATE TABLE `conclusion` (
  `conclusion_number` varchar(10) NOT NULL,
  `evaluation date` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `reason for rating` varchar(40) NOT NULL,
  `subject` text NOT NULL,
  `specification` text NOT NULL,
  `initiator` varchar(30) NOT NULL,
  `object` text NOT NULL,
  `result` varchar(40) NOT NULL,
  `price` int(11) NOT NULL,
  `sad` varchar(10) NOT NULL,
  `status` tinyint(4) NOT NULL DEFAULT 1,
  `letter` varchar(1) NOT NULL DEFAULT 'D',
  `exp` varchar(30) NOT NULL,
  `extra` text NOT NULL,
  `hide extra` text NOT NULL,
  `c1` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `license`
--

CREATE TABLE `license` (
  `id` int(11) NOT NULL,
  `conclusion_number` varchar(10) NOT NULL,
  `name` varchar(30) NOT NULL,
  `date_issue` date NOT NULL,
  `duration` varchar(30) NOT NULL,
  `extra` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `main`
--

CREATE TABLE `main` (
  `inn` varchar(12) NOT NULL,
  `conclusion_number` varchar(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `organisation`
--

CREATE TABLE `organisation` (
  `inn` varchar(12) NOT NULL,
  `name` text NOT NULL,
  `fact adress` text NOT NULL,
  `registration date` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `activity` varchar(30) NOT NULL,
  `legal adress` text NOT NULL,
  `email` varchar(30) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `leader` varchar(30) NOT NULL,
  `founder` text NOT NULL,
  `gendir` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `scoring`
--

CREATE TABLE `scoring` (
  `id` int(11) NOT NULL,
  `conclusion number` varchar(10) NOT NULL,
  `point` varchar(5) NOT NULL,
  `comment` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `name` varchar(30) NOT NULL,
  `password` varchar(30) NOT NULL,
  `letter` varchar(2) NOT NULL,
  `last` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id`, `name`, `password`, `letter`, `last`) VALUES
(1, 'admin', 'admin', 'D', 707);

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `conclusion`
--
ALTER TABLE `conclusion`
  ADD UNIQUE KEY `index` (`conclusion_number`);

--
-- Индексы таблицы `license`
--
ALTER TABLE `license`
  ADD PRIMARY KEY (`id`),
  ADD KEY `index` (`conclusion_number`) USING BTREE;

--
-- Индексы таблицы `main`
--
ALTER TABLE `main`
  ADD UNIQUE KEY `index` (`conclusion_number`);

--
-- Индексы таблицы `organisation`
--
ALTER TABLE `organisation`
  ADD UNIQUE KEY `inn` (`inn`) USING BTREE;

--
-- Индексы таблицы `scoring`
--
ALTER TABLE `scoring`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `license`
--
ALTER TABLE `license`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `scoring`
--
ALTER TABLE `scoring`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
