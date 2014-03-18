using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WonderfulGames.Model
{
    /// <summary>
    /// Hanterar och validerar datan från Developertabellen
    /// </summary>
    public class Developer
    {
        public int DeveloperID { get; set; }

        [Required(ErrorMessage = "Utvecklarfältet är tomt")]
        [StringLength(100, ErrorMessage = "Utvecklarfältet får max vara 100 tecken")]

        public string DeveloperName { get; set; }
    }
}