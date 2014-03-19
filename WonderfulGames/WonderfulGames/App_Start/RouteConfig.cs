using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace WonderfulGames.App_Start
{
    /// <summary>
    /// Gör om länkarna i url'en till det som faller oss in.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Ser till att url'en inte heter sitt filnamn
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Default", "", "~/Pages/GamePages/GameList.aspx");

            routes.MapPageRoute("GameList", "spel", "~/Pages/GamePages/GameList.aspx");
            routes.MapPageRoute("GameCreate", "spel/ny", "~/Pages/GamePages/GameCreate.aspx");
            routes.MapPageRoute("GameDetails", "spel/{id}", "~/Pages/GamePages/GameDetails.aspx");
            routes.MapPageRoute("GameUpdate", "spel/{id}/uppdatera", "~/Pages/GamePages/GameUpdate.aspx");

            routes.MapPageRoute("DeveloperList", "utvecklare", "~/Pages/DeveloperPages/DeveloperList.aspx");

            routes.MapPageRoute("Error", "serverfel", "~/Pages/Shared/ErrorPage.aspx");
        }
    }
}
