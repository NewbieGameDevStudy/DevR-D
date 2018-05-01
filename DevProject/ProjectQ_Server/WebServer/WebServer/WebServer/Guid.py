from datetime import datetime
from time import sleep

class Guid(object):
    
    def __init__(self):
        self.machieId = 0
        self.uniqueCount = 0
        self.milliSecond = 0
        self.maxMiliSecond = 0
        self.maxUniqueCount = 0
        self.prevMilliSecond = 0
        print(self.maxMiliSecond)
        
    def setGuid(self, macheidId):
        self.machieId = macheidId
        dtobj = datetime.strptime('20.12.2016 00:00:00',
                                   '%d.%m.%Y %H:%M:%S')
        self.milliSecond = (int)(dtobj.timestamp() * 1000)
        self.maxMiliSecond = pow(2,42)-1
        self.maxUniqueCount = pow(2,14)-1
            
    def _getNowMs(self):
        nowDateStr = datetime.now().strftime('%d.%m.%Y %H:%M:%S')
        nowDatetime = datetime.strptime(nowDateStr, '%d.%m.%Y %H:%M:%S')
        nowMilliSecond = (int)(nowDatetime.timestamp() * 1000)
        return nowMilliSecond
    
    def createGuid(self):
        nowMilliSecond = self._getNowMs()
        
        if self.prevMilliSecond == nowMilliSecond:
            sleep(0.01)
            nowMilliSecond = self._getNowMs()
        
        ms = nowMilliSecond - self.milliSecond
        
        if ms >= self.maxMiliSecond:
            self.milliSecond = nowMilliSecond
            sleep(0.1)
            nowMilliSecond = self._getNowMs()
            ms = nowMilliSecond - self.milliSecond
        
        self.uniqueCount += 1
        
        if self.uniqueCount >= self.maxUniqueCount:
            self.uniqueCount = 0
            sleep(0.1)
            nowMilliSecond = self._getNowMs()
            
        self.prevMilliSecond = nowMilliSecond
        
        guid = ms << 22
        guid |= self.uniqueCount << 8
        guid |= self.machieId
                
        return guid