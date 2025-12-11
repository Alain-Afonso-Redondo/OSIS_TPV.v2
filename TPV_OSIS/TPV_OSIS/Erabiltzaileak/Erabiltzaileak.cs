using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_OSIS
{
    internal class Erabiltzaileak
    {
        public virtual int Id { get; set; }
        public virtual string Erabiltzailea { get; set; }
        public virtual string Pasahitza { get; set; }
    }
}