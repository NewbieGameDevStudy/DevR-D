from linecache import cache
import Route.Define
import json
from abc import ABC, abstractmethod, abstractclassmethod

class BaseObjResp(ABC):
    def __init__(self):
        self.respDict = {}
        
    def initRespCache(self):
        for key, value in self.__dict__.items():
            if 'resp' in key or 'ig' in key:
                continue
            
            self.respDict[key] = value
            
    def updateResp(self, resultList):
        convertList = list(resultList)
        for key in self.respDict.keys():
            if not convertList:
                break
            self.respDict[key] = convertList.pop(0)
        return self.respDict
    
    @abstractclassmethod
    def getResp(self):        
        pass
    
class BaseContainerResp(ABC):
    def __init__(self):
        self.container = {}
        
    def getContainerResp(self):
        resp = {}
        for value in self.container.values():
            if not value.__class__.__name__ in resp:
                resp[value.__class__.__name__] = []
            resp[value.__class__.__name__].append(value.getResp())
            
        return resp
    
    @abstractclassmethod
    def updateContainer(self, updateList):
        pass
            
            
class RespHandler(object):
    
    def __init__(self):
        self.collectReponse = {}
        
    def mergeResp(self, mergeDict):        
        primaryDict = self.collectReponse
        for key, value in mergeDict.items():
            primaryDict[key]= value
                     
    def getResponse(self, responseCode):
        respDict = self.collectReponse.copy()
        self.collectReponse.clear()
        respDict['responseCode'] = responseCode
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
        