from MetaDataMgr import metaData
from Route import Common
import datetime
import time

class Mail(Common.BaseObjResp):
    def __init__(self):
        super(Mail, self).__init__()
        self.mailIdx = 0            #0
        self.senderAccountId = 0    #2 ~ 7 
        self.sender = ""
        self.title = "" 
        self.body = ""
        self.sendTime = 0
        self.expireTime = 0
        self.readDone = 0
        self.mailType = 0
        
        #senderInfo
        self.senderPortrait = 0
        self.senderLv = 0
        
        self.initRespCache()
    
    def loadValueFromDB(self, loadDatas):
        self.mailIdx = loadDatas[0]
        self.senderAccountId = loadDatas[1]
        self.sender = loadDatas[2]
        self.title = loadDatas[3] 
        self.body = loadDatas[4]
        
        self.sendTime = int(loadDatas[5].timestamp()) 
        self.expireTime = int(loadDatas[6].timestamp())
        self.readDone = loadDatas[7]
        self.mailType = loadDatas[8]
        
        self.senderPortrait = loadDatas[9]
        self.senderLv = loadDatas[10]

    def getResp(self):
        return self.ig_respDict