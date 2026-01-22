using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAirPollution.Model.Validation
{
    /// <summary>
    /// Klasa reprezentująca odpowiedź błędu walidacji zawierającą listę błędów oraz pole z nieprawidłowymi danymi.
    /// </summary>
    public class ValidationErrorResponse
    {
        /// <summary>
        /// Lista przechowująca komunikaty błędów.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Lista przechowująca pola, które zawierają nieprawidłowe dane.
        /// </summary>
        public List<string> InvalidFields { get; set; }
            
        /// <summary>
        /// Metoda służąca do zapisu komunikatów błędów i nieprawidłowych pól do pliku.
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku, do którego mają być zapisane błędy.</param>
        public void LogErrorsToFile(string filePath)
        {
            if (Errors.Count > 0 || InvalidFields.Count > 0)
            {
                try
                {
                    // Otwórz strumień do zapisu wskazanego pliku.
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        // Zapisz komunikaty błędów.
                        writer.WriteLine("Errors:");
                        foreach (string error in Errors)
                        {
                            writer.WriteLine(error);
                        }

                        // Zapisz nieprawidłowe pola.
                        writer.WriteLine("Invalid Fields:");
                        foreach (string invalidField in InvalidFields)
                        {
                            writer.WriteLine(invalidField);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Obsługa ewentualnych błędów podczas zapisu do pliku.
                    Console.WriteLine("Błąd podczas zapisu do pliku: " + ex.Message);
                }
            }
        }
    }

}
