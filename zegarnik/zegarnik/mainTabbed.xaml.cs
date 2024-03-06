using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zegarnik
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mainTabbed : TabbedPage
    {
        List<Alarm> alarmy = new List<Alarm>();
        public mainTabbed()
        {
            InitializeComponent();

            alarmDateTime.Time = DateTime.Now.TimeOfDay;

            UpdateDateTime(null, null);

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += UpdateDateTime;
            timer.Start();
        }
        public void UpdateDateTime(object sunder, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                timeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
                dateLabel.Text = DateTime.Now.ToString("dd MMMM yyyy");

                foreach (Alarm alarm in alarmy.ToList())
                {
                    bool result = alarm.SprawdzCzyDzwonic();
                    if(result)
                    {
                        DisplayAlert(alarm.Tytul, alarm.Opis, "OK");
                        alarmy.Remove(alarm);
                    }
                }

                if (minutnik == Minutnik.nieustawiony) return;

                time = time.Subtract(TimeSpan.Parse("00:00:01"));
                WyswietlMinutnik();

                if (time <= TimeSpan.Parse("00:00:00"))
                {
                    time = new TimeSpan();
                    minutnik = Minutnik.nieustawiony;
                    DisplayAlert("Minutnik", "Alarm!!!\nDoliczono do zera", "OK");
                }
            });
        }

        private void setAlarmButton_Clicked(object sender, EventArgs e)
        {
            alarmy.Add(new Alarm()
            {
                Tytul = titleEntry.Text,
                Opis = descriptionEntry.Text,
                KiedyZadzwonic = alarmDateTime.Time
            });

            DisplayAlert("Udalo sie!", $"Alarm zostal ustawiony na {alarmDateTime.Time}", "OK");

            titleEntry.Text = "";
            descriptionEntry.Text = "";
            alarmDateTime.Time = DateTime.Now.TimeOfDay;
        }

        TimeSpan time = new TimeSpan();
        enum Minutnik
        {
            nieustawiony,
            ustawiony
        }
        Minutnik minutnik = new Minutnik();
        private void saveTimeButton_Clicked(object sender, EventArgs e)
        {
            int minutes = int.Parse((timeEntry.Text).Split(':')[0]);
            int hours = minutes / 60;
            minutes = minutes % 60;

            time = TimeSpan.Parse($"{hours}:{minutes}:00");
            minutnik = Minutnik.ustawiony;

            WyswietlMinutnik();

            timeEntry.Text = "";
        }

        private void WyswietlMinutnik()
        {
            timeToAlarmLabel.Text = time.ToString();
        }
    }
}