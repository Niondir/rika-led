-- phpMyAdmin SQL Dump
-- version 2.11.4
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Erstellungszeit: 03. Februar 2009 um 11:33
-- Server Version: 5.0.51
-- PHP-Version: 5.2.5

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

--
-- Datenbank: `rika`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `led_traces`
--

DROP TABLE IF EXISTS `led_traces`;
CREATE TABLE IF NOT EXISTS `led_traces` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `timestamp` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Daten für Tabelle `led_traces`
--

INSERT INTO `led_traces` (`id`, `timestamp`) VALUES
(1, '2009-02-01 17:58:14'),
(2, '2009-02-01 17:58:16'),
(3, '2009-02-01 17:58:17');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `led_waypoints`
--

DROP TABLE IF EXISTS `led_waypoints`;
CREATE TABLE IF NOT EXISTS `led_waypoints` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `regions_id` varchar(4) NOT NULL,
  `traces_id` int(10) unsigned NOT NULL,
  `time` int(10) unsigned default NULL,
  PRIMARY KEY  (`id`),
  KEY `waypoints_FKIndex2` (`traces_id`),
  KEY `waypoints_FKIndex3` USING BTREE (`regions_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9 ;

--
-- Daten für Tabelle `led_waypoints`
--

INSERT INTO `led_waypoints` (`id`, `regions_id`, `traces_id`, `time`) VALUES
(1, '0', 1, 0),
(2, '5555', 1, 3464),
(3, '1', 1, 7014),
(4, '2', 1, 7033),
(5, '3', 1, 7053),
(6, '4', 1, 7079),
(7, '0', 2, 0),
(8, '0', 3, 0);
