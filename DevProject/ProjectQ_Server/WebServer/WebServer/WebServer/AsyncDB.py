from contextlib import contextmanager
from DB import dbConnection
import asyncio

class AsyncDBExcute:

    @contextmanager
    def asyncIO(self):
        try:
            loop = asyncio.get_event_loop()
            future = asyncio.Future()
            yield (future, loop)
        finally:
            loop.close()                
            
    async def selectDb(self, future, queryStr):
        result = dbConnection.select_query(queryStr)
        future.set_result(result)
 
    def asyncSelectMethod(self, queryStr):
        with self.asyncIO() as (future, loop):
            asyncio.ensure_future(self.selectDb(future, queryStr))
            loop.run_until_complete(future)
            result = future.result()
        return result

     
asyncFunc = AsyncDBExcute()
