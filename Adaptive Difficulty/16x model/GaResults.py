import numpy as np
from scipy.optimize import curve_fit
import pandas as pd
import matplotlib.pyplot as plt
import matplotlib.ticker as plticker

random = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/test 85.csv")
pred1 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga prediction.csv")
pred2 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga global average.csv")
pred3 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga 5 point average.csv")
pred4 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance.csv")
pred5 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance fitness.csv")
pred6 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance fitness 50 50.csv")
pred7 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga static target 1000.csv")
pred8 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga 5 point average 1000.csv")
targets = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/target testing.csv")
av2 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga 2 average 8 target.csv")
av5 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga 5 average 8 target.csv")
av7 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga 7 average 8 target.csv")
av10 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga 10 average 8 target.csv")
variance2 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance 20 1000 v2.csv")
variance5 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance 50 1000 v2.csv")
variance8 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance 80 1000 v2.csv")
variance0 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga static target 1000 v2.csv")
variance10 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance 100 1000 v2.csv")
iframes50 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/50 iframes 5 average.csv")
iframes50_static = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/50 iframes no average.csv")
iframes25 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/25 iframes 5 average.csv")
iframes25_static = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/25 iframes no average.csv")
iframes1 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/1 iframe 5 average.csv")
iframes1_static = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/1 iframe no average.csv")
steering1 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/steering 1 5 average.csv")
steering1_static = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/steering 1 no average.csv")
steering005 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/steering 0.05 5 average.csv")
steering005_static = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/steering 0.05 no average.csv")

