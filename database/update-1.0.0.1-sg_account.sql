/*
Navicat MySQL Data Transfer

Source Server         : streetengine_db
Source Server Version : 50543
Source Host           : sql2.freemysqlhosting.net:3306
Source Database       : sql286017

Target Server Type    : MYSQL
Target Server Version : 50543
File Encoding         : 65001

Date: 2015-10-21 16:10:28
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `sg_account`
-- ----------------------------
DROP TABLE IF EXISTS `sg_account`;
CREATE TABLE `sg_account` (
  `id` int(255) NOT NULL,
  `user` text NOT NULL,
  `password` text NOT NULL,
  `username` text,
  `email` text,
  `first_login` int(255) NOT NULL,
  `auth_key` text NOT NULL,
  `char_rank` text NOT NULL,
  `char_type` int(255) NOT NULL,
  `char_level` int(255) NOT NULL,
  `char_exp` int(255) NOT NULL,
  `char_liscence` int(255) NOT NULL,
  `char_tricks` text NOT NULL,
  `char_gpotatos` int(255) NOT NULL,
  `char_rupees` int(255) NOT NULL,
  `char_coins` int(255) NOT NULL,
  `char_questpoints` int(255) DEFAULT NULL,
  `char_clanid` text,
  `char_clanname` text,
  `char_age` int(255) DEFAULT NULL,
  `char_bio` text,
  `char_zoneid` int(255) DEFAULT NULL,
  `char_zoneinfo` text
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sg_account
-- ----------------------------
INSERT INTO `sg_account` VALUES ('0', 'admin', '21232F297A57A5A743894A0E4A801FC3', 'Admin', '', '1', 'TAtJpdxyZURXCZepI', 'Admin', '1', '45', '102400', '4', '5|4|5|5|4|3|3|3|3|5|0|3|4', '500', '0', '0', '0', 'CL#1', 'GameMaster', '18', 'GameMaster', '0', 'GameMaster');
INSERT INTO `sg_account` VALUES ('1', 'test', '21232F297A57A5A743894A0E4A801FC3', 'Test', '', '1', 'ZwUZodSuGYnfCKCcc', 'Player', '2', '45', '102400', '4', '5|4|5|5|4|3|3|3|3|5|0|3|4', '500', '0', '0', '0', 'CL#1', 'GameMaster', '18', 'GameMaster', '0', 'GameMaster');
