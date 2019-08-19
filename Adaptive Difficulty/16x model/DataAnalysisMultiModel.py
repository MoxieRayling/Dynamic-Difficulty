import numpy as np
from scipy.optimize import curve_fit
import pandas as pd
import matplotlib.pyplot as plt

he = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/health 1.csv")
ss = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/shotspeed 1.csv")
fr = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/firerate 1.csv")
waves = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/waves 1.csv")

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

def model1(X, a,b,c,d,e):
    he,ss,fr = X
    return (linModel(he,he_a,he_b) * b*logModel(ss*100,ss_a,ss_b,ss_c) * c*rexpModel(fr,fr_a,fr_b,fr_c))*d + e

init_guess = [1,1,1,1,1]  
fit = curve_fit(model1, 
(train['Health1'], train['ShotSpeed1'], train['FireRate1']), 
train['Hits'], 
p0=init_guess, 
absolute_sigma=True)
ans,cov = fit
fit_a, fit_b, fit_c, fit_d, fit_e = ans
print(ans)
predict = model1((test['Health1'],test['ShotSpeed1'],test['FireRate1']),fit_a,fit_b,fit_c,fit_d,fit_e)

'''
def model(X, a,b,c,d,e):
    he,ss,fr,Fit = X
    he_a,he_b, ss_a,ss_b,ss_c,fr_a,fr_b,fr_c = Fit
    return (linModel(he,he_a,he_b) * b*logModel(ss*100,ss_a,ss_b,ss_c) * c*rexpModel(fr,fr_a,fr_b,fr_c))*d + e
def remodel1(Data, n):
    hData, hHits, sData, sHits, fData, fHits, waves = Data
    subfit = [0.088, 0.129, 1.325, 0.441, 0.067, 0.011, 3.579, -0.443]
    fit = [1.000, 1.344, 0.516, 0.656,  - 0.037]
    
    hData +=[5,15,25,35,45]
    hHits += [linModel(5,subfit[0],subfit[1]),linModel(15,subfit[0],subfit[1]),linModel(25,subfit[0],subfit[1]),linModel(35,subfit[0],subfit[1]),linModel(45,subfit[0],subfit[1])]
    sData += [10,20,30,40,50]
    sHits += [logModel(10,subfit[2],subfit[3],subfit[4]),logModel(20,subfit[2],subfit[3],subfit[4]),logModel(30,subfit[2],subfit[3],subfit[4]),logModel(40,subfit[2],subfit[3],subfit[4]),logModel(50,subfit[2],subfit[3],subfit[4])]
    fData += [10,34,58,82,106]
    fHits += [rexpModel(10,subfit[5],subfit[6],subfit[7]),rexpModel(34,subfit[5],subfit[6],subfit[7]),rexpModel(58,subfit[5],subfit[6],subfit[7]),rexpModel(82,subfit[5],subfit[6],subfit[7]),rexpModel(106,subfit[5],subfit[6],subfit[7])]
    
    fit = curve_fit(logModel, sData[:n], sHits[:n], p0=[1,1,1], absolute_sigma=True)
    ans,cov = fit
    ss_a, ss_b, ss_c = ans
    fit = curve_fit(rexpModel, fData[:n], fHits[:n], p0=[1,1,1], absolute_sigma=True)
    ans,cov = fit
    fr_a, fr_b, fr_c = ans
    Fit = subfit[0],subfit[1],ss_a,ss_b,ss_c,fr_a,fr_b,fr_c
    init_guess = [1,1,1,1,1]  
    fit = curve_fit(model1, (hData[:n], sData[:n], fData[:n],Fit),waves[:n], p0=init_guess, absolute_sigma=True)
    ans,cov = fit
    fit_a, fit_b, fit_c, fit_d, fit_e = ans
    predict = model1((hData[n:],sData[n:],fData[n:],Fit),fit_a,fit_b,fit_c,fit_d,fit_e)
    print(predict)
    return predict

'''
'''
error = np.absolute(waves['Hits']-predict)
#plt.fill_between(waves['WAVE_ID'],0.5,-0.5)
plt.scatter(waves['WAVE_ID'],waves['Hits'],color='red')
plt.scatter(waves['WAVE_ID'],predict,color='green')
#plt.scatter(waves['WAVE_ID'],error,color='blue')
plt.annotate("average error: " + '%.3f'%(sum(error)/len(error)),(-5,9))
plt.annotate("correct: " + str(len([e for e in error if e<0.5])),(-5,8.5))
plt.annotate("incorrect: "+str(len([e for e in error if e>=0.5])),(-5,8))
plt.annotate("accuracy: "+'%.3f'%(len([e for e in error if e<0.5])/len(error)),(-5,7.5))

subModel = [he_a,he_b,ss_a,ss_b,ss_c,fr_a,fr_b,fr_c] 
print(['%.3f'%e for e in subModel])
print(['%.3f'%e for e in ans])
'''

meanpointprops = dict(marker='o', markeredgecolor='black', markerfacecolor='red')
error = test['Hits']-predict
absError = np.absolute(test['Hits']-predict)
xaxis = ['Prediction', 'Hits', 'Error', 'Absolute Error']
fig, ax = plt.subplots()
ax.set_xticklabels(xaxis, rotation=45, fontsize=8)
ax.boxplot([predict,test['Hits'],error,absError], meanprops=meanpointprops, showmeans=True)
ax.yaxis.grid(True)
ax.annotate("average error: " + '%.3f'%(sum(error)/len(error)),(0.5,9.5))
ax.annotate("average absolute error: " + '%.3f'%(sum(absError)/len(absError)),(0.5,9))
ax.annotate("correct: " + str(len([e for e in absError if e<0.5])),(0.5,8.5))
ax.annotate("incorrect: "+str(len([e for e in absError if e>=0.5])),(0.5,8))
ax.annotate("accuracy: "+'%.3f'%(len([e for e in absError if e<0.5])/len(absError)),(0.5,7.5))
ax.set_ylabel("Hits per wave")
ax.set_title("1 Enemy Wave")

plt.show()