using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace zegarnik
{
    public class Alarm
    {
        public string Tytul { get; set; }
        public string Opis { get; set; }
        public DateTime KiedyZadzwonic { get; set; }
        public bool SprawdzCzyDzwonic()
        {
            if(DateTime.Now == KiedyZadzwonic)
            {
                return true;
            }
            return false;
        }
    }
}
