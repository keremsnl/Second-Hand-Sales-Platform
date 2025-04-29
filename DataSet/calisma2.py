#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Mon Apr 28 20:37:55 2025

@author: keremberk
"""

import pandas as pd

import numpy as np

from statsmodels.graphics.gofplots import qqplot

from matplotlib import pyplot

# CSV dosyasını okuma
veri2 = pd.read_csv('dataset.csv')
veri2 = veri2[pd.to_numeric(veri2['Stress_Level'], errors='coerce').notnull()]

olcumler = np.array([17, 160, 234, 149, 145, 107, 197, 75, 201, 225, 211, 119, 
              157, 145, 167, 244, 163, 114, 145,  170, 112, 185, 202, 146,
              203, 224, 203, 114, 188, 156, 187, 154, 177, 95, 165, 50, 110, 
              216, 198, 151, 176, 135, 155, 84, 251, 173, 131, 207, 121, 120])

pyplot.hist(veri2.Stress_Level)
qqplot(veri2.Stress_Level, line='s')
#stats.probplot(olcumler, dist="norm",plot=pylab)
#pylab.show()
#pd.DataFrame(olcumler).plot.hist()
