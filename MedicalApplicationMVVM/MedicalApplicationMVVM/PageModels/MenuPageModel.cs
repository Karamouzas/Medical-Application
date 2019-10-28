using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MedicalApplicationMVVM
{
    public class MenuPageModel : FreshBasePageModel
    {
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


        public Command NavigateProfilePage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<ProfilePageModel>();
                });
            }
        }
        public Command NavigatePatientTestPage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<PatientTestPageModel>();
                });
            }
        }
        public Command NavigatePatientDrugPage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<PatientDrugPageModel>();
                });
            }
        }
        public Command NavigateSupervisorPage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<SupervisorPageModel>();
                });
            }
        }
        public Command NavigateReminderPage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<ReminderPageModel>();
                });
            }
        }
        
    }
}
