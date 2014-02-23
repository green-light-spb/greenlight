CREATE DATABASE  IF NOT EXISTS `greenlight` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `greenlight`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: greenlight
-- ------------------------------------------------------
-- Server version	5.5.21

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `table_credprogr`
--

DROP TABLE IF EXISTS `table_credprogr`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `table_credprogr` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Bank` varchar(50) DEFAULT NULL,
  `Min_Age` int(11) DEFAULT NULL,
  `Max_Age` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `table_credprogr`
--

LOCK TABLES `table_credprogr` WRITE;
/*!40000 ALTER TABLE `table_credprogr` DISABLE KEYS */;
INSERT INTO `table_credprogr` VALUES (4,'Альфа',10,30),(5,'ВТБ',23,30),(6,'Сбер',10,20);
/*!40000 ALTER TABLE `table_credprogr` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `references`
--

DROP TABLE IF EXISTS `references`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `references` (
  `ReferenceID` int(11) NOT NULL,
  `ReferenceName` varchar(45) DEFAULT NULL,
  `Hierarchycal` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`ReferenceID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `references`
--

LOCK TABLES `references` WRITE;
/*!40000 ALTER TABLE `references` DISABLE KEYS */;
/*!40000 ALTER TABLE `references` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `table_clients`
--

DROP TABLE IF EXISTS `table_clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `table_clients` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `SName` varchar(50) DEFAULT NULL,
  `Age` float DEFAULT NULL,
  `Gender` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `table_clients`
--

LOCK TABLES `table_clients` WRITE;
/*!40000 ALTER TABLE `table_clients` DISABLE KEYS */;
INSERT INTO `table_clients` VALUES (1,'Иван','Иванов',23,'М'),(2,'Лена','Иванова',22,'Ж'),(4,'вАСЯ','Петров',12,'М');
/*!40000 ALTER TABLE `table_clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tableconfig`
--

DROP TABLE IF EXISTS `tableconfig`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tableconfig` (
  `TableConfigID` int(11) NOT NULL AUTO_INCREMENT,
  `TableDBName` varchar(30) DEFAULT NULL,
  `TableName` varchar(30) DEFAULT NULL,
  `ColumnName` varchar(45) DEFAULT NULL,
  `ColumnDBName` varchar(45) DEFAULT NULL,
  `ColumnDBName_Old` varchar(45) DEFAULT NULL,
  `ColumnType` varchar(50) DEFAULT NULL,
  `ColumnReference` int(11) DEFAULT NULL,
  `ReferenceMultiSelect` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`TableConfigID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tableconfig`
--

LOCK TABLES `tableconfig` WRITE;
/*!40000 ALTER TABLE `tableconfig` DISABLE KEYS */;
INSERT INTO `tableconfig` VALUES (2,'Clients','Клиенты','Имя','Name','Name','Строка50',1,NULL),(3,'Clients','Клиенты','Фамилия','SName','SName','Строка50',1,1),(6,'Clients','Клиенты','Возраст','Age','Age','Число с плавающей точкой',NULL,NULL),(7,'Clients','Клиенты','Пол','Gender','Gender','Строка50',NULL,NULL),(8,'CredProgr','Кредитные программы','Банк','Bank','Bank','Строка50',NULL,NULL),(9,'CredProgr','Кредитные программы','Мин Возраст','Min_Age','Min_Age','Целое число',NULL,NULL),(10,'CredProgr','Кредитные программы','Макс возраст','Max_Age','Max_Age','Целое число',NULL,NULL);
/*!40000 ALTER TABLE `tableconfig` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'greenlight'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-15 15:04:43
