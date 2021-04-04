# imports
import numpy as np
import pandas as pd
from sklearn.preprocessing import LabelEncoder, OneHotEncoder
from sklearn.preprocessing import StandardScaler
import tensorflow as tf
import keras
from keras.models import Sequential
from keras.layers import Dense

df = pd.read_csv('../matches_played.csv')
X = df.iloc[:, 0:21].values
Y = df.iloc[:, 21:23].values

# encode home-team, away-team and form
labelEncoder_X_1 = LabelEncoder()
X[:, 1] = labelEncoder_X_1.fit_transform(X[:, 1])

labelEncoder_X_2 = LabelEncoder()
X[:, 2] = labelEncoder_X_2.fit_transform(X[:, 2])

labelEncoder_X_4 = LabelEncoder()
X[:, 4] = labelEncoder_X_4.fit_transform(X[:, 4])

labelEncoder_X_6 = LabelEncoder()
X[:, 6] = labelEncoder_X_6.fit_transform(X[:, 6])

labelEncoder_X_19 = LabelEncoder()
X[:, 19] = labelEncoder_X_19.fit_transform(X[:, 19])

labelEncoder_X_20 = LabelEncoder()
X[:, 20] = labelEncoder_X_20.fit_transform(X[:, 20])

# mess around
sc = StandardScaler()
X = sc.fit_transform(X)
X = sc.transform(X)

# Initialize
classifier = Sequential()

classifier.add(Dense(units = 8, kernel_initializer = 'uniform', activation = 'relu', input_dim = [None, 21]))
classifier.add(Dense(units = 8, kernel_initializer = 'uniform', activation = 'relu'))
classifier.add(Dense(units = 2, kernel_initializer = 'uniform', activation = 'sigmoid'))

classifier.compile(optimizer = 'adam', loss = 'absolute_difference', metrics = ['accuracy'])

classifier.fit(X, Y, batch_size = 10)




