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
  `iGameMoney` int(10) unsigned NOT NULL DEFAULT '9999',
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
INSERT INTO `account` VALUES (194621334355968257,'ouiouio',1,0,0,6,0,0,0,0,'2018-06-10 01:15:52'),(194621518905344513,'yrtyrtytr',1,0,0,4,0,0,0,0,'2018-06-10 01:16:26'),(194621799923712769,'iououoiu',1,0,0,0,0,0,0,0,'2018-06-16 02:51:17'),(194621879615489025,'gdfgdfgdfg',1,0,0,6,0,0,0,0,'2018-06-16 02:40:55'),(194622114496513281,'78978987',1,0,0,0,0,0,0,0,'2018-06-10 01:18:48'),(194623171461120257,'867867',1,0,0,5,0,0,0,0,'2018-06-10 01:23:01'),(194623347621888513,'gdfsgdsfgdfs',1,0,0,2,0,0,0,0,'2018-06-10 01:23:47'),(194625105035264769,'675675756',1,0,0,0,0,0,0,0,'2018-06-10 01:30:55'),(194627000860672257,'9879789',1,0,3499,5,0,0,0,0,'2018-06-15 19:57:53'),(194628569530368513,'432432432',1,0,0,0,0,0,0,0,'2018-06-10 01:44:27'),(194630578601984257,'765876867867',16,0,9700,6,0,14,0,0,'2018-06-16 01:24:19'),(194631262273536513,'3243241231',1,0,0,5,0,0,0,0,'2018-06-12 22:16:54');
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `guild`
--

DROP TABLE IF EXISTS `guild`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `guild` (
  `iguildIdx` bigint(20) NOT NULL AUTO_INCREMENT,
  `cguildName` varchar(45) NOT NULL,
  `iguildMemberCount` smallint(10) NOT NULL DEFAULT '1',
  `iguildJoinType` tinyint(1) NOT NULL DEFAULT '0',
  `iguildLeaderId` bigint(20) NOT NULL DEFAULT '0',
  `iguildLeaderId2` bigint(20) NOT NULL DEFAULT '0',
  `iguildGrade` tinyint(1) NOT NULL DEFAULT '0',
  `iguildMark` tinyint(1) NOT NULL DEFAULT '0',
  `iguildScore` smallint(3) NOT NULL DEFAULT '0',
  `dguildCreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`iguildIdx`),
  UNIQUE KEY `cguildName_UNIQUE` (`cguildName`)
) ENGINE=InnoDB AUTO_INCREMENT=2000000015 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `guild`
--

LOCK TABLES `guild` WRITE;
/*!40000 ALTER TABLE `guild` DISABLE KEYS */;
INSERT INTO `guild` VALUES (2000000014,'test1',10,0,194631262273536513,0,0,5,42,'2018-06-13 02:24:20');
/*!40000 ALTER TABLE `guild` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `guild_member`
--

DROP TABLE IF EXISTS `guild_member`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `guild_member` (
  `iguildIdx` bigint(20) NOT NULL,
  `iAccountId` varchar(45) NOT NULL,
  PRIMARY KEY (`iAccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `guild_member`
--

LOCK TABLES `guild_member` WRITE;
/*!40000 ALTER TABLE `guild_member` DISABLE KEYS */;
INSERT INTO `guild_member` VALUES (2000000014,'194621518905344513'),(2000000014,'194621799923712769'),(2000000014,'194621879615489025'),(2000000014,'194623171461120257'),(2000000014,'194623347621888513'),(2000000014,'194627000860672257'),(2000000014,'194630578601984257'),(2000000014,'194631262273536513');
/*!40000 ALTER TABLE `guild_member` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=100000002 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventory`
--

LOCK TABLES `inventory` WRITE;
/*!40000 ALTER TABLE `inventory` DISABLE KEYS */;
INSERT INTO `inventory` VALUES (100000001,194627000860672257,0,0);
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
) ENGINE=InnoDB AUTO_INCREMENT=300000005 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item`
--

LOCK TABLES `item` WRITE;
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
INSERT INTO `item` VALUES (1,194627000860672257,2,1),(2,194627000860672257,1,1),(3,194627000860672257,3,1),(300000001,194630578601984257,0,1),(300000002,194630578601984257,1,1),(300000003,194630578601984257,3,2),(300000004,194630578601984257,4,1);
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
  `iReadDone` tinyint(4) NOT NULL DEFAULT '0',
  `iMailType` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`iIdx`,`iAccountId`)
) ENGINE=InnoDB AUTO_INCREMENT=700000031 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mailbox`
--

