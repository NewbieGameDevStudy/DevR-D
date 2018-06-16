'''
Created on 2018. 5. 29.

@author: namoeye
'''
import csv
import os
import re
from os import listdir
from os.path import isfile, join
from Util import SingletonInstane

class MetaDataMgr(SingletonInstane):
    def __init__(self):
        self.metaDataDict = {}
        
    def loadData(self):
        path = os.getcwd() + '\\CsvDataTable'
        onlyfiles = [f for f in listdir(path) if isfile(join(path, f))]
        
        boolRegx = re.compile("TRUE", re.I)
        intRegx = re.compile("(^[0-9]*$)")
        strRegx = re.compile('[a-zA-Z]+')
        
        for fileName in onlyfiles:
            try:
                keyStr = fileName.split('.')
                self.metaDataDict[keyStr[0]] = {}
                csvlist = self.metaDataDict[keyStr[0]]
                file = './CsvDataTable/' + fileName
                with open(file, 'r', encoding='utf-8-sig') as f:
                    reader_csv = csv.DictReader(f)
                    idx = 0 
                    for row in reader_csv:
                        first = True             
                        rowDict = {}
                        for key, val in row.items():                        
                            if first:
                                idx = int(val)
                                first = False
                                
                            #bRegx = boolRegx.match(val)
                            
                            iRegx = intRegx.match(val)
                            if iRegx:
                                rowDict[key] = int(val)
                                continue
                            rowDict[key] = val
#                             sRegx = strRegx.match(val)
#                             if not sRegx:
#                                 rowDict[key] = int(val)
                                                                                            
                        csvlist[idx] = rowDict
            except Exception as e:
                print(str(e))
                print(fileName)
                
    def getMetaData(self, metaStr, metaId):
        findDict = self.getMetaDatas(metaStr)
        if findDict is None:
            return None
        
        if not metaId in findDict:
            return None
        
        return findDict[metaId]
    
    def getMetaDatas(self, metaStr):
        if not metaStr in self.metaDataDict:
            return None
        
        return self.metaDataDict[metaStr]

metaData = MetaDataMgr.instance()
metaData.loadData()
