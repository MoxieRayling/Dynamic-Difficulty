import numpy as np
from scipy.optimize import curve_fit
import pandas as pd
import matplotlib.pyplot as plt

d = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/test 71.csv")

def expModel(x,a,b,c):
    return a * np.exp(-b * x + c) 
def logModel(x,a,b,c):
    return a * np.log(b*x + c)

def model(x,a,b):
    return a*x+b

init_guess = [1,1]
fit = curve_fit(model, d['HitCredit'],d['HEALTH'],p0=init_guess, absolute_sigma=True)
ans,cov = fit
fit_a, fit_b = ans

plt.errorbar(d['HitCredit'],d['HEALTH'],fmt='b.')
plt.ylabel("Fire Delay")
plt.xlabel("Hits per No. of Enemies")

t = np.linspace(0,10)
plt.plot(t,model(t,fit_a,fit_b), label="model")
plt.show()