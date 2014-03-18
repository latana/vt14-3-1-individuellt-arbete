using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WonderfulGames.Model.DAL;

namespace WonderfulGames.Model
{
    /// <summary>
    /// Validerar och hanterar data mellan dataåtkomstlagret och code-behind lagret.
    /// Kontrollerar data och väljer vilken metod som skall anropas.
    /// </summary>
    public class Service
    {

        private GameDAL _gameDAL;

        private GenreDAL _genreDal;

        private DeveloperDAL _developerDal;

        private GameDAL GameDAL
        {
            get { return _gameDAL ?? (_gameDAL = new GameDAL()); }
        }

        private GenreDAL GenreDAL
        {
            get { return _genreDal ?? (_genreDal = new GenreDAL()); }
        }

        private DeveloperDAL DeveloperDAL
        {
            get { return _developerDal ?? (_developerDal = new DeveloperDAL()); }
        }

        /// <summary>
        /// Hämtar ett spel med hjälp ut av ett specifikt id
        /// </summary>
        /// <param name="gameId">Spelts nummer</param>
        /// <returns>Ett Game objekt som innehåller spelets data-uppgifter</returns>
        public Game GetGame(int gameId)
        {
            return GameDAL.GetGameById(gameId);
        }

        /// <summary>
        /// Sparar ett spels uppgifter i databasen
        /// </summary>
        /// <param name="game">Spelets uppgifter som skall sparas</param>
        public void SaveGame(Game game)
        {
            ICollection<ValidationResult> validationResults;

            // Om någon av objekten i Model inte uppfyller validaringen så används en extension metod för validering.
            if (!game.Validate(out validationResults))
            {
                // Om valideringen inte uppfylls så kastas ett undantag med ett allmänt felmeddeland och en samling med felmeddelanden.
                var ex = new ValidationException("Spelobjektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            if (game.GameID == 0)
            {
                GameDAL.InsertGame(game);
            }
            else
            {
                GameDAL.UpdateGame(game);
            }
        }

        /// <summary>
        /// Tar bort det specifika spelet.
        /// </summary>
        /// <param name="gameId">Innehåller det specifika spelets id som skall tas bort</param>
        public void DeleteGame(int gameId)
        {
            GameDAL.DeleteGame(gameId);
        }

        /// <summary>
        /// Går igenom all data och räknar upp hur många sidor och rader som skall renderas ut.
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <returns></returns>
        public IEnumerable<Game> GetGamesPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return GameDAL.GetGamesPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        /// <summary>
        /// Hämtar den specifika utvecklaren.
        /// </summary>
        /// <param name="developerId">Innehåller ett id för den specifika utvecklaren</param>
        /// <returns></returns>
        public Developer GetDeveloperById(int developerId)
        {
            return DeveloperDAL.GetDeveloperById(developerId);
        }

        /// <summary>
        /// Hämtar ut alla namnen på utvecklarna
        /// </summary>
        /// <returns>Ett Developer-objekt som innehåller alla namnen på utvecklarna</returns>
        public IEnumerable<Developer> GetDeveloperName()
        {
            return DeveloperDAL.GetDeveloperName();
        }

        /// <summary>
        /// Sparar en utvecklare i databasen
        /// </summary>
        /// <param name="game">Utvecklarens id som skall sparas</param>
        public void SaveDeveloper(Developer developer)
        {
            // Om någon av objekten i Model inte uppfyller validaringen så används en extension metod för validering.
            ICollection<ValidationResult> validationResults;

            // Om valideringen inte uppfylls så kastas ett undantag med ett allmänt felmeddelande och en samling med felmeddelanden.
            if (!developer.Validate(out validationResults))
            {
                var ex = new ValidationException("Utvecklarobjectet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            if (developer.DeveloperID == 0)
            {
                DeveloperDAL.InsertDeveloper(developer);
            }
            else
            {
                DeveloperDAL.UpdateDeveloper(developer);
            }
        }

        /// <summary>
        /// Tar bort den specifika utvecklaren
        /// </summary>
        /// <param name="developerId">Innehåller det specifika id't</param>
        public void DeleteDeveloper(int developerId)
        {
            DeveloperDAL.DeleteDeveloper(developerId);
        }

        /// <summary>
        /// Visar alla utvecklare och räknar igenom hur många poster det finns och hur många som får visas på samma sida.
        /// </summary>
        /// <param name="maximumRows">Hur många rader som får finnas på varje sida</param>
        /// <param name="startRowIndex">Innehåller 1</param>
        /// <param name="totalRowCount">Innehåller 0 men kommer att innehålla antalet poster som räknats fram</param>
        /// <returns></returns>
        public IEnumerable<Developer> GetDevelopersPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return DeveloperDAL.GetDevelopersPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        /// <summary>
        /// Hämtar alla Genre i databasen
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns>Ett listobjekt som innehåller referenser till Genre-objektet</returns>
        public IEnumerable<Genre> GetGenreType(bool refresh = false)
        {
            // Hämtar listan med genretyperna från cachen
            var genreTypes = HttpContext.Current.Cache["GenreTypes"] as IEnumerable<Genre>;

            // Om det inte finns någon lista med genre
            if (genreTypes == null || refresh)
            {
                // Så hämtar vi listan med genre från databasen.
                genreTypes = GenreDAL.GetGenreType();

                // Sen cachar vi detta list-objekt och genre-objekt. Det kommer att ske igen efter 25 min
                //då denna data inte har någon CRUD.
                HttpContext.Current.Cache.Insert("GenreTypes", genreTypes, null, DateTime.Now.AddMinutes(25), TimeSpan.Zero);
            }
            return genreTypes;
        }
    }
}