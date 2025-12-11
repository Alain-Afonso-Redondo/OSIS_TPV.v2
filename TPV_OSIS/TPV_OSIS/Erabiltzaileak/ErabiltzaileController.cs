using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_OSIS
{
    internal class ErabiltzaileController
    {
        public bool BalidatuLogin(string erabiltzailea, string pasahitza)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var erab = session.Query<Erabiltzaileak>()
                                  .FirstOrDefault(e => e.Erabiltzailea == erabiltzailea && e.Pasahitza == pasahitza);

                return erab != null;
            }
        }
    }
}