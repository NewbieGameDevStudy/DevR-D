from linecache import cache
import Route.Define

class RespBase(object):
    def __init__(self):
        self.responseCode = 0
        self.ig_dbCache = []
        self.ig_responseCache = []
        self.ig_fieldValueCache = {}
        self.ig_queryStr = ""
        
    #db convert fields
    def convertDBField(self):
        for key, value in self.__dict__.items():
            if 'ig' in key or key == 'responseCode':
                continue
            convertType = type(value)
            if convertType.__name__ == 'str':
                self.ig_dbCache.append('c%s' % key)
            elif convertType.__name__ == 'int':
                self.ig_dbCache.append('i%s' % key)
                       
            self.ig_responseCache.append(key)
            self.ig_fieldValueCache[key] = value
        
        lastIdx = len(self.ig_dbCache) - 1
        for idx, str in enumerate(self.ig_dbCache):
            self.ig_queryStr += str
            if idx != lastIdx:
                self.ig_queryStr += ", "
    
    def convertDBFieldValue(self, fieldChangeDict = None):       
        if fieldChangeDict is None:
            return self.ig_fieldValueCache.values()
        
        for key, value in fieldChangeDict.items():
            if key in self.ig_fieldValueCache:
                self.ig_fieldValueCache[key] = value
        
        return self.ig_fieldValueCache.values()
    
    def successToJson(self, resultList, responseCode = Route.Define.RESPONSE_OK):
        jsonResult = {}
        if not len(self.ig_responseCache) == len(resultList):
            jsonResult['responseCode'] = responseCode
            return jsonResult
        
        jsonResult = dict(zip(self.ig_responseCache, resultList))
        jsonResult['responseCode'] = responseCode
        return jsonResult
    
    def errorToJson(self, responseCode):
        return {'responseCode':responseCode}
    
    @staticmethod
    def errorResponse(responseCode):
        return {'responseCode':responseCode}
    
    @staticmethod
    def successResponse(responseCode, respDict): 
        dictResp = {}
        if not respDict is None:
            for resp in respDict:
                dictResp[resp] = respDict[resp]
        
        dictResp['responseCode'] = responseCode
        return dictResp