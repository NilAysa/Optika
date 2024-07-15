using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optika_Lens
{
    public class Korisnik
    {
        public int Index { get; set; }
        public string Ime_Prezime { get; set; }
        public string Broj_Telefona { get; set; }
        public int Id { get; set; }
    }

    public class Karton
    {
        public int Id { get; set; }
        public int Id_korisnika { get; set; }
        public string Pro_dist_longa_OS { get; set; }
        public string Pro_dist_longa_OD { get; set; }
        public string Pro_dist_media_OS { get; set; }
        public string Pro_dist_media_OD { get; set; }
        public string Pro_dist_propria_OS { get; set; }
        public string Pro_dist_propria_OD { get; set; }
        public string Dist_pupill { get; set; }
        public string Vrsta_stakla { get; set; }
        public string Proizvodjac_stakla { get; set; }
        public string Datum_pregleda { get; set; }
        public string Doktor { get; set; }
        public string Napomena { get; set; }
    }

}

