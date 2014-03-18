using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;

namespace WonderfulGames.Model
{
    /// <summary>
    /// Hanterar och validerar datan från Gametabellen
    /// </summary>
    public class Game
    {
        public int GameID { get; set; }

        public int GenreID { get; set; }

        public int DeveloperID { get; set; }
        [Required(ErrorMessage = "Titelfältet är tomt")]
        [StringLength(50, ErrorMessage = "Titeln får max vara 50 tecken")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Utgivetfälet är tomt")]
        public DateTime Released { get; set; }

        public int Grade { get; set; }
    }
}