'''
Created on 2018. 4. 29.

@author: namoeye
'''

class Helper():
    @staticmethod
    def Response(respCode, *respDatas):
        resp = {}
        resp["responseCode"] = respCode
        resp