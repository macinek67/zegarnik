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
    }
}