using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PatientAppLib
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderPage : ContentPage
    {
        public ReminderPage()
        {

            InitializeComponent();
        }

        private void ContentPage_BindingContextChanged(object sender, EventArgs e)
        {

        }
    }
}