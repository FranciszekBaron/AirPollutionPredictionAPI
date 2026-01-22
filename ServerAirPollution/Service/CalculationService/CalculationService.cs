using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AirPollutionPrediction.Model;
using Newtonsoft.Json;
using ServerAirPollution.Model;
using ServerAirPollution.Model.DTO;
using ServerAirPollution.Service.ValidationService;

namespace ServerAirPollution.Service.CalculationService
{
    /// <summary>
    /// Klasa reprezentująca serwis do obliczeń i przetwarzania danych dotyczących zanieczyszczeń i prognoz.
    /// </summary>
    public class CalculationService : ICalculationService
    {
        /// <summary>
        /// Metoda wypełniająca dane dotyczące zanieczyszczeń i ich prognoz na podstawie dostarczonych danych wejściowych.
        /// </summary>
        /// <param name="inputData">Dane wejściowe zawierające informacje o stacji pomiarowej i prognozach.</param>
        /// <param name="record">Rekord pomiarowy zawierający informacje o zanieczyszczeniach.</param>
        /// <param name="toRequestPrediction">Dane do prognozy przekazywane do modelu prognozującego.</param>
        /// <param name="pollution_Type">Rodzaj zanieczyszczenia.</param>
        /// <returns>Obiekt zawierający dane zanieczyszczeń i prognoz.</returns>
        public async Task<OutputData> fillPollutionValues(InputData inputData, Record record, PredictionData toRequestPrediction, string pollution_Type)
        {
            // Tworzenie obiektu wyjściowego z danymi zanieczyszczeń i prognozami.
            OutputData outputData = new OutputData
            {
                Station_Name = record.Station_Name,
                Station_Timestamp = record.Station_Timestamp,
                Values = new List<Values>()
            };

            Values values = new Values();

            // Wybór odpowiednich wartości w zależności od rodzaju zanieczyszczenia.
            switch (pollution_Type)
            {
                case "PM10":
                    values.pollution_Type = pollution_Type;
                    values.Value = record.ValuePm10;
                    values.ValueCat = record.ValueCatPm10;
                    toRequestPrediction.ValueCat = findMapping(record.ValueCatPm10);
                    values.ValueForNextDay = reverseMapping(await makePrediction(toRequestPrediction, pollution_Type));
                    break;
                case "PM2.5":
                    values.pollution_Type = pollution_Type;
                    values.Value = record.ValuePm25;
                    values.ValueCat = record.ValueCatPm25;
                    toRequestPrediction.ValueCat = findMapping(record.ValueCatPm25);
                    values.ValueForNextDay = reverseMapping(await makePrediction(toRequestPrediction, pollution_Type));
                    break;
                case "SO2":
                    values.pollution_Type = pollution_Type;
                    values.Value = record.ValueSo2;
                    values.ValueCat = record.ValueCatSo2;
                    toRequestPrediction.ValueCat = findMapping(record.ValueCatSo2);
                    values.ValueForNextDay = reverseMapping(await makePrediction(toRequestPrediction, pollution_Type));
                    break;
                case "O3":
                    values.pollution_Type = pollution_Type;
                    values.Value = record.ValueO3;
                    values.ValueCat = record.ValueCatO3;
                    toRequestPrediction.ValueCat = findMapping(record.ValueCatO3);
                    values.ValueForNextDay = reverseMapping(await makePrediction(toRequestPrediction, pollution_Type));
                    break;
                case "NO2":
                    values.pollution_Type = pollution_Type;
                    values.Value = record.ValueNo2;
                    values.ValueCat = record.ValueCatNo2;
                    toRequestPrediction.ValueCat = findMapping(record.ValueCatNo2);
                    values.ValueForNextDay = reverseMapping(await makePrediction(toRequestPrediction, pollution_Type));
                    break;
            }
                    
            // Dodawanie wartości do listy i przypisanie jej do obiektu wyjściowego.
            outputData.Values = new List<Values>{values};
                    
            // Zwracanie obiektu wyjściowego z danymi zanieczyszczeń i prognozami.
            return outputData;
        }


                    
                




