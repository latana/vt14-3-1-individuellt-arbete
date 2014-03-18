using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WonderfulGames
{
    /// <summary>
    /// Går igenom all validering på tabell-klasserna i model
    /// och skickar iväg alla felmeddelandena som dycker upp.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Validerar objektet som "instance" innehåller och skapar en lista av validationResult. 
        /// </summary>
        /// <typeparam name="T">Innehåller själva klassnamnet tex ("Game" game)</typeparam>
        /// <param name="instance">Innehåller själva objektet som skall valideras</param>
        /// <param name="validationResults">En lista med alla felmeddelandena</param>
        /// <returns>retunerar ut alla felmeddelandena som uppstår.</returns>
        public static bool Validate<T>(this T instance, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, validationContext, validationResults, true);
        }
    }
}