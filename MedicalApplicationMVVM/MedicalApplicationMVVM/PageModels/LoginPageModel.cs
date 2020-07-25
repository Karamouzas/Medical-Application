using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PatientAppLib
{
    public class LoginPageModel : FreshBasePageModel
    {
        #region Fields
        private string _username;
        private string _password;
        #endregion

        #region Properties
        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged("Username"); }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged("Password"); }
        }
        #endregion

        #region Default Override functions  
        public override void Init(object initData)
        {
            base.Init(initData);
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
        public Command NavigateMenuPage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PopPageModel();
                    await CoreMethods.PushPageModel<MenuPageModel>();
                });
            }
        }
        #endregion
    }
}
