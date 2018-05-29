'''
Created on 2018. 5. 29.

@author: namoeye
'''
import csv
import os
from os import listdir
from os.path import isfile, join
from Util import SingletonInstane

class MetaDataMgr(SingletonInstane):
    def __init__(self):
        self.metaDataDict = {}
        
    def loadData(self):
        path = os.getcwd() + '\\CsvDataTable'
        onlyfiles = [f for f in listdir(path) if isfile(join(path, f))]
        for fileName in onlyfiles:
            keyStr = fileName.split('.')
            self.metaDataDict[keyStr[0]] = {}
            csvlist = self.metaDataDict[keyStr[0]]
            file = './CsvDataTable/' + fileName
            with open(file, 'r') as f:
                reader_csv = csv.DictReader(f)
                idx = 0 
                for row in reader_csv:
                    first = True             
                    rowDict = {}
                    for key, val in row.items():
                        rowDict[key] = val
                        if first:
                            idx = int(val)
                            first = False                    
                    csvlist[idx] = rowDict
                    
    def getMetaData(self, metaStr, metaId):
        if not metaStr in self.metaDataDict:
            return None
        
        findDict = self.metaDataDict[metaStr]
        
        if not metaId in findDict:
            return None
        
        return findDict[metaId]

metaData = MetaDataMgr.instance()
metaData.loadData()