LOCK TABLES `mailbox` WRITE;
/*!40000 ALTER TABLE `mailbox` DISABLE KEYS */;
INSERT INTO `mailbox` VALUES (700000000,194627000860672257,194628569530368513,'9879789','테스트타이블','테스ㅡ바디','2018-06-11 23:25:55','2018-06-11 23:25:55',1,0),(700000001,194627000860672257,194628569530368513,'9879789','테스트타이블','테스ㅡ바디','2018-06-11 23:26:12','2018-06-11 23:26:12',1,0),(700000002,194628569530368513,194627000860672257,'9879789','테스트타이블','테스ㅡ바디','2018-06-11 23:39:22','2018-06-11 23:39:22',0,0),(700000007,194630578601984257,194631262273536513,'3243241231','가입메일','가입요청해요','2018-06-14 00:40:00','2018-06-14 00:40:00',0,1),(700000010,194630578601984257,194630578601984257,'765876867867','가입요청','[765876867867]님이 길드에 가입요청을 하였습니다.','2018-06-14 23:17:39','2018-06-14 23:17:39',0,1),(700000011,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-14 23:22:12','2018-06-14 23:22:12',0,0),(700000012,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:13:05','2018-06-15 00:13:05',0,0),(700000015,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:18:26','2018-06-15 00:18:26',0,0),(700000016,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:19:05','2018-06-15 00:19:05',0,0),(700000017,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:20:51','2018-06-15 00:20:51',0,0),(700000018,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:22:22','2018-06-15 00:22:22',0,0),(700000019,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:24:43','2018-06-15 00:24:43',0,0),(700000021,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:25:54','2018-06-15 00:25:54',0,0),(700000022,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:26:59','2018-06-15 00:26:59',0,0),(700000025,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:34:37','2018-06-15 00:34:37',0,0),(700000026,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:36:36','2018-06-15 00:36:36',0,0),(700000027,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:37:36','2018-06-15 00:37:36',0,0),(700000028,194630578601984257,194630578601984257,'765876867867','가입정보','[765876867867]님이 길드에 가입을 하였습니다.','2018-06-15 00:40:50','2018-06-15 00:40:50',0,0),(700000029,194631262273536513,194621879615489025,'gdfgdfgdfg','가입정보','[gdfgdfgdfg]님이 길드에 가입을 하였습니다.','2018-06-16 02:41:04','2018-06-16 02:41:04',0,0),(700000030,194631262273536513,194621799923712769,'iououoiu','가입정보','[iououoiu]님이 길드에 가입을 하였습니다.','2018-06-16 02:51:56','2018-06-16 02:51:56',0,0);
/*!40000 ALTER TABLE `mailbox` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'gamedb'
--
/*!50003 DROP PROCEDURE IF EXISTS `Game_Guild_Create` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Guild_Create`( 
IN i_accountId BIGINT(8),
IN i_guildName VARCHAR(45),
IN i_guildJoinType TINYINT(1),
IN i_guildMark TINYINT(1),
IN i_gameMoney INT(10),
OUT o_error SMALLINT(2),
OUT o_guildIdx BIGINT(20)
)
GuildCreate:BEGIN
	
    SET o_error = -1;
    SET @createGuildIdx = 0;
    SET o_guildIdx = 0;
    
    IF (SELECT EXISTS(SELECT 1 FROM gamedb.guild_member WHERE iAccountId = i_accountId LIMIT 1)) = 0 THEN
		INSERT INTO gamedb.guild (cguildName, iguildJoinType, iguildLeaderId, iguildMark)
			VALUES (i_guildName, i_guildJoinType, i_accountId, i_guildMark);
            
        SELECT iguildIdx INTO o_guildIdx FROM gamedb.guild WHERE cguildName = i_guildName;
        
        INSERT INTO gamedb.guild_member (iguildIdx, iAccountId)
			VALUES (o_guildIdx, i_accountId);
        
		UPDATE gamedb.account SET igamemoney = igamemoney - i_gameMoney WHERE iaccountid = i_accountId;
        
        SET o_error = 1;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Game_Guild_Exit` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Guild_Exit`( 
IN i_accountId BIGINT(8),
IN i_guildIdx BIGINT(8),
OUT o_error SMALLINT(2)
)
GuildExit:BEGIN
	    
	SET o_error = -1;
    SET @winRecord = 0;
        
    SELECT iwinRecord INTO @winRecord FROM gamedb.account WHERE iaccountId = i_accountId;    
    DELETE FROM gamedb.guild_member WHERE iaccountId = i_accountId AND iguildIdx = i_guildIdx;    
    UPDATE gamedb.guild SET iguildMemberCount = iguildMemberCount - 1, iguildScore = iguildScore - @winRecord, iguildLeaderId = iguildLeaderId2, iguildLeaderId2 = 0 WHERE iguildIdx = i_guildIdx;
    
    IF ROW_COUNT() > 0 THEN
		SET o_error = 1;
	ELSE
		SET o_error = -1;
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Game_Guild_GetList` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Guild_GetList`( 
IN i_accountId BIGINT(8),
IN i_guildIdx BIGINT(8),
OUT o_error SMALLINT(2)
)
GuildGetList:BEGIN
	    
	SET o_error = -1;
    
    SELECT A.iAccountId, A.cName, A.iLevel, A.iExp, A.iportrait, A.ibestRecord, A.iwinRecord, A.icontinueRecord FROM gamedb.guild_member AS M 
		LEFT JOIN gamedb.account AS A 
			ON M.iAccountId = A.iAccountId;

	IF ROW_COUNT() > 0 THEN
		SET o_error = 1;
	ELSE
		SET o_error = -1;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Game_Guild_Join` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Guild_Join`( 
IN i_accountId BIGINT(8),
IN i_userName VARCHAR(45),
IN i_guildName VARCHAR(45),
IN i_title VARCHAR(50),
IN i_guildJoinMsg VARCHAR(100),
OUT o_error SMALLINT(2)
)
GuildJoin:BEGIN
	    
	SET o_error = -1;
    SET @joinGuildIdx = 0, @joinGuildType = 0;
    SET @guildLeaderId = 0, @guildLeaderId2 = 0;
        
    #길드가입조건 확인    
	SELECT iguildIdx, iguildJoinType, iguildLeaderId, iguildLeaderId2
		INTO @joinGuildIdx, @joinGuildType, @guildLeaderId, @guildLeaderId2
			FROM gamedb.guild WHERE cguildName = i_guildName;
	    
    INSERT INTO gamedb.mailbox (iaccountid, isenderaccountid, csender, ctitle, cbody, dsendtime, dexpiretime, imailtype)
		VALUES (@guildLeaderId, i_accountId, i_userName, i_title, i_guildJoinMsg, NOW(), NOW(), @joinGuildType);
        
    IF @guildLeaderId2 != 0 THEN    
		INSERT INTO gamedb.mailbox (iaccountid, isenderaccountid, csender, ctitle, cbody, dsendtime, dexpiretime, imailtype)
			VALUES (@guildLeaderId2, i_accountId, i_userName, i_title, i_guildJoinMsg, NOW(), NOW(), @joinGuildType);
	END IF;
            
    IF @joinGuildType = 0 THEN
		SET @winRecord = 0;
		SELECT iwinRecord INTO @winRecord FROM gamedb.account WHERE iaccountId = i_accountId;
		UPDATE gamedb.guild SET iguildMemberCount = iguildMemberCount + 1, iguildScore = iguildScore + @winRecord WHERE iguildIdx = @joinGuildIdx;
		INSERT INTO gamedb.guild_member (iguildIdx, iAccountId)
			VALUES (@joinGuildIdx, i_accountId);    		
	END IF;    
	
    IF ROW_COUNT() > 0 THEN
		SET o_error = 1;
	ELSE
		SET o_error = -1;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Game_Guild_Kick` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Guild_Kick`( 
IN i_accountId BIGINT(8),
IN i_guildIdx BIGINT(8),
IN i_kickUserId BIGINT(8),
IN i_guildGrade TINYINT(1),
OUT o_error SMALLINT(2)
)
GuildKick:BEGIN
	    
	SET o_error = -1;
	SET @winRecord = 0;
    
    #길드가입조건 확인    
	IF (SELECT EXISTS(SELECT 1 FROM gamedb.guild_member WHERE iAccountId = i_kickUserId LIMIT 1)) = 0 THEN
		SET o_error = -1;
	ELSE

		SELECT iwinRecord INTO @winRecord FROM gamedb.account WHERE iaccountId = i_kickUserId;    
		DELETE FROM gamedb.guild_member WHERE iaccountId = i_kickUserId AND iguildIdx = i_guildIdx;
        
		IF i_guildGrade = 1 THEN
			UPDATE gamedb.guild SET iguildMemberCount = iguildMemberCount - 1, iguildScore = iguildScore - @winRecord, iguildLeaderId2 = 0 WHERE iguildIdx = i_guildIdx;
		ELSE
			UPDATE gamedb.guild SET iguildMemberCount = iguildMemberCount - 1, iguildScore = iguildScore - @winRecord WHERE iguildIdx = i_guildIdx;
		END IF;
    
		IF ROW_COUNT() > 0 THEN
			SET o_error = 1;
		ELSE
			SET o_error = -1;
		END IF; 
	END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Game_Guild_Leave` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Guild_Leave`( 
IN i_accountId BIGINT(8),
IN i_guildIdx BIGINT(8),
IN i_guildGrade TINYINT(1),
OUT o_error SMALLINT(2)
)
GuildExit:BEGIN
	    
	SET o_error = -1;
    SET @winRecord = 0;
        
    SELECT iwinRecord INTO @winRecord FROM gamedb.account WHERE iaccountId = i_accountId;    
    DELETE FROM gamedb.guild_member WHERE iaccountId = i_accountId AND iguildIdx = i_guildIdx;
    
    IF i_guildGrade = 2 THEN
		UPDATE gamedb.guild SET iguildMemberCount = iguildMemberCount - 1, iguildScore = iguildScore - @winRecord, iguildLeaderId = iguildLeaderId2, iguildLeaderId2 = 0 WHERE iguildIdx = i_guildIdx;
    ELSEIF i_guildGrade = 1 THEN
		UPDATE gamedb.guild SET iguildMemberCount = iguildMemberCount - 1, iguildScore = iguildScore - @winRecord, iguildLeaderId2 = 0 WHERE iguildIdx = i_guildIdx;
    ELSE
		UPDATE gamedb.guild SET iguildMemberCount = iguildMemberCount - 1, iguildScore = iguildScore - @winRecord WHERE iguildIdx = i_guildIdx;
    END IF;
    
    IF ROW_COUNT() > 0 THEN
		SET o_error = 1;
	ELSE
		SET o_error = -1;
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
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
    
    UPDATE gamedb.account SET iGameMoney = iGameMoney - (itemPrice * itemCount) WHERE iAccountId = accountId;
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
/*!50003 DROP PROCEDURE IF EXISTS `Game_Slot_Equip` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Slot_Equip`( 
IN i_accountId BIGINT(8),
IN i_slotId SMALLINT(2),
IN i_itemIdx BIGINT(8),
OUT o_error SMALLINT(2)
)
BEGIN
	IF (SELECT EXISTS(SELECT 1 FROM gamedb.inventory WHERE iAccountId = i_accountId LIMIT 1)) = 0 THEN		
        IF i_slotId = 0 THEN
			INSERT INTO gamedb.inventory (iAccountId, iSlot0) 
				VALUES (i_accountId, i_itemIdx); #ON duplicate key update iCount = iCount + itemCount;
		ELSE
			INSERT INTO gamedb.inventory (iAccountId, iSlot1) 
				VALUES (i_accountId, i_itemIdx); #ON duplicate key update iCount = iCount + itemCount;
        END IF;		
        
	ELSE
		IF i_slotId = 0 THEN
			UPDATE gamedb.inventory SET iSlot0 = i_itemIdx WHERE iAccountId = i_accountId;
		ELSE
			UPDATE gamedb.inventory SET iSlot1 = i_itemIdx WHERE iAccountId = i_accountId;
		END IF;
    END IF;
    
	SET o_error := 1;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Game_Slot_UnEquip` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Game_Slot_UnEquip`( 
IN i_accountId BIGINT(8),
IN i_slotId SMALLINT(2),
OUT o_error SMALLINT(2)
)
BEGIN
	IF (SELECT EXISTS(SELECT 1 FROM gamedb.inventory WHERE iAccountId = i_accountId LIMIT 1)) = 1 THEN		
        IF i_slotId = 0 THEN
			UPDATE gamedb.inventory SET iSlot0 = 0 WHERE iAccountId = i_accountId;
		ELSE
			UPDATE gamedb.inventory SET iSlot1 = 0 WHERE iAccountId = i_accountId;
        END IF;		        
        SET o_error := 1;
	ELSE		
        SET o_error := -1;
    END IF;
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

-- Dump completed on 2018-06-16  2:55:48
