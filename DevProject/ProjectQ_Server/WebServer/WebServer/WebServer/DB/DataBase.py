import sqlalchemy.pool as pool
import pymysql
import enum

class QueryType(enum.Enum):
    QUERY_UPDATE = 1
    QUERY_INSERT = 2
    QUERY_SELECT = 3
    
class DBConnection:
    def __init__(self):
        self._dbPool = pool.QueuePool(self._getconn, max_overflow=20, pool_size=10)
         
    def _getconn(self):
        conn = pymysql.connect(host='localhost', user='root', password='1234567890', db='gamedb')
        return conn
    
    def InsertQuery(self, queryStr):
        try:
            conn = self._dbPool.connect()
            with conn.cursor() as cursor:
                cursor.execute(queryStr)
                conn.commit()
        finally:
            conn.close() 
            
    def UpdateQuery(self, queryStr):
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
            
    def SelectQuery(self, findTable, findDataStr, matchDataStr, *outputArgs):
        findStr = "select"
        selectStr = ""
        lastIdx = len(outputArgs) - 1
        for idx, str in enumerate(outputArgs):
            selectStr += str
            if idx != lastIdx:
                selectStr += ", "
        
        findStr = findStr + " " + selectStr + " from " + findTable + " where " + findDataStr
        findStr = findStr + " = %s" % matchDataStr 
        
        return self._selectQuery(findStr)
        
db = DBConnection()

        