meanpointprops = dict(marker='o', markeredgecolor='black', markerfacecolor='red')
'''
plt.plot(random['HITS'][:100],label = 'Hits')
plt.plot(np.convolve(random['HITS'][:100],np.ones((10,))/10,mode='full'),label = 'Moving Average')
plt.plot(np.ones((100,))*5,label='Target')

plt.legend(loc='upper left')
plt.xlabel('Waves')
plt.ylabel('Hits')
plt.title("Random Waves")
plt.annotate("average error: " + '%.3f'%(np.average(random['HITS'][:100]-5)),(50,19))
'''
'''
plt.plot(pred1['HITS'][:100],label = 'Hits')
plt.plot(np.convolve(pred1['HITS'][:100],np.ones((10,))/10),label='Moving Average')
plt.plot(np.ones((100,))*5,label='Target')
plt.plot(pred1['PREDICTION'][:100],label='Prediction')

plt.legend(loc='upper left')
plt.xlabel('Waves')
plt.ylabel('Hits')
plt.title("GA Prediction: No Average")
plt.annotate("average error: " + '%.3f'%(np.average(pred1['HITS'][:100]-5)),(50,19))
'''
'''
plt.plot(pred2['HITS'][:100],label = 'Hits')
plt.plot(np.convolve(pred2['HITS'][:100],np.ones((10,))/10),label='Moving Average')
plt.plot(pred2['PREDICTION'][:100],label='Prediction')
plt.plot(np.ones((100,))*5,label='Target')

plt.legend(loc='upper left')
plt.xlabel('Waves')
plt.ylabel('Hits')
plt.title("GA Prediction: Run-Length Average")
plt.annotate("average error: " + '%.3f'%(np.average(pred2['HITS'][:100]-5)),(50,19))
'''
'''
plt.plot(pred3['HITS'][:100],label = 'Hits')
plt.plot(np.convolve(pred3['HITS'][:100],np.ones((10,))/10),label='Moving Average')
plt.plot(pred3['PREDICTION'][:100],label='Prediction')
plt.plot(np.ones((100,))*5,label='Target')

plt.legend(loc='upper left')
plt.xlabel('Waves')
plt.ylabel('Hits')
plt.title("GA Prediction: 5 Point Average")
plt.annotate("average error: " + '%.3f'%(np.average(pred3['HITS'][:100]-5)),(50,19))
'''
'''
plt.plot(pred4['HITS'][:100],label = 'Hits')
plt.plot(np.convolve(pred4['HITS'][:100],np.ones((10,))/10),label='Moving Average')
plt.plot(pred4['PREDICTION'][:100],label='Prediction')
plt.plot(pred4['VARIANCE'][:100]*10,label='Variance')
plt.plot(np.ones((100,))*5,label='Target')

plt.legend(loc='upper left')
plt.xlabel('Waves')
plt.ylabel('Hits')
plt.title("GA Prediction: Variance")
plt.annotate("average variance: " + '%.3f'%(np.average(pred4['VARIANCE'][:100])),(50,18))
plt.annotate("average error: " + '%.3f'%(np.average(pred4['HITS'][:100]-5)),(50,19))
'''
'''
plt.plot(pred5['HITS'][:100],label = 'Hits')
plt.plot(np.convolve(pred5['HITS'][:100],np.ones((10,))/10),label='Moving Average')
plt.plot(pred5['PREDICTION'][:100],label='Prediction')
plt.plot(pred5['VARIANCE'][:100]*10,label='Variance')
plt.plot(np.ones((100,))*5,label='Target')
plt.legend(loc='upper left')
plt.xlabel('Waves')
plt.ylabel('Hits')
plt.title("GA Prediction: Variance")
plt.annotate("average variance: " + '%.3f'%(np.average(pred5['VARIANCE'][:100])),(50,18))
plt.annotate("average error: " + '%.3f'%(np.average(pred5['HITS'][:100]-5)),(50,19))
'''
'''
plt.plot(pred6['HITS'][:100],label = 'Hits')
plt.plot(np.convolve(pred6['HITS'][:100],np.ones((10,))/10),label='Moving Average')
plt.plot(pred6['PREDICTION'][:100],label='Prediction')
plt.plot(pred6['VARIANCE'][:100]*10,label='Variance')
plt.plot(np.ones((100,))*5,label='Target')

plt.legend(loc='upper left')
plt.xlabel('Waves')
plt.ylabel('Hits')
plt.title("GA Prediction: Variance")
plt.annotate("average variance: " + '%.3f'%(np.average(pred6['VARIANCE'][:100])),(50,18))
plt.annotate("average error: " + '%.3f'%(np.average(pred6['HITS'][:100]-5)),(50,19))
'''
'''
titles = ['Hits', 'Prediction', 'Variance', 'Fitness']
fig, ax = plt.subplots()
ax.set_xticklabels(titles)
ax.boxplot([pred7['HITS'][:1000],pred7['PREDICTION'][:1000],pred7['VARIANCE'][:1000]*10,pred7['FITNESS'][:1000]*10])
ax.yaxis.grid(True)
'''
'''
titles = ['Hits', 'Prediction', 'Variance', 'Fitness']
fig, ax = plt.subplots()
ax.set_xticklabels(titles)
ax.boxplot([pred8['HITS'][:1000],pred8['PREDICTION'][:1000],pred8['VARIANCE'][:1000]*10,pred8['FITNESS'][:1000]*10])
ax.yaxis.grid(True)
'''
'''
fig, ax = plt.subplots()
sqrt = [1,1]+[np.sqrt(i) for i in range(1,37)]
plt.fill_between(range(0,38),sqrt,[-i for i in sqrt],color='#00ff00')
ax.boxplot([
targets.query('TARGET == 0')['HITS'][:100]-targets.query('TARGET == 0')['PREDICTION'][:100],
targets.query('TARGET == 1')['HITS'][:100]-targets.query('TARGET == 1')['PREDICTION'][:100],
targets.query('TARGET == 2')['HITS'][:100]-targets.query('TARGET == 2')['PREDICTION'][:100],
targets.query('TARGET == 3')['HITS'][:100]-targets.query('TARGET == 3')['PREDICTION'][:100],
targets.query('TARGET == 4')['HITS'][:100]-targets.query('TARGET == 4')['PREDICTION'][:100],
targets.query('TARGET == 5')['HITS'][:100]-targets.query('TARGET == 5')['PREDICTION'][:100],
targets.query('TARGET == 6')['HITS'][:100]-targets.query('TARGET == 6')['PREDICTION'][:100],
targets.query('TARGET == 7')['HITS'][:100]-targets.query('TARGET == 7')['PREDICTION'][:100],
targets.query('TARGET == 8')['HITS'][:100]-targets.query('TARGET == 8')['PREDICTION'][:100],
targets.query('TARGET == 9')['HITS'][:100]-targets.query('TARGET == 9')['PREDICTION'][:100],
targets.query('TARGET == 10')['HITS'][:100]-targets.query('TARGET == 10')['PREDICTION'][:100],
targets.query('TARGET == 11')['HITS'][:100]-targets.query('TARGET == 11')['PREDICTION'][:100],
targets.query('TARGET == 12')['HITS'][:100]-targets.query('TARGET == 12')['PREDICTION'][:100],
targets.query('TARGET == 13')['HITS'][:100]-targets.query('TARGET == 13')['PREDICTION'][:100],
targets.query('TARGET == 14')['HITS'][:100]-targets.query('TARGET == 14')['PREDICTION'][:100],
targets.query('TARGET == 15')['HITS'][:100]-targets.query('TARGET == 15')['PREDICTION'][:100],
targets.query('TARGET == 16')['HITS'][:100]-targets.query('TARGET == 16')['PREDICTION'][:100],
targets.query('TARGET == 17')['HITS'][:100]-targets.query('TARGET == 17')['PREDICTION'][:100],
targets.query('TARGET == 18')['HITS'][:100]-targets.query('TARGET == 18')['PREDICTION'][:100],
targets.query('TARGET == 19')['HITS'][:100]-targets.query('TARGET == 19')['PREDICTION'][:100],
targets.query('TARGET == 20')['HITS'][:100]-targets.query('TARGET == 20')['PREDICTION'][:100],
targets.query('TARGET == 21')['HITS'][:100]-targets.query('TARGET == 21')['PREDICTION'][:100],
targets.query('TARGET == 22')['HITS'][:100]-targets.query('TARGET == 22')['PREDICTION'][:100],
targets.query('TARGET == 23')['HITS'][:100]-targets.query('TARGET == 23')['PREDICTION'][:100],
targets.query('TARGET == 24')['HITS'][:100]-targets.query('TARGET == 24')['PREDICTION'][:100],
targets.query('TARGET == 25')['HITS'][:100]-targets.query('TARGET == 25')['PREDICTION'][:100],
targets.query('TARGET == 26')['HITS'][:100]-targets.query('TARGET == 26')['PREDICTION'][:100],
targets.query('TARGET == 27')['HITS'][:100]-targets.query('TARGET == 27')['PREDICTION'][:100],
targets.query('TARGET == 28')['HITS'][:100]-targets.query('TARGET == 28')['PREDICTION'][:100],
targets.query('TARGET == 29')['HITS'][:100]-targets.query('TARGET == 29')['PREDICTION'][:100],
targets.query('TARGET == 30')['HITS'][:100]-targets.query('TARGET == 30')['PREDICTION'][:100],
targets.query('TARGET == 31')['HITS'][:100]-targets.query('TARGET == 31')['PREDICTION'][:100],
targets.query('TARGET == 32')['HITS'][:100]-targets.query('TARGET == 32')['PREDICTION'][:100],
targets.query('TARGET == 33')['HITS'][:100]-targets.query('TARGET == 33')['PREDICTION'][:100],
targets.query('TARGET == 34')['HITS'][:100]-targets.query('TARGET == 34')['PREDICTION'][:100],
targets.query('TARGET == 35')['HITS'][:100]-targets.query('TARGET == 35')['PREDICTION'][:100]
],  meanprops=meanpointprops, showmeans=True)
plt.xlabel('Predicted Hits')
plt.ylabel('Error (predition - hits)')
ax.yaxis.set_major_locator(plticker.MultipleLocator(base=5))
ax.grid(which='major',axis = 'y',linestyle='-')
ax.set_xticklabels(range(0,36))
'''
'''
titles = ['Average 2 Hits', 'Average 2 Prediction','Average 5 Hits', 'Average 5 Prediction','Average 7 Hits', 'Average 7 Prediction','Average 10 Hits', 'Average 10 Prediction']
fig, ax = plt.subplots()
plt.fill_between(range(0,10),8.1,7.9,color='#00ff00')
ax.boxplot([av2['HITS'],av2['PREDICTION'],av5['HITS'],av5['PREDICTION'],av7['HITS'],av7['PREDICTION'],av10['HITS'],av10['PREDICTION']],  meanprops=meanpointprops, showmeans=True)
ax.set_xticklabels(titles, rotation=45, fontsize=8)
'''
'''
titles = ['0% Variance Hits', '20% Variance Hits','50% Variance Hits','80% Variance Hits', '100% Variance Hits']
fig, ax = plt.subplots()
plt.fill_between(range(0,7),8.1,7.9,color='#00ff00')
ax.boxplot([variance0['HITS'],variance2['HITS'],variance5['HITS'],variance8['HITS'],variance10['HITS']],  meanprops=meanpointprops, showmeans=True)
ax.set_xticklabels(titles, rotation=45, fontsize=8)
plt.ylabel('Hits per Wave')
'''
'''
titles = ['0% Variance', '20% Variance','50% Variance','80% Variance', '100% Variance']
fig, ax = plt.subplots()
ax.boxplot([variance0['VARIANCE'],variance2['VARIANCE'],variance5['VARIANCE'],variance8['VARIANCE'],variance10['VARIANCE']],  meanprops=meanpointprops, showmeans=True)
ax.set_xticklabels(titles, rotation=45, fontsize=8)
plt.ylabel('Vairance per Wave (compared to previous 5 waves)')
'''

