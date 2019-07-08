import numpy as np
from scipy.optimize import curve_fit
import pandas as pd
import matplotlib.pyplot as plt

d = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/test 78 waves.csv")


def linModel(x,a,b,c):
    return a*x + b

def expModel(x,a,b,c):
    return np.exp(a*x)*b + c

def model(X,a,b,c,d,e,f,g,h,i):
    ec,fr1,ss1,he1,fr2,ss2,he2,fr3,ss3,he3,fr4,ss4,he4,fr5,ss5,he5,fr6,ss6,he6 = X
    if ec[0] == 1:
        return linModel(fr1,a,b,c) * linModel(ss1,d,e,f) * linModel(he1,g,h,i)
    elif ec[0] == 2:
        return linModel(fr1,a,b,c) * linModel(ss1,d,e,f) * linModel(he1,g,h,i) + linModel(fr2,a,b,c) * linModel(ss2,d,e,f) * linModel(he2,g,h,i)
    elif ec[0] == 3:
        return linModel(fr1,a,b,c) * linModel(ss1,d,e,f) * linModel(he1,g,h,i) + linModel(fr2,a,b,c) * linModel(ss2,d,e,f) * linModel(he2,g,h,i) + linModel(fr3,a,b,c) * linModel(ss3,d,e,f) * linModel(he3,g,h,i)
    elif ec[0] == 4:
        return linModel(fr1,a,b,c) * linModel(ss1,d,e,f) * linModel(he1,g,h,i) + linModel(fr2,a,b,c) * linModel(ss2,d,e,f) * linModel(he2,g,h,i) + linModel(fr3,a,b,c) * linModel(ss3,d,e,f) * linModel(he3,g,h,i) + linModel(fr4,a,b,c) * linModel(ss4,d,e,f) * linModel(he4,g,h,i)
    elif ec[0] == 5:
        return linModel(fr1,a,b,c) * linModel(ss1,d,e,f) * linModel(he1,g,h,i) + linModel(fr2,a,b,c) * linModel(ss2,d,e,f) * linModel(he2,g,h,i) + linModel(fr3,a,b,c) * linModel(ss3,d,e,f) * linModel(he3,g,h,i) + linModel(fr4,a,b,c) * linModel(ss4,d,e,f) * linModel(he4,g,h,i) + linModel(fr5,a,b,c) * linModel(ss5,d,e,f) * linModel(he5,g,h,i)
    elif ec[0] == 6:
        return linModel(fr1,a,b,c) * linModel(ss1,d,e,f) * linModel(he1,g,h,i) + linModel(fr2,a,b,c) * linModel(ss2,d,e,f) * linModel(he2,g,h,i) + linModel(fr3,a,b,c) * linModel(ss3,d,e,f) * linModel(he3,g,h,i) + linModel(fr4,a,b,c) * linModel(ss4,d,e,f) * linModel(he4,g,h,i) + linModel(fr5,a,b,c) * linModel(ss5,d,e,f) * linModel(he5,g,h,i) + linModel(fr6,a,b,c) * linModel(ss6,d,e,f) * linModel(he6,g,h,i)
    



init_guess = [1,1,1,1,1,1,1,1,1]
fit = curve_fit(model, 
(d['eCount'],d['FireRate1'],d['ShotSpeed1'],d['Health1'],d['FireRate2'],d['ShotSpeed2'],d['Health2'],d['FireRate3'],d['ShotSpeed3'],d['Health3'],d['FireRate4'],d['ShotSpeed4'],d['Health4'],d['FireRate5'],d['ShotSpeed5'],d['Health5'],d['FireRate6'],d['ShotSpeed6'],d['Health6']),
d['Hits'],
p0=init_guess, absolute_sigma=True)

ans,cov = fit
fit_a, fit_b, fit_c, fit_d, fit_e, fit_f, fit_g, fit_h, fit_i = ans
z = model((d['eCount'],d['FireRate1'],d['ShotSpeed1'],d['Health1'],d['FireRate2'],d['ShotSpeed2'],d['Health2'],d['FireRate3'],d['ShotSpeed3'],d['Health3'],d['FireRate4'],d['ShotSpeed4'],d['Health4'],d['FireRate5'],d['ShotSpeed5'],d['Health5'],d['FireRate6'],d['ShotSpeed6'],d['Health6']),
fit_a,fit_b, fit_c, fit_d, fit_e, fit_f, fit_g, fit_h, fit_i)
error = np.sqrt((d['Hits']-z)**2)
#plt.scatter(d['WAVE_ID'],z, color = 'green')
plt.fill_between(d['WAVE_ID'],0.5,0)
plt.scatter(d['WAVE_ID'],error, color = 'red')
plt.annotate("average error: " + '%.3f'%(sum(error)/len(error)),(-0.5,4))
plt.annotate("correct: " + str(len([e for e in error if e<0.5])),(-0.5,3.5))
plt.annotate("incorrect: "+str(len([e for e in error if e>=0.5])),(-0.5,3))
#plt.annotate(init_guess,(-0.5,4.5))
plt.annotate([float('%.3f'%(a)) for a in ans],(-0.5,4.5))
plt.show()