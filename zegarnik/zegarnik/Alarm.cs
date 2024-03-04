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
        public TimeSpan KiedyZadzwonic { get; set; }
        public bool SprawdzCzyDzwonic()
        {
            if ((KiedyZadzwonic - DateTime.Now.TimeOfDay).TotalSeconds < 0)
            {
                return true;
            }
            return false;
        }
    }
}
