-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: gamedb
-- ------------------------------------------------------
-- Server version	5.7.21-log

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
-- Table structure for table `account`
--

DROP TABLE IF EXISTS `account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account` (
  `iAccountId` bigint(8) unsigned NOT NULL,
  `cName` varchar(45) NOT NULL,
  `iLevel` tinyint(1) NOT NULL DEFAULT '1',
  `iExp` int(10) unsigned NOT NULL DEFAULT '0',
  `iGameMoney` int(10) unsigned NOT NULL DEFAULT '0',
  `iportrait` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `ibestRecord` int(10) unsigned NOT NULL DEFAULT '0',
  `iwinRecord` int(10) unsigned NOT NULL DEFAULT '0',
  `icontinueRecord` int(10) unsigned NOT NULL DEFAULT '0',
  `idailyMailCount` smallint(1) NOT NULL DEFAULT '0',
  `dLoginDate` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`iAccountId`),
  UNIQUE KEY `uAccount_UNIQUE` (`iAccountId`),
  UNIQUE KEY `cName_UNIQUE` (`cName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` VALUES (101,'ddfdsafdsa',1,0,0,1,0,0,0,0,'0000-00-00 00:00:00'),(185144400936961537,'fsdfdsfsd',1,0,0,5,0,0,0,0,'0000-00-00 00:00:00'),(185144526766081793,'uytuyt',1,0,0,5,0,0,0,1,'2018-06-06 14:23:38'),(185144614846466049,'234234',1,0,0,5,0,0,0,0,'0000-00-00 00:00:00'),(185144728092674305,'5675675',1,0,0,5,0,0,0,0,'0000-00-00 00:00:00'),(185144996528130561,'fdasf',1,0,0,5,0,0,0,0,'0000-00-00 00:00:00'),(185146892353538817,'gfdgfds',1,0,0,5,0,0,0,0,'0000-00-00 00:00:00'),(185167226339331073,'gfdgdf',1,0,0,0,0,0,0,0,'0000-00-00 00:00:00'),(185167293448195329,'fdsfsaf',1,0,0,0,0,0,0,0,'0000-00-00 00:00:00'),(185169336074243585,'dd',1,0,0,1,0,0,0,0,'0000-00-00 00:00:00'),(185183122751491841,'tertert',1,0,0,5,0,0,0,0,'0000-00-00 00:00:00'),(185183785451524097,'fdsafds',1,0,0,5,0,0,0,0,'0000-00-00 00:00:00'),(185918564597760513,'fdsafsaf',1,0,0,5,0,0,0,0,'0000-00-00 00:00:00'),(186281959096320257,'123',1,0,0,5,0,0,0,0,'2018-06-06 14:26:11'),(186282282057728513,'namoeye',1,0,900,6,0,0,0,5,'2018-06-06 12:18:59'),(186831572303872257,'dada',1,0,0,1,0,0,0,0,'0000-00-00 00:00:00'),(186834407653376257,'dada6',1,0,0,1,0,0,0,0,'0000-00-00 00:00:00'),(186838060892160257,'dada6766',1,0,0,1,0,0,0,0,'0000-00-00 00:00:00'),(191313278402562049,'879978978978',1,0,0,10,0,0,0,0,'0000-00-00 00:00:00'),(191322082246656257,'8799789789781',1,0,0,10,0,0,0,0,'0000-00-00 00:00:00'),(191323017576448513,'87997897897811',1,0,0,10,0,0,0,0,'0000-00-00 00:00:00'),(192450702344192257,'테스트123123',1,0,0,1,0,0,0,0,'0000-00-00 00:00:00'),(192451054665728513,'테스트1231233213',1,0,0,1,0,0,0,5,'2018-06-06 12:22:12');
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inventory`
--

DROP TABLE IF EXISTS `inventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `inventory` (
  `iIdx` bigint(20) NOT NULL AUTO_INCREMENT,
  `iAccountId` bigint(8) NOT NULL,
  `iSlot0` bigint(8) NOT NULL DEFAULT '0',
  `iSlot1` bigint(8) NOT NULL DEFAULT '0',
  PRIMARY KEY (`iIdx`),
  UNIQUE KEY `iIdx_UNIQUE` (`iIdx`),
  UNIQUE KEY `iAccountId_UNIQUE` (`iAccountId`)
) ENGINE=InnoDB AUTO_INCREMENT=3000000031 DEFAULT CHARSET=utf8 DELAY_KEY_WRITE=1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventory`
--

LOCK TABLES `inventory` WRITE;
/*!40000 ALTER TABLE `inventory` DISABLE KEYS */;
INSERT INTO `inventory` VALUES (3000000030,186282282057728513,0,0);
/*!40000 ALTER TABLE `inventory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item`
--

DROP TABLE IF EXISTS `item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `item` (
  `iIdx` bigint(8) NOT NULL AUTO_INCREMENT,
  `iAccountId` bigint(8) NOT NULL,
  `iItemId` int(11) NOT NULL,
  `iCount` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`iIdx`,`iAccountId`),
  UNIQUE KEY `iIdx_UNIQUE` (`iIdx`)
) ENGINE=InnoDB AUTO_INCREMENT=200000041 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item`
--

LOCK TABLES `item` WRITE;
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
INSERT INTO `item` VALUES (200000040,186282282057728513,0,999);
/*!40000 ALTER TABLE `item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mailbox`
--

DROP TABLE IF EXISTS `mailbox`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `mailbox` (
  `iIdx` bigint(8) NOT NULL AUTO_INCREMENT,
  `iAccountId` bigint(8) NOT NULL,
  `iSenderAccountId` bigint(8) NOT NULL,
  `cSender` varchar(45) NOT NULL,
  `cTitle` varchar(50) NOT NULL,
  `cBody` varchar(100) NOT NULL,
  `dSendTime` datetime NOT NULL,
  `dExpireTime` datetime DEFAULT NULL,
  PRIMARY KEY (`iIdx`),
  UNIQUE KEY `idx_UNIQUE` (`iIdx`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mailbox`
