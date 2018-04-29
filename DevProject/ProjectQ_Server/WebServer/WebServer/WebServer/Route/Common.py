from linecache import cache
import Route.Define

class RespBase(object):
    def __init__(self):
        self.responseCode = 0
        self.ig_dbCache = []
        self.ig_responseCache = []
                
    #db convert fields
    def convertDBField(self):
        if self.ig_dbCache:
            return self.ig_dbCache
        for key, value in self.__dict__.items():
            if 'ig' in key or key == 'responseCode':
                continue
            if type(value).__name__ == 'str':
                self.ig_dbCache.append('c%s' % key)
            elif type(value).__name__ == 'int':
                self.ig_dbCache.append('i%s' % key)
                
            self.ig_responseCache.append(key)
                
        return self.ig_dbCache  
    
    def successToJson(self, resultList, responseCode = Route.Define.RESPONSE_OK):
        jsonResult = {}
        if not len(self.ig_responseCache) == len(resultList):
            jsonResult['responseCode'] = responseCode
            return
        
        jsonResult = dict(zip(self.ig_responseCache, resultList))
        jsonResult['responseCode'] = responseCode
        return jsonResult
    
    def errorToJson(self, responseCode):
        return {'responseCode':responseCode}