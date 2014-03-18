using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
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
    public partial class GameUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Hämtar datan till GenreDropdownlist som kommer att innehålla alla
        /// Genrer
        /// </summary>
        /// <returns>All data från genre tabellen</returns>
        public IEnumerable<Genre> GenreDropDownList_GetData()
        {
            return GetService.Service.GetGenreType();
        }

        /// <summary>
        /// Hämtar datan till DeveloperDropdownlist som kommer att innehålla alla
        /// utvecklare
        /// </summary>
        /// <returns>All data från Developer tabellen</returns>
        public IEnumerable<Developer> DeveloperDropDownList_GetData()
        {
            return GetService.Service.GetDeveloperName();
        }

        /// <summary>
        /// Hämtar det specifika spel id't och kontrollerar om det finns i databasen.
        /// Om det visar sig att det finns så kallas metoden SaveGame i service klassen
        /// för att behandla datan. Sedan skickar vi in ett rättmeddelande till 
        /// PageExtensions och sist skickas användaren till spellistan för att undvika
        /// upprepning med refresh.
        /// </summary>
        /// <param name="gameId">Det specifika spelets id</param>
        public void GameFormView_UpdateItem(int gameId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var game = GetService.Service.GetGame(gameId);
                    if (game == null)
                    {
                        ModelState.AddModelError(String.Empty,
                            String.Format("Spelet med spelnummer {0} hittades inte.", gameId));
                        return;
                    }

                    if (TryUpdateModel(game))
                    {
                        GetService.Service.SaveGame(game);
                        Page.SetTempData("SuccessMessage", String.Format("Spelet '{0}' är nu uppdaterat.", game.Title));
                        Response.RedirectToRoute("GameList");
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
        }

        /// <summary>
        /// Hämtar det specifika spelId't från url'en och skickar till metoden GetGame
        /// I service klassen. Får sedan tillbaka uppgifterna på spelet som presenteras.
        /// </summary>
        /// <param name="Id">Det specifika id't från url'en</param>
        /// <returns>Uppgifterna på det specifika spelet</returns>
        public Game GameListView_GetItem([RouteData]int Id)
        {
            try
            {
                return GetService.Service.GetGame(Id);
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "Fel inträffade då kunden hämtades vid redigering.");
                return null;
            }
        }
    }
}