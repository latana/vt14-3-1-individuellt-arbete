using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WonderfulGames
{
    /// <summary>
    /// Hanterar en tempurär session som i detta fallet innehåller meddelanden
    /// när användaren har gjort någonting rätt.
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// Retunerar ut värdet ifrån sessionen och förstör den.
        /// </summary>
        /// <param name="page">Innehåller ett object ut av code-behinden som ärver ifrån Page</param>
        /// <param name="key">Sessionens namn</param>
        /// <returns>felmeddelandet som skall presenteras</returns>
        public static object GetTempData(this Page page, string key)
        {
            var value = page.Session[key];
            page.Session.Remove(key);
            return value;
        }

        /// <summary>
        /// Sätter sessionens namn och innehåll.
        /// </summary>
        /// <param name="page">Innehåller ett object ut av code-behinden som ärver ifrån Page</param>
        /// <param name="key">nyckeln. Alltså sessionens namn</param>
        /// <param name="value">Värdet. Alltså innehållet i sessionen. I
        /// detta fallet ett meddelande.</param>
        public static void SetTempData(this Page page, string key, object value)
        {
            page.Session[key] = value;
        }
    }
}