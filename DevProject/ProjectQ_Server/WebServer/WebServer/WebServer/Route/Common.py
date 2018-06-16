from linecache import cache
import Route.Define
import json
from abc import ABC, abstractmethod, abstractclassmethod

class BaseObjResp(ABC):
    def __init__(self):
        self.ig_respDict = {}
        
    def initRespCache(self):
        for key, value in self.__dict__.items():
            if 'resp' in key or 'ig' in key:
                continue
            
            self.ig_respDict[key] = value
            
    def initResp(self, resultList):
        convertList = list(resultList)
        for key in self.ig_respDict.keys():
            if not convertList:
                break
            self.ig_respDict[key] = convertList.pop(0)
        return self.ig_respDict
    
    @abstractclassmethod
    def getResp(self):        
        pass
    
    def syncToResp(self):
        syncFields = self.__dict__.items()
        
        self.ig_respDict.clear()
        for key, value in syncFields:
            if "ig" in key:
                continue
            self.ig_respDict[key] = value
    
class BaseContainerResp(ABC):
    def __init__(self):
        self.ig_container = {}
        self.ig_respDict = {}
        
    def initRespCache(self):
        for key, value in self.__dict__.items():
            if 'resp' in key or 'ig' in key:
                continue
            
            self.ig_respDict[key] = value
            
    def loadBasicInitDataFromDB(self, updateList):
        pass
    
    def syncToResp(self):
        syncFields = self.__dict__.items()
        
        self.ig_respDict.clear()
        for key, value in syncFields:
            if "ig" in key:
                continue
            self.ig_respDict[key] = value
        
    def getContainerResp(self):
        baseName = self.__class__.__name__
        baseResp = {baseName:{}}        
        resp = baseResp[baseName]
        
        if len(self.ig_respDict) > 0:
            for key, value in self.ig_respDict.items():            
                resp[key] = value
        
        for value in self.ig_container.values():
            if not value.__class__.__name__ in resp:
                resp[value.__class__.__name__] = []
            resp[value.__class__.__name__].append(value.getResp())
            
        return baseResp
    
    @abstractclassmethod
    def loadValueFromDB(self, updateList):
        pass

class BaseRoute(object):
    def getSession(self, request):
        session = request.cookies.get("Session")
        return session                        
            
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
    
    def customeResponse(self, responseCode, respDict = None): 
        dictResp = {}
        if not respDict is None:
            for resp in respDict:
                dictResp[resp] = respDict[resp]
        
        dictResp['responseCode'] = responseCode
        return dictResp

respHandler = RespHandler()
        