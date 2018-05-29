'''
Created on 2018. 5. 29.

@author: namoeye
'''
import csv
import os
from os import listdir
from os.path import isfile, join

gg = {}

path = os.getcwd()

#C:\Users\namoeye\Desktop\dev\Server\DevProject\ProjectQ_Server\WebServer\test

path += '\\data'

onlyfiles = [f for f in listdir(path) if isfile(join(path, f))]

dd = 0
with open('AnimalDataTable.csv', 'r') as f:
    #reader_csv = csv.reader(f, delimiter=',')
    reader_csv = csv.DictReader(f)
    for row in reader_csv:
        for key, d in row.items():
            gg[key] = d
    
    dd = 1