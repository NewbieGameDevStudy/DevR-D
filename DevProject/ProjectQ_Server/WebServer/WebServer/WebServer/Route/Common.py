from linecache import cache
import Route.Define
import json

class ObjRespBase(object):
    def __init__(self):
        self.responseCode = 0
        self.ig_dbCache = []
        self.ig_responseCache = []
        self.ig_fieldValueCache = {}
        self.ig_queryStr = ""
        self.ig_fieldType = {}
        self.ig_resp = {}
        
    def initFieldDBQueryCache(self):
        for key, value in self.__dict__.items():
            if 'ig' in key or key == 'responseCode':
                continue
            
            if 'db_' in key:
                convertType = type(value)
                convertStr = key[3:]
                if convertType.__name__ == 'str':
                    self.ig_dbCache.append('c%s' % convertStr)

                elif convertType.__name__ == 'int':
                    self.ig_dbCache.append('i%s' % convertStr)
                                     
                self.ig_fieldType[convertStr] = convertType
                
                self.ig_responseCache.append(convertStr)
                self.ig_fieldValueCache[convertStr] = value
                
                dbKey = key[3:]
                self.ig_resp[dbKey] = value
            else:                  
                self.ig_resp[key] = value
        
        lastIdx = len(self.ig_dbCache) - 1
        for idx, str in enumerate(self.ig_dbCache):
            self.ig_queryStr += str
            if idx != lastIdx:
                self.ig_queryStr += ", "
    
    def getRenewFieldDBCache(self, fieldChangeDict = None):       
        if fieldChangeDict is None:
            return self.ig_fieldValueCache.values()
        
        for key, value in fieldChangeDict.items():
            if key in self.ig_fieldValueCache:
                self.ig_fieldValueCache[key] = value
        
        return self.ig_fieldValueCache.values()
    
    def getConvertToResponse(self, resultList, responseCode):
        convertList = list(resultList)
        for key in self.ig_resp.keys():
            if not convertList:
                break
            self.ig_resp[key] = convertList.pop(0)
            
        return self.ig_resp
    
    def getConvertToDBField(self, checkField, targetField):
        if not checkField in self.ig_fieldType:
            return None
        
        type = self.ig_fieldType[checkField]
        if type is str:
            return "\"%s\"" % targetField
        else:
            return targetField
            
class RespHandler(object):
    
    def __init__(self):
        self.collectReponse = {}
    
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
        