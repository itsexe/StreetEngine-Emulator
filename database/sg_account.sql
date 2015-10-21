-- phpMyAdmin SQL Dump
-- version 3.5.5
-- http://www.phpmyadmin.net
--
-- Client: sql2.freemysqlhosting.net
-- Généré le: Lun 07 Septembre 2015 à 18:39
-- Version du serveur: 5.5.43-0ubuntu0.12.04.1
-- Version de PHP: 5.3.28

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données: `sql286017`
--

-- --------------------------------------------------------

--
-- Structure de la table `sg_account`
--

CREATE TABLE IF NOT EXISTS `sg_account` (
  `id` int(255) NOT NULL,
  `user` text NOT NULL,
  `password` text NOT NULL,
  `username` text NOT NULL,
  `email` text NOT NULL,
  `first_login` int(255) NOT NULL,
  `auth_key` text NOT NULL,
  `char_rank` text NOT NULL,
  `char_type` int(255) NOT NULL,
  `char_level` int(255) NOT NULL,
  `char_exp` int(255) NOT NULL,
  `char_liscence` int(255) NOT NULL,
  `char_gpotatos` int(255) NOT NULL,
  `char_rupees` int(255) NOT NULL,
  `char_coins` int(255) NOT NULL,
  `char_questpoints` int(255) NOT NULL,
  `char_clanid` text NOT NULL,
  `char_clanname` text NOT NULL,
  `char_age` int(255) NOT NULL,
  `char_bio` text NOT NULL,
  `char_zoneid` int(255) NOT NULL,
  `char_zoneinfo` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `sg_account`
--

INSERT INTO `sg_account` (`id`, `user`, `password`, `username`, `email`, `first_login`, `auth_key`, `char_rank`, `char_type`, `char_level`, `char_exp`, `char_liscence`, `char_gpotatos`, `char_rupees`, `char_coins`, `char_questpoints`, `char_clanid`, `char_clanname`, `char_age`, `char_bio`, `char_zoneid`, `char_zoneinfo`) VALUES
(0, 'admin', 'admin', 'Admin', 'no-reply@no-reply.com', 1, 'nxYRmXvGoTvlaYnhr', 'Admin', 3, 45, 10240, 4, 999999, 999999, 999999, 999999, 'CL#1', 'GameMaster', 18, 'GameMaster', 0, 'GameMaster');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
