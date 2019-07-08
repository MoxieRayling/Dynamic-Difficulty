import tensorflow as tf 
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from tensorflow.keras.callbacks import TensorBoard

NAME = "128x3-relu"

df = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/waves.csv")
hits = df.pop('Hits')
X = np.array(df.values).reshape(-1,19)
y = np.array(hits.values).reshape(-1,1)

test = pd.read_csv("C:/Users/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/test.csv")
test_hits = test.pop('Hits')
X_test = np.array(test.values).reshape(-1,19)
y_test = np.array(test_hits.values).reshape(-1,1)



model = tf.keras.models.Sequential() 
model.add(tf.keras.layers.Dense(128, activation=tf.nn.relu)) 
model.add(tf.keras.layers.Dense(128, activation=tf.nn.relu)) 
model.add(tf.keras.layers.Dense(128, activation=tf.nn.relu)) 
model.add(tf.keras.layers.Dense(55, activation=tf.nn.softmax))  

tensorboard = TensorBoard(log_dir="logs/{}".format(NAME))
model.compile(optimizer='adam', 
              loss='sparse_categorical_crossentropy', 
              metrics=['accuracy'])  

model.fit(X, y, batch_size = 32, epochs=20,
          callbacks=[tensorboard])  

val_loss, val_acc = model.evaluate(X_test, y_test) 
print(val_loss)  
print(val_acc) 