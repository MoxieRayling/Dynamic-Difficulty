import numpy as np
from scipy.optimize import curve_fit
import pandas as pd
import matplotlib.pyplot as plt

random = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/test 85.csv")
pred1 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga prediction.csv")
pred2 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga global average.csv")
pred3 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga 5 point average.csv")
pred4 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance.csv")
pred5 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance fitness.csv")
pred6 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga variance fitness 50 50.csv")
pred7 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga static target 1000.csv")
pred8 = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/16x model/ga 5 point average 1000.csv")

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

titles = ['Hits', 'Prediction', 'Variance', 'Fitness']
fig, ax = plt.subplots()
ax.set_xticklabels(titles)
ax.boxplot([pred7['HITS'][:1000],pred7['PREDICTION'][:1000],pred7['VARIANCE'][:1000]*10,pred7['FITNESS'][:1000]*10])
ax.yaxis.grid(True)

'''
titles = ['Hits', 'Prediction', 'Variance', 'Fitness']
fig, ax = plt.subplots()
ax.set_xticklabels(titles)
ax.boxplot([pred8['HITS'][:1000],pred8['PREDICTION'][:1000],pred8['VARIANCE'][:1000]*10,pred8['FITNESS'][:1000]*10])
ax.yaxis.grid(True)
'''
plt.show()