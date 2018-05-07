'''
Created on 2018. 4. 29.

@author: namoeye
'''

from flask import jsonify, request, session
import json

class dictTest():
    def __init__(self):
        self.a = 10
        self.b = {}
        self.b['b1'] = 2
        self.b['b2'] = 3
        
        self.c = []
        self.c.append(4)
        self.c.append(5)
        
    def get(self):
        return self.__dict__
    
    def tt(self):
        for key, value in self.__dict__.items():
            print(key)
            print(value)
    
    
# dc = dictTest()
# print(dc.get())
# 
# cc = 'i'
# c = json.dumps(cc)
# 
# print(c)
# d = json.dumps(dc.get())
# 
# print(d)
# 
# f = dc.tt()

c = []
c.append('1')
c.append('2')
c.append('3')

d = []
d.append(1)
d.append(2)
d.append(3)

f = dict(zip(c, 0))

print(type(f))
print(f)

d = 'ig_dd'

if 'ig' in d:
    print('find')

