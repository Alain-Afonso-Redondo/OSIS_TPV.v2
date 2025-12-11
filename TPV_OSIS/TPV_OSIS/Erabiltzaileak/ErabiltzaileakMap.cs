using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace TPV_OSIS
{
    internal class ErabiltzaileakMap : ClassMap<Erabiltzaileak>
    {
        ErabiltzaileakMap()
        {
            Table("Erabiltzaileak");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Erabiltzailea);
            Map(x => x.Pasahitza);
        }
    }

}