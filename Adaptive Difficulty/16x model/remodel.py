import numpy as np
from scipy.optimize import curve_fit
import pandas as pd
import matplotlib.pyplot as plt
from DataAnalysisMultiModel import remodel1

waves = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/steering 1 waves.csv")
waves1 = waves.query('eCount == 1')
waves1h1 = waves1['Health1']
waves1s1 = waves1['ShotSpeed1']
waves1f1 = waves1['FireRate1']
waves1hits = waves1['Hits']
prediction = remodel1((list(waves1h1),list(waves1hits),list(waves1s1),list(waves1hits),list(waves1f1),list(waves1hits),list(waves1hits)),10)
'''
meanpointprops = dict(marker='o', markeredgecolor='black', markerfacecolor='red')
fig, ax = plt.subplots()
ax.boxplot(prediction,  meanprops=meanpointprops, showmeans=True)
plt.show
'''
subFit1 = [0.088, 0.129, 1.325, 0.441, 0.067, 0.011, 3.579, -0.443]
fit1 = [1.000, 1.344, 0.516, 0.656,  - 0.037]

subFit2 = [0.080, 0.839, 19.006, 0.523, -0.994, 0.008, 5.294, -1.161]
fit2 = [-0.912, -0.773, -0.397, 1.237, -0.251, 1.343, -0.638, 0.081]

subFit3 = [0.081, 1.222, 1.567, 0.512, 0.743, 0.006, 7.454, -2.816]
fit3 = [1.841, 1.903, 2.547, 0.437, -13.057]

subFit4 = [1.317, 0.872, 0.038, 1.803, 0.008, 6.685, -1.556]
fit4 = [1.248, 1.324, 0.987, 0.914, 0.109, -2.005]

subFit5 = [0.559, 0.890, 0.041, 1.869, 0.010, 6.699, -0.948]
fit5 = [1.732, 1.000, 1.650, 1.597, 1.373, 0.190, -8.719]

subFit6 = [0.896, 0.958, 0.720, 0.040, 2.001, 0.010, 6.870, -0.755]
fit6 = [0.867, 0.843, 0.753, 0.732, 0.634, 0.532, 0.276, -6.928]