        /// <summary>
        /// Metoda przypisująca wartości kategorii zanieczyszczeń do odpowiadających im liczb całkowitych.
        /// </summary>
        /// <param name="value">Wartość kategorii zanieczyszczenia.</param>
        /// <returns>Liczba całkowita odpowiadająca wartości kategorii.</returns>
        private static int findMapping(string value)
        {
            // Słownik mapujący wartości kategorii na odpowiadające im liczby całkowite.
            // Słownik Ma na celu ułatwienie komunikacji pomiędzy danymi jakie musza zostać przekazane do modelu sztucznej inteligencji a tymi, które dostajemy 
            Dictionary<string, int> mapping = new Dictionary<string, int>
            {
                { "Bardzo dobry", 0 },
                { "Bardzo zły", 1 },
                { "Dobry", 2 },
                { "Dostateczny", 3 },
                { "Umiarkowany", 4 },
                { "Zły", 5 },
                { "null", -1 },
            };

            // Sprawdzanie, czy wartość istnieje w słowniku, i zwracanie przypisanej jej liczby całkowitej.
            if (mapping.TryGetValue(value, out int result))
            {
                return result;
            }
            else
            {
                // Rzucanie wyjątku w przypadku nieprawidłowej wartości.
                throw new ArgumentException("Nieprawidłowa wartość stringa.");
            }
        }



    /// <summary>
    /// Metoda odwracająca mapowanie wartości liczbowych na odpowiadające im kategorie zanieczyszczeń.
    /// </summary>
    /// <param name="value">Wartość liczba całkowita.</param>
    /// <returns>Kategoria zanieczyszczenia odpowiadająca wartości liczbowej.</returns>
    private static string reverseMapping(int value)
    {   
        // Słownik mapujący wartości liczbowe na odpowiadające im kategorie zanieczyszczeń.
        Dictionary<int, string> reverseMapping = new Dictionary<int, string>
        {
            { 0, "Bardzo dobry" },
            { 1, "Bardzo zły" },
            { 2, "Dobry" },
            { 3, "Dostateczny" },
            { 4, "Umiarkowany" },
            { 5, "Zły" },
            {-1, "Prognoza niedostępna" }
        };

        // Sprawdzanie, czy wartość istnieje w słowniku, i zwracanie przypisanej jej kategorii.
        if (reverseMapping.TryGetValue(value, out string result))
        {
            return result;
        }
        else
        {
            // Rzucanie wyjątku w przypadku nieprawidłowej wartości.
            throw new ArgumentException("Nieprawidłowa wartość liczby całkowitej.");
        }
    }



       
        
    /// Wykonuje prognozę przy użyciu modelu AirPollutionModel dla określonego rodzaju zanieczyszczenia.
    /// <param name="predictionData">Dane wejściowe prognozy.</param>
    /// <param name="pollution_Type">Rodzaj zanieczyszczenia do przewidzenia.</param>
    /// <returns>Przewidywana wartość lub -1, jeśli prognoza jest niedostępna.</returns>
    public async Task<int> makePrediction(PredictionData predictionData, string pollution_Type)
    {
        // Tworzenie klienta HTTP
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://127.0.0.1:12345/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        
        // Wysłanie żądania POST do punktu końcowego prognozy jest nim "mini API" stworzone w jezyku flask
        HttpResponseMessage response = await client.PostAsJsonAsync($"AirPollutionModel/Predict{pollution_Type}", predictionData);
        response.EnsureSuccessStatusCode();

        // Odczytanie wyniku prognozy z odpowiedzi
        var x = await response.Content.ReadFromJsonAsync<Prediction>();

        // Zwrócenie przewidywanej wartości, jeśli jest dostępna, w przeciwnym razie -1
        if (x is not null && predictionData.ValueCat != -1)
            return x.prediction;

        return -1;
    }



        

    
    }
}