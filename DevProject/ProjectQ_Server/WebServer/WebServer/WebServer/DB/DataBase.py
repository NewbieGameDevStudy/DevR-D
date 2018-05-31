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
        try:
            conn = pymysql.connect(host="localhost", user="root", password="1234567890", db="gamedb", charset='utf8')
        except:
            print("DB not Connect")
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
            
    def _selectListQuery(self, queryStr):
        try:
            conn = self._dbPool.connect()
            with conn.cursor() as cursor:
                cursor.execute(queryStr)
                result = cursor.fetchall()
                return result
        finally:
            conn.close()  
    
    def selectQuery(self, table, matchDataStr, matchInDataStr, selectQueryStr):    
        findStr = "select %s from %s where %s = %s" % (selectQueryStr, table, matchDataStr, matchInDataStr)        
        return self._selectQuery(findStr)
    
    def customSelectQuery(self, queryStr):
        return self._selectQuery(queryStr)
    
    def customeSelectListQuery(self, queryStr):
        return self._selectListQuery(queryStr)
    
    def customInsertQuery(self, queryStr):
        return self._insertQuery(queryStr)
        
db = DBConnection()

        
