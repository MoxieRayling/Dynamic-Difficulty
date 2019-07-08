import numpy as np
from scipy.optimize import curve_fit
import pandas as pd
import matplotlib.pyplot as plt

he = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/health 2.csv")
ss = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/shotspeed 2.csv")
fr = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/firerate 2.csv")
waves = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/waves 2.csv")


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

init_guess = [1,1]  

fit = curve_fit(linModel, he['HEALTH'], he['hits'], p0=init_guess, absolute_sigma=True)
ans,cov = fit
he_a, he_b = ans
#x = np.linspace(0,30)
#plt.scatter(he['HEALTH'], he['hits'], color = 'red')
#plt.plot(x, linModel(x,he_a,he_b), color = 'green')

init_guess = [1,1,1]  

fit = curve_fit(logModel, ss['SSBounds'], ss['hits'], p0=init_guess, absolute_sigma=True)
ans,cov = fit
ss_a, ss_b, ss_c = ans
#x = np.linspace(0,51)
#plt.scatter(ss['SSBounds'], ss['hits'], color = 'red')
#plt.plot(x, logModel(x,ss_a,ss_b,ss_c), color = 'green')

init_guess = [1,1,1]  

fit = curve_fit(rexpModel, fr['FIRE_RATE'], fr['hits'], p0=init_guess, absolute_sigma=True)
ans,cov = fit
fr_a, fr_b, fr_c = ans
#x = np.linspace(5,120)
#plt.scatter(fr['FIRE_RATE'], fr['hits'], color = 'red')
#plt.plot(x, rexpModel(x,fr_a,fr_b,fr_c), color = 'green')

def model1(X, a,b,c,d,e,f,g,h):
    he1,ss1,fr1,he2,ss2,fr2 = X
    return (a*linModel(he1,he_a,he_b) * b*logModel(ss1*100,ss_a,ss_b,ss_c) * c*rexpModel(fr1,fr_a,fr_b,fr_c) +
    d*linModel(he2,he_a,he_b) * e*logModel(ss2*100,ss_a,ss_b,ss_c) * f*rexpModel(fr2,fr_a,fr_b,fr_c))*g + h

init_guess = [1,1,1,1,1,1,1,1]  
fit = curve_fit(model1, 
(waves['Health1'], waves['ShotSpeed1'], waves['FireRate1'],waves['Health2'], waves['ShotSpeed2'], waves['FireRate2']), 
waves['Hits'], 
p0=init_guess, 
absolute_sigma=True)
ans,cov = fit
fit_a, fit_b, fit_c, fit_d, fit_e, fit_f, fit_g, fit_h= ans
print(ans)
predict = np.around(model1((waves['Health1'],waves['ShotSpeed1'],waves['FireRate1'],waves['Health2'], waves['ShotSpeed2'], waves['FireRate2']),fit_a,fit_b,fit_c,fit_d, fit_e, fit_f, fit_g,fit_h))
error = np.absolute(waves['Hits']-predict)
plt.fill_between(waves['WAVE_ID'],0.5,-0.5)
plt.scatter(waves['WAVE_ID'],waves['Hits'],color='red')
plt.scatter(waves['WAVE_ID'],predict,color='green')
plt.scatter(waves['WAVE_ID'],error,color='blue')
plt.annotate("average error: " + '%.3f'%(sum(error)/len(error)),(-5,9))
plt.annotate("correct: " + str(len([e for e in error if e<0.5])),(-5,8.5))
plt.annotate("incorrect: "+str(len([e for e in error if e>=0.5])),(-5,8))
plt.annotate("accuracy: "+'%.3f'%(len([e for e in error if e<0.5])/len(error)),(-5,7.5))


plt.show()