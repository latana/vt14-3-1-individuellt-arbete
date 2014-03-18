using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WonderfulGames.Model;

namespace WonderfulGames.App_Infrastructure
{
    /// <summary>
    /// Instansierar och retunerar ut Service-objectet för alla code-behind filer
    /// 
    /// </summary>
    public static class GetService
    {
        private static Service _service;

        public static Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
    }
}