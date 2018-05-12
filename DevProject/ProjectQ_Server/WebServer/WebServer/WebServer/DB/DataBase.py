import sqlalchemy.pool as pool
import pymysql
import enum

class QueryType(enum.Enum):
    QUERY_UPDATE = 1
    QUERY_INSERT = 2
    QUERY_SELECT = 3
    
class DBConnection:
    def __init__(self):
        self._dbPool = pool.QueuePool(self._getconn, max_overflow=30, pool_size=30)
         
    def _getconn(self):
        conn = pymysql.connect(host="localhost", user="root", password="1234567890", db="gamedb")
        return conn
    
    def _insertQuery(self, queryStr):
        try:
            conn = self._dbPool.connect()
            with conn.cursor() as cursor:
                cursor.execute(queryStr)
                conn.commit()
        finally:
            conn.close() 
            
    def updateQuery(self, queryStr):
        try:
            conn = self._dbPool.connect()
            with conn.cursor() as cursor:
                cursor.execute(queryStr)
                conn.commit()
        finally:
            conn.close() 
            
    def _selectQuery(self, queryStr):
        try:
            conn = self._dbPool.connect()
            with conn.cursor() as cursor:
                cursor.execute(queryStr)
                result = cursor.fetchone()
                return result
        finally:
            conn.close() 
    
    def insertQuery(self, insertTable, insertQueryStr, insertValues):
        inputStr = ""
        lastIdx = len(insertValues) - 1
        for idx, data in enumerate(insertValues):
            inputStr += str(data)
            if idx != lastIdx:
                inputStr += ", "
       
        insertStr = "insert into %s (%s) value (%s)" % (insertTable, insertQueryStr, inputStr)
        return self._insertQuery(insertStr)
            
    def selectQuery(self, findTable, matchDataStr, matchInDataStr, selectQueryStr):    
        findStr = "select %s from %s where %s = %s" % (selectQueryStr, findTable, matchDataStr, matchInDataStr)        
        return self._selectQuery(findStr)
        
db = DBConnection()

        
