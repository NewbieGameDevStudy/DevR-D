'''
Created on 2018. 5. 1.

@author: namoeye
'''

import Guid

guidInst = Guid.Guid()

class SingletonInstane:
    __instance = None
    
    @classmethod
    def __getInstance(cls):
        return cls.__instance
    
    @classmethod
    def instance(cls, *args, **kargs):
        cls.__instance = cls(*args, **kargs)
        cls.instance = cls.__getInstance
        return cls.__instance