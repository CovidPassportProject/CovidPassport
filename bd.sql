-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: covid_passport
-- ------------------------------------------------------
-- Server version	8.0.23

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin` (
  `login` varchar(45) NOT NULL,
  `password` varchar(45) DEFAULT NULL,
  `type` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin`
--

LOCK TABLES `admin` WRITE;
/*!40000 ALTER TABLE `admin` DISABLE KEYS */;
INSERT INTO `admin` VALUES ('admin','admin','admin');
/*!40000 ALTER TABLE `admin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `area`
--

DROP TABLE IF EXISTS `area`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `area` (
  `id` int NOT NULL,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `area`
--

LOCK TABLES `area` WRITE;
/*!40000 ALTER TABLE `area` DISABLE KEYS */;
INSERT INTO `area` VALUES (1,'Кировский'),(2,'Калининский'),(3,'Дёмский'),(4,'Ленинский'),(5,'Октябрьский'),(6,'Орджоникидзевский'),(7,'Советский');
/*!40000 ALTER TABLE `area` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `covid_passport`
--

DROP TABLE IF EXISTS `covid_passport`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `covid_passport` (
  `id_passport` int NOT NULL,
  `id_patients` int NOT NULL,
  `surname` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `patronymic` varchar(45) NOT NULL,
  `birthday` date NOT NULL,
  `name_polyclinic` varchar(45) NOT NULL,
  `name_vaccine` varchar(45) NOT NULL,
  `date_vaccine1` date NOT NULL,
  `date_vaccine2` date NOT NULL,
  PRIMARY KEY (`id_passport`),
  KEY `id_patients` (`id_patients`),
  CONSTRAINT `covid_passport_ibfk_1` FOREIGN KEY (`id_patients`) REFERENCES `patients` (`id_patients`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `covid_passport`
--

LOCK TABLES `covid_passport` WRITE;
/*!40000 ALTER TABLE `covid_passport` DISABLE KEYS */;
/*!40000 ALTER TABLE `covid_passport` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `credentials_patients`
--

DROP TABLE IF EXISTS `credentials_patients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `credentials_patients` (
  `id_patients` int NOT NULL,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `type` varchar(45) NOT NULL,
  KEY `credentials_patients_ibfk_1` (`id_patients`),
  CONSTRAINT `credentials_patients_ibfk_1` FOREIGN KEY (`id_patients`) REFERENCES `patients` (`id_patients`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credentials_patients`
--

LOCK TABLES `credentials_patients` WRITE;
/*!40000 ALTER TABLE `credentials_patients` DISABLE KEYS */;
INSERT INTO `credentials_patients` VALUES (1,'log','log','user'),(3,'nek','123','user'),(4,'rok','rok','user'),(5,'g','g','user'),(6,'ty','ty','user'),(7,'ufa','ufa','user'),(8,'victor','vector','user'),(9,'bth','bth','user');
/*!40000 ALTER TABLE `credentials_patients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patients`
--

DROP TABLE IF EXISTS `patients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patients` (
  `id_patients` int NOT NULL,
  `surname` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `patronymic` varchar(45) NOT NULL,
  `datebirthday` date NOT NULL,
  `snils` int NOT NULL,
  `address` varchar(45) NOT NULL,
  `status_vaccine` int NOT NULL,
  PRIMARY KEY (`id_patients`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES (1,'Петров','Петр','Петрович','2000-01-01',12345,'ул.Мира,д.4,кв.78',0),(3,'Никитов','Лев','Андреевич','2000-07-06',12345678,'ул.Рыльского,д.1,кв.90',1),(4,'Омаров','Омар','Рюрикович','2000-01-10',1234566777,'ываывавыа',0),(5,'fdf','fdgd','gd','1954-07-15',233131313,'fdgd',0),(6,'dfg','df','fgdfg','2000-05-10',12122121,'dfdfdsfs',1),(7,'Рюриков','Александр','Львович','2000-10-13',1212313,'ул.Вольская,д.3',1),(8,'Викторов','Николай','Александрович','1999-12-15',154246545,'ул. Первомайская, д.2',1),(9,'Олькова','Ирина','Олеговна','1985-12-15',1131313,'ул.Пролетарская',0);
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `polyclinic`
--

DROP TABLE IF EXISTS `polyclinic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `polyclinic` (
  `id` int NOT NULL,
  `id_area` int NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `polyclinic_ibfk_1` (`id_area`),
  CONSTRAINT `polyclinic_ibfk_1` FOREIGN KEY (`id_area`) REFERENCES `area` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `polyclinic`
--

LOCK TABLES `polyclinic` WRITE;
/*!40000 ALTER TABLE `polyclinic` DISABLE KEYS */;
INSERT INTO `polyclinic` VALUES (1,3,'Поликлиника №2'),(2,3,'Поликлиника, Городская клиническая больница Демского района г. Уфы'),(3,2,'Поликлиника №1, Городская клиническая больница №13'),(4,2,'Поликлиника №2, Городская клиническая больница №13'),(5,2,'Поликлиника №3, Городская клиническая больница №13'),(6,1,'Городская поликлиника №46'),(7,1,'Городская поликлиника №1'),(8,1,'Городская поликлиника №52'),(9,1,'Городская клиническая больница №5'),(10,1,'Поликлиника №2,Городская клиническая больница №21'),(11,4,'Городская поликлиника №44'),(12,4,'Городская поликлиника №9'),(13,5,'Поликлиника №21'),(14,5,'Поликлиника №43'),(15,5,'Городская поликлиника №38'),(16,5,'Городская поликлиника №50'),(17,6,'Поликлиника №1'),(18,6,'Городская клиническая больница №8'),(19,6,'Поликлиника №32'),(20,6,'Поликлиника №2'),(21,6,'Городская поликлиника №38'),(22,7,'Городская поликлиника №48'),(23,7,'Городская поликлиника №50'),(24,7,'Городская поликлиника №51'),(25,7,'Городская клиническая больница №5');
/*!40000 ALTER TABLE `polyclinic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type_vaccine`
--

DROP TABLE IF EXISTS `type_vaccine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `type_vaccine` (
  `id` int NOT NULL,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type_vaccine`
--

LOCK TABLES `type_vaccine` WRITE;
/*!40000 ALTER TABLE `type_vaccine` DISABLE KEYS */;
INSERT INTO `type_vaccine` VALUES (1,'Спутник V'),(2,'ЭпиВакКорона'),(3,'КовиВак');
/*!40000 ALTER TABLE `type_vaccine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `zapic_vaccine`
--

DROP TABLE IF EXISTS `zapic_vaccine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `zapic_vaccine` (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_patients` int NOT NULL,
  `date_vaccine1` date NOT NULL,
  `date_vaccine2` date NOT NULL,
  `id_polyclinic` int NOT NULL,
  `id_type_vaccine` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `2_idx` (`id_type_vaccine`),
  KEY `3_idx` (`id_polyclinic`),
  KEY `1_idx` (`id_patients`),
  CONSTRAINT `1` FOREIGN KEY (`id_patients`) REFERENCES `patients` (`id_patients`),
  CONSTRAINT `2` FOREIGN KEY (`id_type_vaccine`) REFERENCES `type_vaccine` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `3` FOREIGN KEY (`id_polyclinic`) REFERENCES `polyclinic` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `zapic_vaccine`
--

LOCK TABLES `zapic_vaccine` WRITE;
/*!40000 ALTER TABLE `zapic_vaccine` DISABLE KEYS */;
INSERT INTO `zapic_vaccine` VALUES (11,3,'2021-10-26','2021-11-16',12,2),(12,7,'2021-10-31','2021-11-21',4,2),(13,8,'2021-11-08','2021-11-29',4,2),(14,6,'2021-11-08','2021-11-29',5,1);
/*!40000 ALTER TABLE `zapic_vaccine` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-11-09 11:36:13
