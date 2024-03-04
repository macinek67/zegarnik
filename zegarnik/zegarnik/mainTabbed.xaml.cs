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
        public mainTabbed()
        {
            InitializeComponent();
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
            });
        }
    }
}