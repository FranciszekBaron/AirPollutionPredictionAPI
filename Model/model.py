
import pickle
import numpy as np
print('I\' workin\'')

def pred(a,b,c,d,e,f,g,h,i,j,k,l,m):

    import pickle
    import numpy as np

    model_xgb = pickle.load(open('C:/Users/fbaron/Downloads/modelXGB(1).sav', "rb"))

    input_data = (a,b,c,d,e,f,g,h,i,j,k,l,m)
    
    # change the input data to a numpy array
    input = np.asarray(input_data)

    # reshape the numpy array as we are predicting for only on instance
    input_data_reshaped = input.reshape(1, -1)
    prediction = model_xgb.predict(input_data_reshaped)
    return prediction[0].item()











  

