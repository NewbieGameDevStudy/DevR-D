from linecache import cache
import Route.Define
import json

class ObjRespBase(object):
    def __init__(self):
        self.respDict = {}
        
    def initRespCache(self):
        for key, value in self.__dict__.items():
            if 'resp' in key:
                continue
            
            self.respDict[key] = value
            
    def updateResp(self, resultList):
        convertList = list(resultList)
        for key in self.respDict.keys():
            if not convertList:
                break
            self.respDict[key] = convertList.pop(0)
        return self.respDict
    
    def getResp(self):
        return self.respDict
    
            
class RespHandler(object):
    
    def __init__(self):
        self.collectReponse = {}
    
    def addResponse
    
    def getResponse(self, responseKey, responseDatas):
        respDict = {}
        if responseKey == "base":
            respDict = responseDatas
            return respDict
        
        respDict[responseKey] = responseDatas
        return respDict
    
    def errorResponse(self, responseCode):
        return {'responseCode':responseCode}
    
    def customeResponse(self, responseCode, respDict): 
        dictResp = {}
        if not respDict is None:
            for resp in respDict:
                dictResp[resp] = respDict[resp]
        
        dictResp['responseCode'] = responseCode
        return dictResp

respHandler = RespHandler()
        