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

'''
plt.plot(random['HITS'][:100],label = 'Hits')
plt.plot(np.convolve(random['HITS'][:100],np.ones((10,))/10,mode='full'),label = 'Moving Average')
plt.plot(np.ones((100,))*5,label='Target')

plt.legend(loc='upper left')
plt.xlabel('Waves')
plt.ylabel('Hits')
plt.title("Random Waves")
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
plt.show()