import asyncio
import datetime
from builtins import staticmethod
   
async def add(a,b):
    print("add{0} + {1}".format(a,b))
    await asyncio.sleep(5.0)
    return a + b

async def print_add(a,b):
    result= await loop.run_in_executor(None, add, a,b)
    #result = await add(a, b)
    print("print _add:{0} + {1} = {2}".format(a, b, result))
    

# loop = asyncio.get_event_loop()
# loop.run_until_complete(print_add(1,2))
# loop.close()
# print("nn")

async def display_date(loop):
    end_time = loop.time() + 5.0
    while True:
        print(datetime.datetime.now())
        if(loop.time() + 1.0) >= end_time:
            break;
        await asyncio.sleep(1)
        
        
def testMethod():
    loop = asyncio.get_event_loop()
    loop.run_until_complete(display_date(loop))
    loop.close()
    print("nn")
    
# print("before")
# testMethod()
# print("after")

entitys = []
entitys.append(1)
entitys.append(2)
entitys.append(3)
entitys.append(4)

# for x in range(10):
#     entitys.append(x)

class TestEntity:
    def __init__(self, id):
        self.id = id
        
    def showId(self):
        print(self.id)

class HelperEntity:
    @staticmethod
    def _getEntity():
        return map(lambda x: TestEntity(x), entitys)
    
    @staticmethod
    def showEntitys():
        map(lambda x: x.showId(), HelperEntity._getEntity())
        
from functools import reduce
        
test = reduce(lambda x, y: x + y, entitys)        
HelperEntity.showEntitys()