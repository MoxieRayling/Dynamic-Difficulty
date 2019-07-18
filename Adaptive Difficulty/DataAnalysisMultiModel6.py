import numpy as np
from scipy.optimize import curve_fit
import pandas as pd
import matplotlib.pyplot as plt

he = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/health 6.csv")
ss = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/shotspeed 6.csv")
fr = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/firerate 6.csv")
waves = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/waves 6.csv")


def linModel(x,a,b):
    return a*x + b

def expModel(x,a,b,c):
    return np.exp(a*x)*b + c

def rexpModel(x,a,b,c):
    return np.exp(-1*a*x)*b + c

def logModel(x,a,b,c):
    return np.log(a*x)*b + c

def gaussModel(x,a,b,c):
    return a/np.sqrt(2*np.pi)*np.exp(-((x-c)/b)**2/2)

init_guess = [1,1,1]  

fit = curve_fit(logModel, he['HEALTH'], he['hits'], p0=init_guess, absolute_sigma=True)
ans,cov = fit
he_a, he_b, he_c = ans
#x = np.linspace(0,30)
#plt.scatter(he['HEALTH'], he['hits'], color = 'red')
#plt.plot(x, logModel(x,he_a,he_b,he_c), color = 'green')

init_guess = [1,1]  

fit = curve_fit(linModel, ss['SSBounds'], ss['hits'], p0=init_guess, absolute_sigma=True)
ans,cov = fit
ss_a, ss_b = ans
#x = np.linspace(0,51)
#plt.scatter(ss['SSBounds'], ss['hits'], color = 'red')
#plt.plot(x, linModel(x,ss_a,ss_b), color = 'green')

init_guess = [1,1,1]  

fit = curve_fit(rexpModel, fr['FIRE_RATE'], fr['hits'], p0=init_guess, absolute_sigma=True)
ans,cov = fit
fr_a, fr_b, fr_c = ans
x = np.linspace(5,120)
plt.scatter(fr['FIRE_RATE'], fr['hits'], color = 'red')
plt.plot(x, rexpModel(x,fr_a,fr_b,fr_c), color = 'green')
'''
def model1(X, a,b,c,d,e,f,g,h):
    he1,ss1,fr1,he2,ss2,fr2,he3,ss3,fr3,he4,ss4,fr4,he5,ss5,fr5,he6,ss6,fr6 = X
    return (a*(linModel(he1,he_a,he_b) + linModel(ss1*100,ss_a,ss_b) + rexpModel(fr1,fr_a,fr_b,fr_c)) +
    b*(linModel(he2,he_a,he_b) + linModel(ss2*100,ss_a,ss_b) + rexpModel(fr2,fr_a,fr_b,fr_c)) +
    c*(linModel(he3,he_a,he_b) + linModel(ss3*100,ss_a,ss_b) + rexpModel(fr3,fr_a,fr_b,fr_c)) +
    d*(linModel(he4,he_a,he_b) + linModel(ss4*100,ss_a,ss_b) + rexpModel(fr4,fr_a,fr_b,fr_c)) +
    e*(linModel(he5,he_a,he_b) + linModel(ss5*100,ss_a,ss_b) + rexpModel(fr5,fr_a,fr_b,fr_c)) +
    f*(linModel(he6,he_a,he_b) + linModel(ss6*100,ss_a,ss_b) + rexpModel(fr6,fr_a,fr_b,fr_c))
    )*g+h

init_guess = [1,1,1,1,1,1,1,1]  
fit = curve_fit(model1, 
(waves['Health1'], waves['ShotSpeed1'], waves['FireRate1'],waves['Health2'], waves['ShotSpeed2'], waves['FireRate2'],waves['Health3'], waves['ShotSpeed3'], waves['FireRate3'],
waves['Health4'], waves['ShotSpeed4'], waves['FireRate4'], waves['Health5'], waves['ShotSpeed5'], waves['FireRate5'], waves['Health6'], waves['ShotSpeed6'], waves['FireRate6']), 
waves['Hits'], 
p0=init_guess, 
absolute_sigma=True)
ans,cov = fit
fit_a, fit_b, fit_c, fit_d, fit_e, fit_f, fit_g, fit_h= ans
predict = np.around(model1((waves['Health1'],waves['ShotSpeed1'],waves['FireRate1'],waves['Health2'], waves['ShotSpeed2'], waves['FireRate2'],waves['Health3'], waves['ShotSpeed3'], waves['FireRate3'],
waves['Health4'], waves['ShotSpeed4'], waves['FireRate4'], waves['Health5'], waves['ShotSpeed5'], waves['FireRate5'], waves['Health6'], waves['ShotSpeed6'], waves['FireRate6']),
fit_a,fit_b,fit_c,fit_d, fit_e, fit_f, fit_g,fit_h))
error = np.absolute(waves['Hits']-predict)
#plt.fill_between(waves['WAVE_ID'],0.5,-0.5)
plt.scatter(waves['WAVE_ID'],waves['Hits'],color='red')
plt.scatter(waves['WAVE_ID'],predict,color='green')
#plt.scatter(waves['WAVE_ID'],error,color='blue')
plt.annotate("average error: " + '%.3f'%(sum(error)/len(error)),(-5,40))
plt.annotate("correct: " + str(len([e for e in error if e<0.5])),(-5,39))
plt.annotate("incorrect: "+str(len([e for e in error if e>=0.5])),(-5,38))
plt.annotate("accuracy: "+'%.3f'%(len([e for e in error if e<0.5])/len(error)),(-5,37))
subModel = [he_a,he_b,he_c,ss_a,ss_b,fr_a,fr_b,fr_c] 
print(['%.3f'%e for e in subModel])
print(['%.3f'%e for e in ans])
'''
plt.show()