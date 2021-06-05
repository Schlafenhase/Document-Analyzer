# -*- coding: utf-8 -*-
"""
Created on Sat May 29 17:32:08 2021

@author: Jesus Sandoval
"""
import pandas as pd
import pickle

class SwearAnalysisService:
    def __init__(self, profanityPath, vectorizerPath):
        self.loaded_model = pickle.load(open(profanityPath, "rb"))
        self.vectorizer = pickle.load(open(vectorizerPath, "rb"))
    
    def makeSwearAnalysis(self, text):
        a = 0
        for word in text.split():
            X = self.vectorizer.transform([word])
            result = self.loaded_model.predict(X)

            if result[0] == 1:
                a +=1
            else:
                None

        return a
        



# filename = 'profanity.sav'
# loaded_model = pickle.load(open(filename, 'rb'))


# vectorizer_filename = 'vectorizer.sav'
# vectorizer = pickle.load(open(vectorizer_filename, 'rb'))
# with open('TOKENIZE5.txt','r') as data:
#     a = 0
#     for line in data:
#         for word in line.split():
#             X = vectorizer.transform([word])
#             result = loaded_model.predict(X)
            
#             if result[0] == 1:
#                 a +=1
#             else:
#                 None
    
#     if a == 0:          
#         print("This document is ok buddy")
#     else:
#         print("This document needs Jesus " + str(a) + " profanities has been found")
            