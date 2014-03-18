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
    public partial class GameDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Skickar iväg id't som hämtas ifrån url'en till service och
        /// presenterar ett specifikt spel.
        /// </summary>
        /// <param name="id">Innehåller det id som finns i url'en</param>
        /// <returns></returns>
        public Game GameDetails_GetItem([RouteData]int id)
        {
            try
            {
                return GetService.Service.GetGame(id);
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "Fel inträffade då spelet hämtades vid redigering.");
                return null;
            }
        }

        /// <summary>
        /// Hämtar datan som de främande nycklarna representerar
        /// och presenterar dem så att vi slipper se de främandenycklarnas
        /// intetsägande siffra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Objektet som innehåller den specifika datan vi vill omvandla</param>
        protected void GameListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {   // tar in den önskade taggen.
                var genreLabel = e.Item.FindControl("GenreLabel") as Label;
                if (genreLabel != null)
                {
                    //     Typomvandlar e.Item.DataItem så att primärnyckelns värde kan hämtas.
                    var game = (Game)e.Item.DataItem;
                    //   Hämtar det förhoppningsvis redan chachade objektet
                    var genreType = GetService.Service.GetGenreType()
                                         .Single(ct => ct.GenreID == game.GenreID);

                    //  Presenterar objektet så som vi vill ha det. Alltså vad den främande nyckeln representerar.
                    genreLabel.Text = String.Format(genreLabel.Text, genreType.GenreType);
                }
                // tar in den önskade taggen.
                var developerLabel = e.Item.FindControl("DeveloperLabel") as Label;
                if (developerLabel != null)
                {
                    //     Typomvandlar e.Item.DataItem så att primärnyckelns värde kan hämtas.
                    var game = (Game)e.Item.DataItem;
                    //    Hämtar objektet. Denna cashas inte då man har full crud på Developer
                    var gameType = GetService.Service.GetDeveloperName()
                                         .Single(ct => ct.DeveloperID == game.DeveloperID);

                    //  Presenterar objektet så som vi vill ha det. Alltså vad den främande nyckeln representerar.
                    developerLabel.Text = String.Format(developerLabel.Text, gameType.DeveloperName);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            
        }

        /// <summary>
        /// Skickar med det specifika id't till service klassen
        /// för att ta bort spelet. Sedan skickar vi in ett meddelande och 
        /// ett namn till "PageExtension för att presentera ett lyckat meddelande.
        /// Sist så tar vi användaren till spellistan för att undvika återupprepning med
        /// refresh.
        /// </summary>
        /// <param name="gameId">Det specifika spelets Id</param>
        public void GameListView_DeleteItem(int gameId)
        {
            try
            {
                GetService.Service.DeleteGame(gameId);
                this.SetTempData("SuccessMessage", "Spelet togs bort.");
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