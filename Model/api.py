# Dependencies
import pickle
import sys
from flask import Flask, request, jsonify
import joblib
import traceback
import pandas as pd
import numpy as np
import xgboost


# Your API definition
app = Flask(__name__)

expected_features = [
    'minimalnaTemperaturaPrzyGruncie',
    'róznicaMaksymalnejIMinimalnejTemperatury',
    'średniaTemperaturaDobowa',
    'średniaDobowaWilgotnośćWzględna',
    'średniaDobowaPrędkośćWiatru',
    'średnieDoboweZachmurzenieOgólne',
    'średniaDoboweCiśnienieNaPoziomieStacji',
    'sumaOpaduDzień',
    'location',
    'dzieńRoku',
    'dzieńTygodnia',
    'value',
    'valueCat'
]

@app.route('/AirPollutionModel/PredictPM10', methods=['POST'])
def predictPM10():
    if lrPM10:
        try:
            json_ = request.json
            print("Received JSON:")
            print(json_)
            
            query = pd.DataFrame([json_])
            print("DataFrame from JSON:")
            print(query)
            
            query = pd.get_dummies(query)
            print("DataFrame after get_dummies:")
            print(query)

            # Ensure all expected features are present
            for feature in expected_features:
                if feature not in query.columns:
                    query[feature] = 0
            
            print("DataFrame after adding missing features:")
            print(query)
            
            # Direct prediction
            try:
                prediction = lrPM10.predict(query)
                print("Prediction results:")
                print(prediction)
                
                # Ensure prediction is a list
                prediction_list = prediction.tolist()
                print("Prediction as list:")
                print(prediction_list)
                
                return jsonify({'prediction': str(prediction[0])})
            except Exception as e:
                print("Error during prediction execution:")
                print(e)
                print(traceback.format_exc())
                return jsonify({'trace': str(e)})

        except Exception as e:
            print("Error during data processing:")
            print(traceback.format_exc())
            return jsonify({'trace': traceback.format_exc()})
    else:
        print('Train the model first')
        return ('No model here to use')


    

@app.route('/AirPollutionModel/PredictPM2.5', methods=['POST'])
def predictPM25():
    if lrPM25:
        try:
            json_ = request.json
            print("Received JSON:")
            print(json_)
            
            query = pd.DataFrame([json_])
            print("DataFrame from JSON:")
            print(query)
            
            query = pd.get_dummies(query)
            print("DataFrame after get_dummies:")
            print(query)

            # Ensure all expected features are present
            for feature in expected_features:
                if feature not in query.columns:
                    query[feature] = 0
            
            print("DataFrame after adding missing features:")
            print(query)
            
            # Direct prediction
            try:
                prediction = lrPM25.predict(query)
                print("Prediction results:")
                print(prediction)
                
                # Ensure prediction is a list
                prediction_list = prediction.tolist()
                print("Prediction as list:")
                print(prediction_list)
                
                return jsonify({'prediction': str(prediction[0])})
            except Exception as e:
                print("Error during prediction execution:")
                print(e)
                print(traceback.format_exc())
                return jsonify({'trace': str(e)})

        except Exception as e:
            print("Error during data processing:")
            print(traceback.format_exc())
            return jsonify({'trace': traceback.format_exc()})
    else:
        print('Train the model first')
        return ('No model here to use')
    
@app.route('/AirPollutionModel/PredictO3', methods=['POST'])
def predictO3():
    if lrO3:
        try:
            json_ = request.json
            print("Received JSON:")
            print(json_)
            
            query = pd.DataFrame([json_])
            print("DataFrame from JSON:")
            print(query)
            
            query = pd.get_dummies(query)
            print("DataFrame after get_dummies:")
            print(query)

            # Ensure all expected features are present
            for feature in expected_features:
                if feature not in query.columns:
                    query[feature] = 0
            
            print("DataFrame after adding missing features:")
            print(query)
            
            # Direct prediction
            try:
                prediction = lrO3.predict(query)
                print("Prediction results:")
                print(prediction)
                
                # Ensure prediction is a list
                prediction_list = prediction.tolist()
                print("Prediction as list:")
                print(prediction_list)
                
                return jsonify({'prediction': str(prediction[0])})
            except Exception as e:
                print("Error during prediction execution:")
                print(e)
                print(traceback.format_exc())
                return jsonify({'trace': str(e)})

        except Exception as e:
            print("Error during data processing:")
            print(traceback.format_exc())
            return jsonify({'trace': traceback.format_exc()})
    else:
        print('Train the model first')
        return ('No model here to use')
    
