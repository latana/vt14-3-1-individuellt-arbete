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
    public partial class GameList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Kallar på GetGamesPageWise i service klassen för att få
        /// tillbaka en lista med spel.
        /// </summary>
        /// <param name="maximumRows">Hur många rader som får finnas på varje sida</param>
        /// <param name="startRowIndex">Innehåller 1</param>
        /// <param name="totalRowCount">Innehåller 0 men kommer att innehålla antalet poster som räknats fram</param>
        /// <returns>Alla spelen från tabellen.</returns>
        public IEnumerable<Game> GameListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return GetService.Service.GetGamesPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        
    }
}