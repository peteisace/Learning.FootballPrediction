# imports
import numpy as np
import pandas as pd
from sklearn.preprocessing import LabelEncoder, OneHotEncoder
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split
import tensorflow as tf
import keras
from keras.models import Sequential
from keras.layers import Dense

# read file
dataset = pd.read_csv('../matches_played.csv')


def format_output(data):
    y1 = data.pop('home_goals_scored')
    y1 = np.array(y1)
    y2 = data.pop('away_goals_scored')
    y2 = np.array(y2)

    return y1, y2


# drop columns
X = dataset.drop(labels = ['season', 'home_teamid', 'away_teamid'], axis = 1)
Y = dataset[['home_goals_scored', 'away_goals_scored']]

print(Y)

# label encoding
label1 = LabelEncoder()
X['played'] = label1.fit_transform(X['played'])

label2 = LabelEncoder()
X['home_team'] = label2.fit_transform(X['home_team'])

label3 = LabelEncoder()
X['away_team'] = label3.fit_transform(X['away_team'])

label4 = LabelEncoder()
X['home_form'] = label4.fit_transform(X['home_form'])

label5 = LabelEncoder()
X['away_form'] = label5.fit_transform(X['away_form'])

# Feature standardization
scaler = StandardScaler()

X_train, X_test, Y_train, Y_test = train_test_split(X, Y, test_size = 0.2, random_state = 0)
X_train = scaler.fit_transform(X_train)
X_test = scaler.transform(X_test)

# Build the model
model = Sequential()
model.add(Dense(X.shape[1], activation = 'relu', input_dim = X.shape[1]))
model.add(Dense(32, activation = 'relu'))
model.add(Dense(2, activation = 'softmax'))

model.compile(optimizer = 'adam', loss = 'mean_absolute_error', metrics = ['accuracy'])
model.fit(X_train, Y_train, batch_size = 10, epochs = 30, verbose = 1)

y_pred = np.argmax(model.predict(X_test), axis = 1)
print(Y_train)
print(y_pred)
