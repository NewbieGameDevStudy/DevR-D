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
  UNIQUE KEY `cName_UNIQUE` (`cName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` VALUES (194621334355968257,'ouiouio',1,0,0,6,0,0,0,0,'2018-06-10 01:15:52'),(194621518905344513,'yrtyrtytr',1,0,0,4,0,0,0,0,'2018-06-10 01:16:26'),(194621799923712769,'iououoiu',1,0,0,0,0,0,0,0,'2018-06-10 01:17:33'),(194621879615489025,'gdfgdfgdfg',1,0,0,6,0,0,0,0,'2018-06-10 01:17:52'),(194622114496513281,'78978987',1,0,0,0,0,0,0,0,'2018-06-10 01:18:48'),(194623171461120257,'867867',1,0,0,5,0,0,0,0,'2018-06-10 01:23:01'),(194623347621888513,'gdfsgdsfgdfs',1,0,0,2,0,0,0,0,'2018-06-10 01:23:47'),(194625105035264769,'675675756',1,0,0,0,0,0,0,0,'2018-06-10 01:30:55'),(194627000860672257,'9879789',1,0,0,5,0,0,0,0,'2018-06-10 01:38:17'),(194628569530368513,'432432432',1,0,0,0,0,0,0,0,'2018-06-10 01:44:27'),(194630578601984257,'765876867867',1,0,0,6,0,0,0,0,'2018-06-10 01:52:26'),(194631262273536513,'3243241231',1,0,0,5,0,0,0,0,'2018-06-10 01:55:09');
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
  UNIQUE KEY `iAccountId_UNIQUE` (`iAccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 DELAY_KEY_WRITE=1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventory`
--

LOCK TABLES `inventory` WRITE;
/*!40000 ALTER TABLE `inventory` DISABLE KEYS */;
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
  PRIMARY KEY (`iIdx`),
  KEY `index2` (`iAccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item`
--

LOCK TABLES `item` WRITE;
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
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
  PRIMARY KEY (`iIdx`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mailbox`
--

LOCK TABLES `mailbox` WRITE;
/*!40000 ALTER TABLE `mailbox` DISABLE KEYS */;
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
	ELSEIF DATE(@loginTime) != DATE(Now()) THEN    
		UPDATE gamedb.account SET dLoginDate = Now() WHERE iaccountid = i_acccountId;	
    END IF;
	
    SET o_error := 1;
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
        #SELECT o_error;
		LEAVE mailwrite;
    END IF;
    
    INSERT INTO gamedb.mailbox (iaccountid, isenderaccountid, csender, ctitle, cbody, dsendtime, dexpiretime)
		VALUES (@targetAccountId, i_isenderAccountid, @senderNickName, i_ctitle, i_cbody, NOW(), NOW());
	
    IF @dailymailcount < 5 THEN
		SET @dailymailcount := @dailymailcount + 1;
    END IF;
    
    UPDATE gamedb.account SET igamemoney = i_igameMoney, idailymailcount = @dailymailcount WHERE iaccountid = i_isenderAccountid;
        
	SET o_error := 1;
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

-- Dump completed on 2018-06-10  2:08:49