@app.route('/AirPollutionModel/PredictNO2', methods=['POST'])
def predictNO2():
    if lrNO2:
        try:
            json_ = request.json
            print("Received JSON:")
            print(json_)
            
            query = pd.DataFrame([json_])
            print("DataFrame from JSON:")
            print(query)
            
            query = pd.get_dummies(query)
            print("DataFrame after get_dummies:")
            print(query)

            # Ensure all expected features are present
            for feature in expected_features:
                if feature not in query.columns:
                    query[feature] = 0
            
            print("DataFrame after adding missing features:")
            print(query)
            
            # Direct prediction
            try:
                prediction = lrNO2.predict(query)
                print("Prediction results:")
                print(prediction)
                
                # Ensure prediction is a list
                prediction_list = prediction.tolist()
                print("Prediction as list:")
                print(prediction_list)
                
                return jsonify({'prediction': str(prediction[0])})
            except Exception as e:
                print("Error during prediction execution:")
                print(e)
                print(traceback.format_exc())
                return jsonify({'trace': str(e)})

        except Exception as e:
            print("Error during data processing:")
            print(traceback.format_exc())
            return jsonify({'trace': traceback.format_exc()})
    else:
        print('Train the model first')
        return ('No model here to use')

@app.route('/AirPollutionModel/PredictSO2', methods=['POST'])
def predictSO2():
    if lrSO2:
        try:
            json_ = request.json
            print("Received JSON:")
            print(json_)
            
            query = pd.DataFrame([json_])
            print("DataFrame from JSON:")
            print(query)
            
            query = pd.get_dummies(query)
            print("DataFrame after get_dummies:")
            print(query)

            # Ensure all expected features are present
            for feature in expected_features:
                if feature not in query.columns:
                    query[feature] = 0
            
            print("DataFrame after adding missing features:")
            print(query)
            
            # Direct prediction
            try:
                prediction = lrSO2.predict(query)
                print("Prediction results:")
                print(prediction)
                
                # Ensure prediction is a list
                prediction_list = prediction.tolist()
                print("Prediction as list:")
                print(prediction_list)
                
                return jsonify({'prediction': str(prediction[0])})
            except Exception as e:
                print("Error during prediction execution:")
                print(e)
                print(traceback.format_exc())
                return jsonify({'trace': str(e)})

        except Exception as e:
            print("Error during data processing:")
            print(traceback.format_exc())
            return jsonify({'trace': traceback.format_exc()})
    else:
        print('Train the model first')
        return ('No model here to use')


if __name__ == '__main__':
    try:
        port = int(sys.argv[1]) # This is for a command-line input
    except:
        port = 12345 # If you don't provide any port the port will be set to 12345

    # Load models
    try:
        lrPM10 = pickle.load(open('C:/Users/Franczi/Downloads/modelXGB(PM10)_NAJLEPSZY.sav', "rb"))
        print('PM10 model loaded successfully')
    except Exception as e:
        print('Failed to load PM10 model:')
        print(e)

    try:
        lrPM25 = pickle.load(open('C:/Users/Franczi/Downloads/modelXGB(pm25)(1).sav', "rb"))
        print('PM25 model loaded successfully')
    except Exception as e:
        print('Failed to load PM25 model:')
        print(e)

    try:
        lrO3 = pickle.load(open('C:/Users/Franczi/Downloads/modelXGB(O3)(1).sav', "rb"))
        print('O3 model loaded successfully')
    except Exception as e:
        print('Failed to load O3 model:')
        print(e)

    try:
        lrSO2 = pickle.load(open('C:/Users/Franczi/Downloads/modelXGB(SO2).sav', "rb"))
        print('SO2 model loaded successfully')
    except Exception as e:
        print('Failed to load SO2 model:')
        print(e)

    try:
        lrNO2 = pickle.load(open('C:/Users/Franczi/Downloads/modelXGB(NO2)(1).sav', "rb"))
        print('NO2 model loaded successfully')
    except Exception as e:
        print('Failed to load NO2 model:')
        print(e)

    print('Models loaded')

    app.run(port=port, debug=True)