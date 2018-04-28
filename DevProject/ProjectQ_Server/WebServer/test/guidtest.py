'''
Created on 2018. 3. 28.

@author: namoeye
'''
import unittest
import test
import time

class Test(unittest.TestCase):
    
    guid = test.Guid(0)
    guids = {}
    
    def testName(self):
        index = 0
        while True:
            #time.sleep()
            key = self.guid.createGuid()
                        
            if key in self.guids.keys():
                print("failed, index : {0}".format(key))
                self.assertFalse("guid")
                break
            
            self.guids[key] = index 
            
            if len(self.guids) == 1000000:
                print("clear")
                self.assertTrue("guid")
                break                   
            
            index += 1
            
        print("end")


if __name__ == "__main__":
    #import sys;sys.argv = ['', 'Test.testName']
    unittest.main()