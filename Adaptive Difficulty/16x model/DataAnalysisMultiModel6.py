import numpy as np
from scipy.optimize import curve_fit
import pandas as pd
import matplotlib.pyplot as plt

he = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/health 6.csv")
ss = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/shotspeed 6.csv")
fr = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/firerate 6.csv")
waves = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/waves 6.csv")

test = pd.DataFrame()
train = pd.DataFrame()
if waves.shape[0] > 500:
    test = waves[:500]
    train = waves[500:]

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
#x = np.linspace(5,120)
#plt.scatter(fr['FIRE_RATE'], fr['hits'], color = 'red')
#plt.plot(x, rexpModel(x,fr_a,fr_b,fr_c), color = 'green')

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
(train['Health1'], train['ShotSpeed1'], train['FireRate1'],train['Health2'], train['ShotSpeed2'], train['FireRate2'],train['Health3'], train['ShotSpeed3'], train['FireRate3'],
train['Health4'], train['ShotSpeed4'], train['FireRate4'], train['Health5'], train['ShotSpeed5'], train['FireRate5'], train['Health6'], train['ShotSpeed6'], train['FireRate6']), 
train['Hits'], 
p0=init_guess, 
absolute_sigma=True)
ans,cov = fit
fit_a, fit_b, fit_c, fit_d, fit_e, fit_f, fit_g, fit_h= ans
predict = np.around(model1((test['Health1'],test['ShotSpeed1'],test['FireRate1'],test['Health2'], test['ShotSpeed2'], test['FireRate2'],test['Health3'], test['ShotSpeed3'], test['FireRate3'],
test['Health4'], test['ShotSpeed4'], test['FireRate4'], test['Health5'], test['ShotSpeed5'], test['FireRate5'], test['Health6'], test['ShotSpeed6'], test['FireRate6']),
fit_a,fit_b,fit_c,fit_d, fit_e, fit_f, fit_g,fit_h))
error = test['Hits']-predict
absError = np.absolute(test['Hits']-predict)
'''
#plt.fill_between(waves['WAVE_ID'],0.5,-0.5)
plt.scatter(waves['WAVE_ID'],waves['Hits'],color='red')
plt.scatter(waves['WAVE_ID'],predict,color='green')
#plt.scatter(waves['WAVE_ID'],error,color='blue')
plt.annotate("average error: " + '%.3f'%(sum(error)/len(error)),(-5,49))
plt.annotate("correct: " + str(len([e for e in error if e<0.5])),(-5,48))
plt.annotate("incorrect: "+str(len([e for e in error if e>=0.5])),(-5,47))
plt.annotate("accuracy: "+'%.3f'%(len([e for e in error if e<0.5])/len(error)),(-5,46))
subModel = [he_a,he_b,he_c,ss_a,ss_b,fr_a,fr_b,fr_c] 
print(['%.3f'%e for e in subModel])
print(['%.3f'%e for e in ans])
'''
error = test['Hits']-predict
absError = np.absolute(test['Hits']-predict)
random_dists = ['Prediction', 'Actual', 'Error', 'Absolute Error']
fig, ax = plt.subplots()
ax.set_xticklabels(random_dists, rotation=45, fontsize=8)
ax.boxplot([predict,test['Hits'],error,absError])
ax.yaxis.grid(True)
ax.annotate("average error: " + '%.3f'%(sum(error)/len(error)),(0.5,48))
ax.annotate("average absolute error: " + '%.3f'%(sum(absError)/len(absError)),(0.5,45))
ax.annotate("correct: " + str(len([e for e in absError if e<0.5])),(0.5,42))
ax.annotate("incorrect: "+str(len([e for e in absError if e>=0.5])),(0.5,39))
ax.annotate("accuracy: "+'%.3f'%(len([e for e in absError if e<0.5])/len(absError)),(0.5,36))
ax.set_ylabel("Hits")
ax.set_title("6 Enemy Wave")
plt.show()