titles = ['100 iframes Hits', '100 iframes Prediction','50 iframes static target','50 iframes Hits', '50 iframes Prediction','25 iframes static target','25 iframes Hits', '25 iframes Prediction','1 iframe static target','1 iframe Hits', '1 iframe Prediction']
fig, ax = plt.subplots()
plt.fill_between(range(0,13),8.1,7.9,color='#00ff00')
ax.boxplot([av5['HITS'],av5['PREDICTION'],iframes50_static['HITS'],iframes50['HITS'],iframes50['PREDICTION'],
iframes25_static['HITS'],iframes25['HITS'],iframes25['PREDICTION'],iframes1_static['HITS'],iframes1['HITS'],iframes1['PREDICTION']],  meanprops=meanpointprops, showmeans=True)
ax.set_xticklabels(titles, rotation=45, fontsize=8)
plt.ylabel('Hits per Wave')

'''
titles = ['steering = 0.2', 'steering = 0.2 Prediction','steering = 1 static Hits','steering = 1 Hits','steering = 1 Prediction','steering = 0.05 static Hits','steering = 0.05 Hits', 'steering = 0.05 Prediction']
fig, ax = plt.subplots()
plt.fill_between(range(0,10),8.1,7.9,color='#00ff00')
ax.boxplot([av5['HITS'],av5['PREDICTION'],steering1_static['HITS'],steering1['HITS'],steering1['PREDICTION'],
steering005_static['HITS'],steering005['HITS'],steering005['PREDICTION']], meanprops=meanpointprops, showmeans=True)
ax.set_xticklabels(titles, rotation=45, fontsize=8)
plt.ylabel('Hits per Wave')
'''
plt.show()