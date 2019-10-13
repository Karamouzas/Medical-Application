using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MedicalApplicationMVVM
{
    public class ProfilePageModel : FreshBasePageModel
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

        public Command NavigateAllergyPage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<AllergyPageModel>();
                });
            }
        }
        
    }
}
