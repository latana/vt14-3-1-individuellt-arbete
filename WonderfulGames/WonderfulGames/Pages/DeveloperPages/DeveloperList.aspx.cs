using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WonderfulGames.App_Infrastructure;
using WonderfulGames.Model;

namespace WonderfulGames.Pages.DeveloperPages
{
    /// <summary>
    /// Första lagret för CRUD'en för utvecklarna.
    /// </summary>
    public partial class DeveloperList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Kallar på GetDevelopersPageWise i service klassen för att få
        /// tillbaka datat med utvecklare.
        /// </summary>
        /// <param name="maximumRows">Hur många rader som får finnas på varje sida</param>
        /// <param name="startRowIndex">Innehåller 1</param>
        /// <param name="totalRowCount">Innehåller 0 men kommer att innehålla antalet poster som räknats fram</param>
        /// <returns>Alla spelen från tabellen.</returns>
        public IEnumerable<Developer> DeveloperListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return GetService.Service.GetDevelopersPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        /// <summary>
        /// Kollar först så att det inte finns några problem med objektet vi får in.
        /// Kallar på SaveDeveloper metoden i service och skickar iväg uppgifterna
        /// som vi vill spara. Sedan länkar vi till samma sida för att undvika att användaren 
        /// duplicerar utvecklare med refresh.
        /// </summary>
        /// <param name="developer">Ett objekt med en utvecklare som så småningom skall sparas i databasen</param>
        public void DeveloperListView_InsertItem(Developer developer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    GetService.Service.SaveDeveloper(developer);
                    this.SetTempData("SuccessMessage", String.Format("'{0}' lades till.", developer.DeveloperName));
                    Response.RedirectToRoute("DeveloperList");
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
        }

        /// <summary>
        /// Skickar med det specifika id't till service klassen
        /// för att ta bort utvecklaren. Sedan skickar vi in ett meddelande och 
        /// ett namn till "PageExtension" för att presentera ett lyckat meddelande.
        /// Sist så tar vi användaren till samma sida för att undvika återupprepning med
        /// refresh.
        /// </summary>
        /// <param name="developerId">Den specifika utvecklarens Id</param>
        public void DeveloperListView_DeleteItem(int developerId)
        {
            try
            {
                GetService.Service.DeleteDeveloper(developerId);
                this.SetTempData("SuccessMessage", String.Format("Utvecklaren togs bort"));
                Response.RedirectToRoute("DeveloperList");
                Context.ApplicationInstance.CompleteRequest();
            }
            catch(Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då utvecklaruppgiften skulle tas bort.");
            }
        }

        /// <summary>
        /// Hämtar det specifika utvecklar id't och kontrollerar om det finns i databasen.
        /// Om det visar sig att det finns så kallas metoden SaveDeveloper i service klassen
        /// för att behandla datan. Sedan skickar vi in ett rättmeddelande till 
        /// PageExtensions och sist skickas användaren till samma sida för att undvika
        /// upprepning med refresh.
        /// </summary>
        /// <param name="gameId">Den specifika utvecklar id't</param>
        public void DeveloperListView_UpdateItem(int developerId)
        {
            try
            {
                var developer = GetService.Service.GetDeveloperById(developerId);
                if (developer == null)
                {
                    ModelState.AddModelError(String.Empty,
                        String.Format("Utvecklaren med nummer {0} hittades inte.", developerId));
                    return;
                }

                if (TryUpdateModel(developer))
                {
                    GetService.Service.SaveDeveloper(developer);
                    this.SetTempData("SuccessMessage", String.Format("'{0}' har uppdaterats", developer.DeveloperName));
                    Response.RedirectToRoute("DeveloperList");
                    Context.ApplicationInstance.CompleteRequest();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
        }

        /// <summary>
        /// Kontrollerar om det finns någon data som heter "Okänd"
        /// och i så fall tar den bort edit & delete knappen.
        /// </summary>
        /// <param name="sender">ett objekt som innehåller den aktiva kontrollen</param>
        /// <param name="e">Ett objekt som innehåller datan från ListView'n</param>
        protected void DeveloperListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var developer = e.Item.DataItem as Developer;
            if (developer != null && developer.DeveloperName == "Okänd")
            {
                e.Item.FindControl("developer").Visible = false;
                e.Item.FindControl("EditLinkButton").Visible = false;
                e.Item.FindControl("DeleteLinkButton").Visible = false;
            }
        }
    }
}