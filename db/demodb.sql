-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 28. Feb 2023 um 22:18
-- Server-Version: 10.4.22-MariaDB
-- PHP-Version: 8.0.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `demodb`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `all_cards`
--

CREATE TABLE `all_cards` (
  `card_id` varchar(50) NOT NULL,
  `card_name` varchar(50) NOT NULL,
  `damage` float NOT NULL,
  `card_type` varchar(50) NOT NULL,
  `element` varchar(50) NOT NULL,
  `species` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `all_cards`
--

INSERT INTO `all_cards` (`card_id`, `card_name`, `damage`, `card_type`, `element`, `species`) VALUES
('02a9c76e-b17d-427f-9240-2dd49b0d3bfd', 'RegularSpell', 45, 'spell', 'normal', 'magic'),
('166c1fd5-4dcb-41a8-91cb-f45dcd57cef3', 'Knight', 22, 'monster', 'normal', 'knight'),
('171f6076-4eb5-4a7d-b3f2-2d650cc3d237', 'RegularSpell', 28, 'spell', 'normal', 'magic'),
('1cb6ab86-bdb2-47e5-b6e4-68c5ab389334', 'Ork', 45, 'monster', 'normal', 'ork'),
('1d3f175b-c067-4359-989d-96562bfa382c', 'Ork', 40, 'monster', 'normal', 'ork'),
('2272ba48-6662-404d-a9a1-41a9bed316d9', 'WaterGoblin', 11, 'monster', 'water', 'goblin'),
('237dbaef-49e3-4c23-b64b-abf5c087b276', 'WaterSpell', 40, 'spell', 'water', 'magic'),
('2508bf5c-20d7-43b4-8c77-bc677decadef', 'FireElf', 25, 'monster', 'fire', 'fireelf'),
('27051a20-8580-43ff-a473-e986b52f297a', 'FireElf', 28, 'monster', 'fire', 'fireelf'),
('2c98cd06-518b-464c-b911-8d787216cddd', 'WaterSpell', 21, 'spell', 'water', 'magic'),
('3871d45b-b630-4a0d-8bc6-a5fc56b6a043', 'Dragon', 70, 'monster', 'normal', 'dragon'),
('44c82fbc-ef6d-44ab-8c7a-9fb19a0e7c6e', 'Dragon', 55, 'monster', 'normal', 'dragon'),
('4a2757d6-b1c3-47ac-b9a3-91deab093531', 'Dragon', 55, 'monster', 'normal', 'dragon'),
('4ec8b269-0dfa-4f97-809a-2c63fe2a0025', 'Ork', 55, 'monster', 'normal', 'ork'),
('55ef46c4-016c-4168-bc43-6b9b1e86414f', 'WaterSpell', 20, 'spell', 'water', 'magic'),
('644808c2-f87a-4600-b313-122b02322fd5', 'WaterGoblin', 9, 'monster', 'water', 'goblin'),
('65ff5f23-1e70-4b79-b3bd-f6eb679dd3b5', 'Dragon', 50, 'monster', 'normal', 'dragon'),
('67f9048f-99b8-4ae4-b866-d8008d00c53d', 'WaterGoblin', 10, 'monster', 'water', 'goblin'),
('70962948-2bf7-44a9-9ded-8c68eeac7793', 'WaterGoblin', 9, 'monster', 'water', 'goblin'),
('74635fae-8ad3-4295-9139-320ab89c2844', 'FireSpell', 55, 'spell', 'fire', 'magic'),
('845f0dc7-37d0-426e-994e-43fc3ac83c08', 'WaterGoblin', 10, 'monster', 'water', 'goblin'),
('84d276ee-21ec-4171-a509-c1b88162831c', 'RegularSpell', 28, 'spell', 'normal', 'magic'),
('88221cfe-1f84-41b9-8152-8e36c6a354de', 'WaterSpell', 22, 'spell', 'water', 'magic'),
('8c20639d-6400-4534-bd0f-ae563f11f57a', 'WaterSpell', 25, 'spell', 'water', 'magic'),
('91a6471b-1426-43f6-ad65-6fc473e16f9f', 'WaterSpell', 21, 'spell', 'water', 'magic'),
('951e886a-0fbf-425d-8df5-af2ee4830d85', 'Ork', 55, 'monster', 'normal', 'ork'),
('99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'Dragon', 50, 'monster', 'normal', 'dragon'),
('9e8238a4-8a7a-487f-9f7d-a8c97899eb48', 'Dragon', 70, 'monster', 'normal', 'dragon'),
('a1618f1e-4f4c-4e09-9647-87e16f1edd2d', 'FireElf', 23, 'monster', 'fire', 'fireelf'),
('a6fde738-c65a-4b10-b400-6fef0fdb28ba', 'FireSpell', 55, 'spell', 'fire', 'magic'),
('aa9999a0-734c-49c6-8f4a-651864b14e62', 'RegularSpell', 50, 'spell', 'normal', 'magic'),
('b017ee50-1c14-44e2-bfd6-2c0c5653a37c', 'WaterGoblin', 11, 'monster', 'water', 'goblin'),
('b2237eca-0271-43bd-87f6-b22f70d42ca4', 'WaterGoblin', 11, 'monster', 'water', 'goblin'),
('ce6bcaee-47e1-4011-a49e-5a4d7d4245f3', 'Knight', 21, 'monster', 'normal', 'knight'),
('d04b736a-e874-4137-b191-638e0ff3b4e7', 'Dragon', 70, 'monster', 'normal', 'dragon'),
('d60e23cf-2238-4d49-844f-c7589ee5342e', 'WaterSpell', 22, 'spell', 'water', 'magic'),
('d6e9c720-9b5a-40c7-a6b2-bc34752e3463', 'Knight', 20, 'monster', 'normal', 'knight'),
('d7d0cb94-2cbf-4f97-8ccf-9933dc5354b8', 'WaterGoblin', 9, 'monster', 'water', 'goblin'),
('dcd93250-25a7-4dca-85da-cad2789f7198', 'FireSpell', 23, 'spell', 'fire', 'magic'),
('dfdd758f-649c-40f9-ba3a-8657f4b3439f', 'FireSpell', 25, 'spell', 'fire', 'magic'),
('drei', 'SPELLFIRE', 20, 'spell', 'fire', 'magic'),
('e85e3976-7c86-4d06-9a80-641c2019a79f', 'WaterSpell', 20, 'spell', 'water', 'magic'),
('ed1dc1bc-f0aa-4a0c-8d43-1402189b33c8', 'WaterGoblin', 10, 'monster', 'water', 'goblin'),
('f3fad0f2-a1af-45df-b80d-2e48825773d9', 'Ork', 45, 'monster', 'normal', 'ork'),
('f8043c23-1534-4487-b66b-238e0c3c39b5', 'WaterSpell', 23, 'spell', 'water', 'magic'),
('fc305a7a-36f7-4d30-ad27-462ca0445649', 'Ork', 40, 'monster', 'normal', 'ork'),
('five', 'One1firstonewizzard', 25, 'monster', 'normal', 'wizzard'),
('four', 'Dark Magician Fire Dragon', 45, 'monster', 'fire', 'dragon'),
('one', 'Kuribohgoblin', 10, 'monster', 'normal', 'goblin'),
('two', 'elfwateRknight', 50, 'monster', 'water', 'knight');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `available_cards`
--

CREATE TABLE `available_cards` (
  `id` int(11) NOT NULL,
  `username` varchar(20) NOT NULL,
  `card_id` varchar(50) NOT NULL,
  `card_name` varchar(50) NOT NULL,
  `damage` int(11) NOT NULL,
  `card_type` varchar(50) NOT NULL,
  `element` varchar(50) NOT NULL,
  `species` varchar(50) NOT NULL,
  `used` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `available_cards`
--

INSERT INTO `available_cards` (`id`, `username`, `card_id`, `card_name`, `damage`, `card_type`, `element`, `species`, `used`) VALUES
(98, 'kienboec', 'b017ee50-1c14-44e2-bfd6-2c0c5653a37c', 'WaterGoblin', 11, 'monster', 'water', 'goblin', 'false'),
(99, 'kienboec', 'd04b736a-e874-4137-b191-638e0ff3b4e7', 'Dragon', 70, 'monster', 'normal', 'dragon', 'false'),
(100, 'kienboec', '88221cfe-1f84-41b9-8152-8e36c6a354de', 'WaterSpell', 22, 'spell', 'water', 'magic', 'false'),
(101, 'kienboec', '1d3f175b-c067-4359-989d-96562bfa382c', 'Ork', 40, 'monster', 'normal', 'ork', 'false'),
(102, 'kienboec', '171f6076-4eb5-4a7d-b3f2-2d650cc3d237', 'RegularSpell', 28, 'spell', 'normal', 'magic', 'false'),
(103, 'kienboec', 'b2237eca-0271-43bd-87f6-b22f70d42ca4', 'WaterGoblin', 11, 'monster', 'water', 'goblin', 'false'),
(104, 'kienboec', '9e8238a4-8a7a-487f-9f7d-a8c97899eb48', 'Dragon', 70, 'monster', 'normal', 'dragon', 'false'),
(105, 'kienboec', 'd60e23cf-2238-4d49-844f-c7589ee5342e', 'WaterSpell', 22, 'spell', 'water', 'magic', 'false'),
(106, 'kienboec', 'fc305a7a-36f7-4d30-ad27-462ca0445649', 'Ork', 40, 'monster', 'normal', 'ork', 'false'),
(107, 'kienboec', '84d276ee-21ec-4171-a509-c1b88162831c', 'RegularSpell', 28, 'spell', 'normal', 'magic', 'false'),
(108, 'kienboec', '67f9048f-99b8-4ae4-b866-d8008d00c53d', 'WaterGoblin', 10, 'monster', 'water', 'goblin', 'false'),
(109, 'kienboec', 'aa9999a0-734c-49c6-8f4a-651864b14e62', 'RegularSpell', 50, 'spell', 'normal', 'magic', 'false'),
(110, 'kienboec', 'd6e9c720-9b5a-40c7-a6b2-bc34752e3463', 'Knight', 20, 'monster', 'normal', 'knight', 'false'),
(111, 'kienboec', '02a9c76e-b17d-427f-9240-2dd49b0d3bfd', 'RegularSpell', 45, 'spell', 'normal', 'magic', 'false'),
(112, 'kienboec', '2508bf5c-20d7-43b4-8c77-bc677decadef', 'FireElf', 25, 'monster', 'fire', 'fireelf', 'false'),
(113, 'kienboec', 'd7d0cb94-2cbf-4f97-8ccf-9933dc5354b8', 'WaterGoblin', 9, 'monster', 'water', 'goblin', 'false'),
(114, 'kienboec', '44c82fbc-ef6d-44ab-8c7a-9fb19a0e7c6e', 'Dragon', 55, 'monster', 'normal', 'dragon', 'false'),
(115, 'kienboec', '2c98cd06-518b-464c-b911-8d787216cddd', 'WaterSpell', 21, 'spell', 'water', 'magic', 'false'),
(116, 'kienboec', '951e886a-0fbf-425d-8df5-af2ee4830d85', 'Ork', 55, 'monster', 'normal', 'ork', 'false'),
(117, 'kienboec', 'dcd93250-25a7-4dca-85da-cad2789f7198', 'FireSpell', 23, 'spell', 'fire', 'magic', 'false'),
(118, 'kienboec', '845f0dc7-37d0-426e-994e-43fc3ac83c08', 'WaterGoblin', 10, 'monster', 'water', 'goblin', 'false'),
(119, 'kienboec', '99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'Dragon', 50, 'monster', 'normal', 'dragon', 'false'),
(120, 'kienboec', 'e85e3976-7c86-4d06-9a80-641c2019a79f', 'WaterSpell', 20, 'spell', 'water', 'magic', 'false'),
(121, 'kienboec', '1cb6ab86-bdb2-47e5-b6e4-68c5ab389334', 'Ork', 45, 'monster', 'normal', 'ork', 'false'),
(122, 'kienboec', 'dfdd758f-649c-40f9-ba3a-8657f4b3439f', 'FireSpell', 25, 'spell', 'fire', 'magic', 'false'),
(123, 'kienboec', '2272ba48-6662-404d-a9a1-41a9bed316d9', 'WaterGoblin', 11, 'monster', 'water', 'goblin', 'false'),
(124, 'kienboec', '3871d45b-b630-4a0d-8bc6-a5fc56b6a043', 'Dragon', 70, 'monster', 'normal', 'dragon', 'false'),
(125, 'kienboec', '166c1fd5-4dcb-41a8-91cb-f45dcd57cef3', 'Knight', 22, 'monster', 'normal', 'knight', 'false'),
(126, 'kienboec', '237dbaef-49e3-4c23-b64b-abf5c087b276', 'WaterSpell', 40, 'spell', 'water', 'magic', 'false'),
(127, 'kienboec', '27051a20-8580-43ff-a473-e986b52f297a', 'FireElf', 28, 'monster', 'fire', 'fireelf', 'false'),
(128, 'altenhof', 'b017ee50-1c14-44e2-bfd6-2c0c5653a37c', 'WaterGoblin', 11, 'monster', 'water', 'goblin', 'false'),
(129, 'altenhof', 'd04b736a-e874-4137-b191-638e0ff3b4e7', 'Dragon', 70, 'monster', 'normal', 'dragon', 'false'),
(130, 'altenhof', '88221cfe-1f84-41b9-8152-8e36c6a354de', 'WaterSpell', 22, 'spell', 'water', 'magic', 'false'),
(131, 'altenhof', '1d3f175b-c067-4359-989d-96562bfa382c', 'Ork', 40, 'monster', 'normal', 'ork', 'false'),
(132, 'altenhof', '171f6076-4eb5-4a7d-b3f2-2d650cc3d237', 'RegularSpell', 28, 'spell', 'normal', 'magic', 'false'),
(133, 'altenhof', '644808c2-f87a-4600-b313-122b02322fd5', 'WaterGoblin', 9, 'monster', 'water', 'goblin', 'false'),
(134, 'altenhof', '4a2757d6-b1c3-47ac-b9a3-91deab093531', 'Dragon', 55, 'monster', 'normal', 'dragon', 'false'),
(135, 'altenhof', '91a6471b-1426-43f6-ad65-6fc473e16f9f', 'WaterSpell', 21, 'spell', 'water', 'magic', 'false'),
(136, 'altenhof', '4ec8b269-0dfa-4f97-809a-2c63fe2a0025', 'Ork', 55, 'monster', 'normal', 'ork', 'false'),
(137, 'altenhof', 'f8043c23-1534-4487-b66b-238e0c3c39b5', 'WaterSpell', 23, 'spell', 'water', 'magic', 'false'),
(138, 'altenhof', 'ed1dc1bc-f0aa-4a0c-8d43-1402189b33c8', 'WaterGoblin', 10, 'monster', 'water', 'goblin', 'false'),
(139, 'altenhof', '65ff5f23-1e70-4b79-b3bd-f6eb679dd3b5', 'Dragon', 50, 'monster', 'normal', 'dragon', 'false'),
(140, 'altenhof', '55ef46c4-016c-4168-bc43-6b9b1e86414f', 'WaterSpell', 20, 'spell', 'water', 'magic', 'false'),
(141, 'altenhof', 'f3fad0f2-a1af-45df-b80d-2e48825773d9', 'Ork', 45, 'monster', 'normal', 'ork', 'false'),
(142, 'altenhof', '8c20639d-6400-4534-bd0f-ae563f11f57a', 'WaterSpell', 25, 'spell', 'water', 'magic', 'false'),
(143, 'altenhof', '67f9048f-99b8-4ae4-b866-d8008d00c53d', 'WaterGoblin', 10, 'monster', 'water', 'goblin', 'false'),
(144, 'altenhof', 'aa9999a0-734c-49c6-8f4a-651864b14e62', 'RegularSpell', 50, 'spell', 'normal', 'magic', 'false'),
(145, 'altenhof', 'd6e9c720-9b5a-40c7-a6b2-bc34752e3463', 'Knight', 20, 'monster', 'normal', 'knight', 'false'),
(146, 'altenhof', '02a9c76e-b17d-427f-9240-2dd49b0d3bfd', 'RegularSpell', 45, 'spell', 'normal', 'magic', 'false'),
(147, 'altenhof', '2508bf5c-20d7-43b4-8c77-bc677decadef', 'FireElf', 25, 'monster', 'fire', 'fireelf', 'false'),
(148, 'altenhof', '70962948-2bf7-44a9-9ded-8c68eeac7793', 'WaterGoblin', 9, 'monster', 'water', 'goblin', 'false'),
(149, 'altenhof', '74635fae-8ad3-4295-9139-320ab89c2844', 'FireSpell', 55, 'spell', 'fire', 'magic', 'false'),
(150, 'altenhof', 'ce6bcaee-47e1-4011-a49e-5a4d7d4245f3', 'Knight', 21, 'monster', 'normal', 'knight', 'false'),
(151, 'altenhof', 'a6fde738-c65a-4b10-b400-6fef0fdb28ba', 'FireSpell', 55, 'spell', 'fire', 'magic', 'false'),
(152, 'altenhof', 'a1618f1e-4f4c-4e09-9647-87e16f1edd2d', 'FireElf', 23, 'monster', 'fire', 'fireelf', 'false'),
(153, 'altenhof', 'b2237eca-0271-43bd-87f6-b22f70d42ca4', 'WaterGoblin', 11, 'monster', 'water', 'goblin', 'false'),
(154, 'altenhof', '9e8238a4-8a7a-487f-9f7d-a8c97899eb48', 'Dragon', 70, 'monster', 'normal', 'dragon', 'false'),
(155, 'altenhof', 'd60e23cf-2238-4d49-844f-c7589ee5342e', 'WaterSpell', 22, 'spell', 'water', 'magic', 'false'),
(156, 'altenhof', 'fc305a7a-36f7-4d30-ad27-462ca0445649', 'Ork', 40, 'monster', 'normal', 'ork', 'false'),
(157, 'altenhof', '84d276ee-21ec-4171-a509-c1b88162831c', 'RegularSpell', 28, 'spell', 'normal', 'magic', 'false'),
(158, 'altenhof', 'd7d0cb94-2cbf-4f97-8ccf-9933dc5354b8', 'WaterGoblin', 9, 'monster', 'water', 'goblin', 'false'),
(159, 'altenhof', '44c82fbc-ef6d-44ab-8c7a-9fb19a0e7c6e', 'Dragon', 55, 'monster', 'normal', 'dragon', 'false'),
(160, 'altenhof', '2c98cd06-518b-464c-b911-8d787216cddd', 'WaterSpell', 21, 'spell', 'water', 'magic', 'false'),
(161, 'altenhof', '951e886a-0fbf-425d-8df5-af2ee4830d85', 'Ork', 55, 'monster', 'normal', 'ork', 'false'),
(162, 'altenhof', 'dcd93250-25a7-4dca-85da-cad2789f7198', 'FireSpell', 23, 'spell', 'fire', 'magic', 'false');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `client`
--

CREATE TABLE `client` (
  `id` int(11) NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `coins` int(11) NOT NULL,
  `stats` int(11) NOT NULL,
  `bio` varchar(50) NOT NULL,
  `image` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `client`
--

INSERT INTO `client` (`id`, `username`, `password`, `coins`, `stats`, `bio`, `image`) VALUES
(9, 'kienboec', 'daniel', 0, 75, 'me playin...', ':-)'),
(11, 'admin', 'istrator', 20, 100, '', ''),
(18, 'altenhof', 'markus', 15, 100, ' ', ' ');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `deck`
--

CREATE TABLE `deck` (
  `deck_id` int(11) NOT NULL,
  `username` varchar(20) NOT NULL,
  `card_id1` varchar(50) NOT NULL,
  `card_id2` varchar(50) NOT NULL,
  `card_id3` varchar(50) NOT NULL,
  `card_id4` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `deck`
--

INSERT INTO `deck` (`deck_id`, `username`, `card_id1`, `card_id2`, `card_id3`, `card_id4`) VALUES
(1, 'kienboec', '845f0dc7-37d0-426e-994e-43fc3ac83c08', '99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'e85e3976-7c86-4d06-9a80-641c2019a79f', '171f6076-4eb5-4a7d-b3f2-2d650cc3d237'),
(2, 'altenhof', 'aa9999a0-734c-49c6-8f4a-651864b14e62', 'd6e9c720-9b5a-40c7-a6b2-bc34752e3463', 'd60e23cf-2238-4d49-844f-c7589ee5342e', '02a9c76e-b17d-427f-9240-2dd49b0d3bfd');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `packages`
--

CREATE TABLE `packages` (
  `package_id` int(11) NOT NULL,
  `card_id1` varchar(50) NOT NULL,
  `card_id2` varchar(50) NOT NULL,
  `card_id3` varchar(50) NOT NULL,
  `card_id4` varchar(50) NOT NULL,
  `card_id5` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `packages`
--

INSERT INTO `packages` (`package_id`, `card_id1`, `card_id2`, `card_id3`, `card_id4`, `card_id5`) VALUES
(21, '845f0dc7-37d0-426e-994e-43fc3ac83c08', '99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'e85e3976-7c86-4d06-9a80-641c2019a79f', '1cb6ab86-bdb2-47e5-b6e4-68c5ab389334', 'dfdd758f-649c-40f9-ba3a-8657f4b3439f'),
(25, 'd7d0cb94-2cbf-4f97-8ccf-9933dc5354b8', '44c82fbc-ef6d-44ab-8c7a-9fb19a0e7c6e', '2c98cd06-518b-464c-b911-8d787216cddd', '951e886a-0fbf-425d-8df5-af2ee4830d85', 'dcd93250-25a7-4dca-85da-cad2789f7198'),
(28, '70962948-2bf7-44a9-9ded-8c68eeac7793', '74635fae-8ad3-4295-9139-320ab89c2844', 'ce6bcaee-47e1-4011-a49e-5a4d7d4245f3', 'a6fde738-c65a-4b10-b400-6fef0fdb28ba', 'a1618f1e-4f4c-4e09-9647-87e16f1edd2d'),
(29, '2272ba48-6662-404d-a9a1-41a9bed316d9', '3871d45b-b630-4a0d-8bc6-a5fc56b6a043', '166c1fd5-4dcb-41a8-91cb-f45dcd57cef3', '237dbaef-49e3-4c23-b64b-abf5c087b276', '27051a20-8580-43ff-a473-e986b52f297a'),
(30, '845f0dc7-37d0-426e-994e-43fc3ac83c08', '99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'e85e3976-7c86-4d06-9a80-641c2019a79f', '1cb6ab86-bdb2-47e5-b6e4-68c5ab389334', 'dfdd758f-649c-40f9-ba3a-8657f4b3439f'),
(31, '644808c2-f87a-4600-b313-122b02322fd5', '4a2757d6-b1c3-47ac-b9a3-91deab093531', '91a6471b-1426-43f6-ad65-6fc473e16f9f', '4ec8b269-0dfa-4f97-809a-2c63fe2a0025', 'f8043c23-1534-4487-b66b-238e0c3c39b5'),
(32, 'b017ee50-1c14-44e2-bfd6-2c0c5653a37c', 'd04b736a-e874-4137-b191-638e0ff3b4e7', '88221cfe-1f84-41b9-8152-8e36c6a354de', '1d3f175b-c067-4359-989d-96562bfa382c', '171f6076-4eb5-4a7d-b3f2-2d650cc3d237'),
(33, 'ed1dc1bc-f0aa-4a0c-8d43-1402189b33c8', '65ff5f23-1e70-4b79-b3bd-f6eb679dd3b5', '55ef46c4-016c-4168-bc43-6b9b1e86414f', 'f3fad0f2-a1af-45df-b80d-2e48825773d9', '8c20639d-6400-4534-bd0f-ae563f11f57a'),
(34, 'd7d0cb94-2cbf-4f97-8ccf-9933dc5354b8', '44c82fbc-ef6d-44ab-8c7a-9fb19a0e7c6e', '2c98cd06-518b-464c-b911-8d787216cddd', '951e886a-0fbf-425d-8df5-af2ee4830d85', 'dcd93250-25a7-4dca-85da-cad2789f7198'),
(36, '67f9048f-99b8-4ae4-b866-d8008d00c53d', 'aa9999a0-734c-49c6-8f4a-651864b14e62', 'd6e9c720-9b5a-40c7-a6b2-bc34752e3463', '02a9c76e-b17d-427f-9240-2dd49b0d3bfd', '2508bf5c-20d7-43b4-8c77-bc677decadef'),
(38, '2272ba48-6662-404d-a9a1-41a9bed316d9', '3871d45b-b630-4a0d-8bc6-a5fc56b6a043', '166c1fd5-4dcb-41a8-91cb-f45dcd57cef3', '237dbaef-49e3-4c23-b64b-abf5c087b276', '27051a20-8580-43ff-a473-e986b52f297a'),
(39, '845f0dc7-37d0-426e-994e-43fc3ac83c08', '99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'e85e3976-7c86-4d06-9a80-641c2019a79f', '1cb6ab86-bdb2-47e5-b6e4-68c5ab389334', 'dfdd758f-649c-40f9-ba3a-8657f4b3439f'),
(40, '644808c2-f87a-4600-b313-122b02322fd5', '4a2757d6-b1c3-47ac-b9a3-91deab093531', '91a6471b-1426-43f6-ad65-6fc473e16f9f', '4ec8b269-0dfa-4f97-809a-2c63fe2a0025', 'f8043c23-1534-4487-b66b-238e0c3c39b5'),
(42, 'ed1dc1bc-f0aa-4a0c-8d43-1402189b33c8', '65ff5f23-1e70-4b79-b3bd-f6eb679dd3b5', '55ef46c4-016c-4168-bc43-6b9b1e86414f', 'f3fad0f2-a1af-45df-b80d-2e48825773d9', '8c20639d-6400-4534-bd0f-ae563f11f57a'),
(44, 'b2237eca-0271-43bd-87f6-b22f70d42ca4', '9e8238a4-8a7a-487f-9f7d-a8c97899eb48', 'd60e23cf-2238-4d49-844f-c7589ee5342e', 'fc305a7a-36f7-4d30-ad27-462ca0445649', '84d276ee-21ec-4171-a509-c1b88162831c'),
(45, '67f9048f-99b8-4ae4-b866-d8008d00c53d', 'aa9999a0-734c-49c6-8f4a-651864b14e62', 'd6e9c720-9b5a-40c7-a6b2-bc34752e3463', '02a9c76e-b17d-427f-9240-2dd49b0d3bfd', '2508bf5c-20d7-43b4-8c77-bc677decadef'),
(46, '70962948-2bf7-44a9-9ded-8c68eeac7793', '74635fae-8ad3-4295-9139-320ab89c2844', 'ce6bcaee-47e1-4011-a49e-5a4d7d4245f3', 'a6fde738-c65a-4b10-b400-6fef0fdb28ba', 'a1618f1e-4f4c-4e09-9647-87e16f1edd2d'),
(47, '2272ba48-6662-404d-a9a1-41a9bed316d9', '3871d45b-b630-4a0d-8bc6-a5fc56b6a043', '166c1fd5-4dcb-41a8-91cb-f45dcd57cef3', '237dbaef-49e3-4c23-b64b-abf5c087b276', '27051a20-8580-43ff-a473-e986b52f297a'),
(48, '845f0dc7-37d0-426e-994e-43fc3ac83c08', '99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'e85e3976-7c86-4d06-9a80-641c2019a79f', '1cb6ab86-bdb2-47e5-b6e4-68c5ab389334', 'dfdd758f-649c-40f9-ba3a-8657f4b3439f'),
(49, '644808c2-f87a-4600-b313-122b02322fd5', '4a2757d6-b1c3-47ac-b9a3-91deab093531', '91a6471b-1426-43f6-ad65-6fc473e16f9f', '4ec8b269-0dfa-4f97-809a-2c63fe2a0025', 'f8043c23-1534-4487-b66b-238e0c3c39b5'),
(50, 'b017ee50-1c14-44e2-bfd6-2c0c5653a37c', 'd04b736a-e874-4137-b191-638e0ff3b4e7', '88221cfe-1f84-41b9-8152-8e36c6a354de', '1d3f175b-c067-4359-989d-96562bfa382c', '171f6076-4eb5-4a7d-b3f2-2d650cc3d237'),
(51, 'ed1dc1bc-f0aa-4a0c-8d43-1402189b33c8', '65ff5f23-1e70-4b79-b3bd-f6eb679dd3b5', '55ef46c4-016c-4168-bc43-6b9b1e86414f', 'f3fad0f2-a1af-45df-b80d-2e48825773d9', '8c20639d-6400-4534-bd0f-ae563f11f57a'),
(52, 'd7d0cb94-2cbf-4f97-8ccf-9933dc5354b8', '44c82fbc-ef6d-44ab-8c7a-9fb19a0e7c6e', '2c98cd06-518b-464c-b911-8d787216cddd', '951e886a-0fbf-425d-8df5-af2ee4830d85', 'dcd93250-25a7-4dca-85da-cad2789f7198'),
(55, '70962948-2bf7-44a9-9ded-8c68eeac7793', '74635fae-8ad3-4295-9139-320ab89c2844', 'ce6bcaee-47e1-4011-a49e-5a4d7d4245f3', 'a6fde738-c65a-4b10-b400-6fef0fdb28ba', 'a1618f1e-4f4c-4e09-9647-87e16f1edd2d'),
(56, '2272ba48-6662-404d-a9a1-41a9bed316d9', '3871d45b-b630-4a0d-8bc6-a5fc56b6a043', '166c1fd5-4dcb-41a8-91cb-f45dcd57cef3', '237dbaef-49e3-4c23-b64b-abf5c087b276', '27051a20-8580-43ff-a473-e986b52f297a'),
(57, '845f0dc7-37d0-426e-994e-43fc3ac83c08', '99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'e85e3976-7c86-4d06-9a80-641c2019a79f', '1cb6ab86-bdb2-47e5-b6e4-68c5ab389334', 'dfdd758f-649c-40f9-ba3a-8657f4b3439f'),
(59, 'b017ee50-1c14-44e2-bfd6-2c0c5653a37c', 'd04b736a-e874-4137-b191-638e0ff3b4e7', '88221cfe-1f84-41b9-8152-8e36c6a354de', '1d3f175b-c067-4359-989d-96562bfa382c', '171f6076-4eb5-4a7d-b3f2-2d650cc3d237'),
(60, 'ed1dc1bc-f0aa-4a0c-8d43-1402189b33c8', '65ff5f23-1e70-4b79-b3bd-f6eb679dd3b5', '55ef46c4-016c-4168-bc43-6b9b1e86414f', 'f3fad0f2-a1af-45df-b80d-2e48825773d9', '8c20639d-6400-4534-bd0f-ae563f11f57a'),
(61, 'd7d0cb94-2cbf-4f97-8ccf-9933dc5354b8', '44c82fbc-ef6d-44ab-8c7a-9fb19a0e7c6e', '2c98cd06-518b-464c-b911-8d787216cddd', '951e886a-0fbf-425d-8df5-af2ee4830d85', 'dcd93250-25a7-4dca-85da-cad2789f7198'),
(62, 'b2237eca-0271-43bd-87f6-b22f70d42ca4', '9e8238a4-8a7a-487f-9f7d-a8c97899eb48', 'd60e23cf-2238-4d49-844f-c7589ee5342e', 'fc305a7a-36f7-4d30-ad27-462ca0445649', '84d276ee-21ec-4171-a509-c1b88162831c'),
(63, '67f9048f-99b8-4ae4-b866-d8008d00c53d', 'aa9999a0-734c-49c6-8f4a-651864b14e62', 'd6e9c720-9b5a-40c7-a6b2-bc34752e3463', '02a9c76e-b17d-427f-9240-2dd49b0d3bfd', '2508bf5c-20d7-43b4-8c77-bc677decadef'),
(64, '70962948-2bf7-44a9-9ded-8c68eeac7793', '74635fae-8ad3-4295-9139-320ab89c2844', 'ce6bcaee-47e1-4011-a49e-5a4d7d4245f3', 'a6fde738-c65a-4b10-b400-6fef0fdb28ba', 'a1618f1e-4f4c-4e09-9647-87e16f1edd2d'),
(65, '845f0dc7-37d0-426e-994e-43fc3ac83c08', '99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'e85e3976-7c86-4d06-9a80-641c2019a79f', '1cb6ab86-bdb2-47e5-b6e4-68c5ab389334', 'dfdd758f-649c-40f9-ba3a-8657f4b3439f'),
(66, '644808c2-f87a-4600-b313-122b02322fd5', '4a2757d6-b1c3-47ac-b9a3-91deab093531', '91a6471b-1426-43f6-ad65-6fc473e16f9f', '4ec8b269-0dfa-4f97-809a-2c63fe2a0025', 'f8043c23-1534-4487-b66b-238e0c3c39b5'),
(67, 'b017ee50-1c14-44e2-bfd6-2c0c5653a37c', 'd04b736a-e874-4137-b191-638e0ff3b4e7', '88221cfe-1f84-41b9-8152-8e36c6a354de', '1d3f175b-c067-4359-989d-96562bfa382c', '171f6076-4eb5-4a7d-b3f2-2d650cc3d237'),
(68, 'ed1dc1bc-f0aa-4a0c-8d43-1402189b33c8', '65ff5f23-1e70-4b79-b3bd-f6eb679dd3b5', '55ef46c4-016c-4168-bc43-6b9b1e86414f', 'f3fad0f2-a1af-45df-b80d-2e48825773d9', '8c20639d-6400-4534-bd0f-ae563f11f57a'),
(69, 'd7d0cb94-2cbf-4f97-8ccf-9933dc5354b8', '44c82fbc-ef6d-44ab-8c7a-9fb19a0e7c6e', '2c98cd06-518b-464c-b911-8d787216cddd', '951e886a-0fbf-425d-8df5-af2ee4830d85', 'dcd93250-25a7-4dca-85da-cad2789f7198'),
(70, 'b2237eca-0271-43bd-87f6-b22f70d42ca4', '9e8238a4-8a7a-487f-9f7d-a8c97899eb48', 'd60e23cf-2238-4d49-844f-c7589ee5342e', 'fc305a7a-36f7-4d30-ad27-462ca0445649', '84d276ee-21ec-4171-a509-c1b88162831c'),
(71, '67f9048f-99b8-4ae4-b866-d8008d00c53d', 'aa9999a0-734c-49c6-8f4a-651864b14e62', 'd6e9c720-9b5a-40c7-a6b2-bc34752e3463', '02a9c76e-b17d-427f-9240-2dd49b0d3bfd', '2508bf5c-20d7-43b4-8c77-bc677decadef'),
(72, '70962948-2bf7-44a9-9ded-8c68eeac7793', '74635fae-8ad3-4295-9139-320ab89c2844', 'ce6bcaee-47e1-4011-a49e-5a4d7d4245f3', 'a6fde738-c65a-4b10-b400-6fef0fdb28ba', 'a1618f1e-4f4c-4e09-9647-87e16f1edd2d'),
(73, '2272ba48-6662-404d-a9a1-41a9bed316d9', '3871d45b-b630-4a0d-8bc6-a5fc56b6a043', '166c1fd5-4dcb-41a8-91cb-f45dcd57cef3', '237dbaef-49e3-4c23-b64b-abf5c087b276', '27051a20-8580-43ff-a473-e986b52f297a'),
(74, '845f0dc7-37d0-426e-994e-43fc3ac83c08', '99f8f8dc-e25e-4a95-aa2c-782823f36e2a', 'e85e3976-7c86-4d06-9a80-641c2019a79f', '1cb6ab86-bdb2-47e5-b6e4-68c5ab389334', 'dfdd758f-649c-40f9-ba3a-8657f4b3439f');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `scoreboard`
--

CREATE TABLE `scoreboard` (
  `score_id` int(11) NOT NULL,
  `username` varchar(20) NOT NULL,
  `score` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `scoreboard`
--

INSERT INTO `scoreboard` (`score_id`, `username`, `score`) VALUES
(6, 'kienboec', 75),
(7, 'altenhof', 100);

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `all_cards`
--
ALTER TABLE `all_cards`
  ADD PRIMARY KEY (`card_id`);

--
-- Indizes für die Tabelle `available_cards`
--
ALTER TABLE `available_cards`
  ADD PRIMARY KEY (`id`),
  ADD KEY `card_id` (`card_id`);

--
-- Indizes für die Tabelle `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `deck`
--
ALTER TABLE `deck`
  ADD PRIMARY KEY (`deck_id`);

--
-- Indizes für die Tabelle `packages`
--
ALTER TABLE `packages`
  ADD PRIMARY KEY (`package_id`);

--
-- Indizes für die Tabelle `scoreboard`
--
ALTER TABLE `scoreboard`
  ADD PRIMARY KEY (`score_id`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `available_cards`
--
ALTER TABLE `available_cards`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=163;

--
-- AUTO_INCREMENT für Tabelle `client`
--
ALTER TABLE `client`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT für Tabelle `deck`
--
ALTER TABLE `deck`
  MODIFY `deck_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `packages`
--
ALTER TABLE `packages`
  MODIFY `package_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=75;

--
-- AUTO_INCREMENT für Tabelle `scoreboard`
--
ALTER TABLE `scoreboard`
  MODIFY `score_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `available_cards`
--
ALTER TABLE `available_cards`
  ADD CONSTRAINT `available_cards_ibfk_1` FOREIGN KEY (`card_id`) REFERENCES `all_cards` (`card_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
