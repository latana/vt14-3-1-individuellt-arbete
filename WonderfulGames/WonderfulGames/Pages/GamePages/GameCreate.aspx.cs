using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WonderfulGames.App_Infrastructure;
using WonderfulGames.Model;

namespace WonderfulGames.Pages.GamePages
{
    /// <summary>
    /// Första kontrollen på datan vi får in och tar därefter 
    /// kontakt med service för att vidarbehandla datan.
    /// </summary>
    public partial class GameCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// kallar på GetGenreType i service klassen och får tillbaka
        /// all data ifrån Genre till dropdownlistan.
        /// </summary>
        /// <returns>retunerar all id och namn på genrena</returns>
        public IEnumerable<Genre> GenreDropDownList_GetData()
        {
            return GetService.Service.GetGenreType();
        }

        /// <summary>
        /// Kallar på GetDeveloperName i service klassen och får tillbaka
        /// all data ifrån Developer till dropdownlistan.
        /// </summary>
        /// <returns>retunerar all id och namn på utvecklarna</returns>
        public IEnumerable<Developer> DeveloperDropDownList_GetData()
        {
            return GetService.Service.GetDeveloperName();
        }

        /// <summary>
        /// Kollar först så att det inte finns några problem med objektet vi får in.
        /// Kallar på SaveGame metoden i service och skickar iväg uppgifterna
        /// som vi vill spara. Sedan länkar vi till spel-listan så att användaren kan se
        /// att deras spel finns i listan och för att undvika att användaren duplicerar spel
        /// med refresh.
        /// </summary>
        /// <param name="game">Ett objekt med ett spel som så småningom skall sparas i databasen</param>
        public void GameFormView_InsertItem(Game game)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    GetService.Service.SaveGame(game);
                    this.SetTempData("SuccessMessage", String.Format("Spelet '{0}' lades till.", game.Title));
                    Response.RedirectToRoute("GameList");
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
        }
    }
}