--

LOCK TABLES `mailbox` WRITE;
/*!40000 ALTER TABLE `mailbox` DISABLE KEYS */;
INSERT INTO `mailbox` VALUES (1,192450702344192257,186282282057728513,'namoeye','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-05 23:14:07','2018-06-05 23:14:07'),(2,192450702344192257,186282282057728513,'namoeye','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-05 23:16:28','2018-06-05 23:16:28'),(3,192450702344192257,192451054665728513,'테스트1231233213','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-06 12:57:23','2018-06-06 12:57:23'),(4,192450702344192257,192451054665728513,'테스트1231233213','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-06 12:58:05','2018-06-06 12:58:05'),(5,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:45:49','2018-06-06 13:45:49'),(6,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:46:59','2018-06-06 13:46:59'),(7,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:47:20','2018-06-06 13:47:20'),(8,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:47:42','2018-06-06 13:47:42'),(9,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:47:42','2018-06-06 13:47:42'),(10,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:47:42','2018-06-06 13:47:42'),(11,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:47:43','2018-06-06 13:47:43'),(12,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:47:43','2018-06-06 13:47:43'),(13,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:47:44','2018-06-06 13:47:44'),(14,186281959096320257,186282282057728513,'namoeye','dd','fff','2018-06-06 13:47:44','2018-06-06 13:47:44'),(15,192450702344192257,192451054665728513,'테스트1231233213','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-06 13:51:00','2018-06-06 13:51:00'),(16,192450702344192257,192451054665728513,'테스트1231233213','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-06 13:55:14','2018-06-06 13:55:14'),(17,192450702344192257,192451054665728513,'테스트1231233213','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-06 14:19:55','2018-06-06 14:19:55'),(18,192450702344192257,192451054665728513,'테스트1231233213','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-06 14:22:21','2018-06-06 14:22:21'),(19,192450702344192257,192451054665728513,'테스트1231233213','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-06 14:22:48','2018-06-06 14:22:48'),(20,192450702344192257,192451054665728513,'테스트1231233213','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-06 14:23:07','2018-06-06 14:23:07'),(21,192450702344192257,185144526766081793,'uytuyt','테트스다!','ㅇㅇㅇㅇㅇ','2018-06-06 14:24:13','2018-06-06 14:24:13');
/*!40000 ALTER TABLE `mailbox` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'gamedb'
--
/*!50003 DROP PROCEDURE IF EXISTS `Game_Item_BuyProduct` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Item_BuyProduct`( 
IN itemId INT(11),
IN accountId BIGINT(8),
IN itemCount INT(11),
IN itemPrice INT(11),
OUT itemIdx BIGINT(8)
)
BEGIN
#현재 아이템이 있는지 확인한다
	SET @existItemIdx = 0;
	SELECT iIdx INTO @existItemIdx FROM gamedb.item WHERE iAccountId = accountId and itemId = iItemId;
    
    if @existItemIdx = 0 THEN
		INSERT INTO gamedb.item (iAccountId, iItemId, iCount) 
			VALUES (accountId, itemId, itemCount); #ON duplicate key update iCount = iCount + itemCount;
		SELECT iIdx INTO @existItemIdx FROM gamedb.item WHERE iAccountId = accountId and iItemid = itemId;
        SET itemIdx := @existItemIdx;        
	ELSE
		UPDATE gamedb.item SET iCount = iCount + itemCount WHERE iAccountId = accountId and iIdx = @existItemIdx;
        SET itemIdx := @existItemIdx;
    END IF;
    
    UPDATE gamedb.account SET iGameMoney = iGameMoney - itemPrice WHERE iAccountId = accountId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Game_Login` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Login`( 
IN i_AccountId BIGINT(8),
OUT o_error SMALLINT(2)
)
login:BEGIN
	SET @loginTime = 0;
    SET o_error := -1;
    SELECT dLoginDate INTO @loginTime FROM gamedb.account WHERE iaccountid = i_AccountId;
        
    IF @loginTime = 0 OR DATE(@loginTime) != DATE(Now()) THEN
		UPDATE gamedb.account SET idailyMailCount = 0, dLoginDate = Now() WHERE iaccountid = i_AccountId;
		SET o_error := 1;
        SELECT o_error;		
	ELSEIF DATE(@loginTime) != DATE(Now()) THEN    
		UPDATE gamedb.account SET dLoginDate = Now() WHERE iaccountid = i_acccountId;
        SET o_error := 1;
        SELECT o_error;		
    END IF;
	
    SET o_error := 0;
    SELECT * FROM gamedb.account WHERE iaccountid = i_AccountId;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Game_Mail_Write` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Mail_Write`( 
IN i_ctargetName VARCHAR(45),
IN i_isenderAccountid BIGINT(8),
IN i_ctitle VARCHAR(50),
IN i_cbody VARCHAR(100),
IN i_igameMoney INT(11),
OUT o_error SMALLINT(2)
)
mailwrite:BEGIN
	SET @targetAccountId = 0;
    SET @senderNickName = "";
    SET @dailymailcount = 0;
    
    SELECT iaccountid INTO @targetAccountId FROM gamedb.account WHERE cname = i_ctargetName;
    SELECT cname, idailymailcount INTO @senderNickName, @dailymailcount FROM gamedb.account WHERE iaccountid = i_isenderAccountid;
    
    IF @targetAccountId = 0 THEN
		SET o_error := -1;
        SELECT o_error;
		LEAVE mailwrite;
    END IF;
    
    INSERT INTO gamedb.mailbox (iaccountid, isenderaccountid, csender, ctitle, cbody, dsendtime, dexpiretime)
		VALUES (@targetAccountId, i_isenderAccountid, @senderNickName, i_ctitle, i_cbody, NOW(), NOW());
	
    IF @dailymailcount < 5 THEN
		SET @dailymailcount := @dailymailcount + 1;
    END IF;
    
    UPDATE gamedb.account SET igamemoney = i_igameMoney, idailymailcount = @dailymailcount WHERE iaccountid = i_isenderAccountid;
        
	SET o_error := 1;
    SELECT o_error;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-06-06 14:27:29
