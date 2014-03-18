using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WonderfulGames.Model
{
    /// <summary>
    /// Hanterar och datan från Genretabellen
    /// </summary>
    public class Genre
    {
        public int GenreID { get; set; }

        public string GenreType { get; set; }
    }
}