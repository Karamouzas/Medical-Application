using FreshMvvm;
using PatientAppLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PatientAppMVVM
{
    public class ReminderPageModel : FreshBasePageModel
    {
        #region Fields
        private string _username;
        private string _password;
        #endregion

        #region Properties
        private ObservableCollection<ReminderInfoPageModel> reminderList;

        public ObservableCollection<ReminderInfoPageModel> ReminderList
        {
            get { return reminderList; }
            set { reminderList = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Default Override functions  
        public override void Init(object initData)
        {
            base.Init(initData);
            ReminderList = new ObservableCollection<ReminderInfoPageModel> { new ReminderInfoPageModel { TestLabel = "eksetaseis1" }, new ReminderInfoPageModel { TestLabel = "eksetaseis2" } };
            RaisePropertyChanged(nameof(ReminderList));
            //ReminderList.Add(new ReminderInfoPage());
            //ReminderList.Add(new ReminderInfoPage());
            //ReminderList.Add(new ReminderInfoPage());
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }
        #endregion

        #region Commands  
 
        #endregion
    }
}
