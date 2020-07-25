using FreshMvvm;
using PatientAppLib.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientAppLib
{
    public class AllergyInfoPageModel : FreshBasePageModel
    {
        private long _AllergyIdLabel;
        private string _AllergyDescriptionLabel;

        public long AllergyIdLabel
        {
            get { return _AllergyIdLabel; }
            set { _AllergyIdLabel = value; RaisePropertyChanged(); }

        }
        public string AllergyDescriptionLabel
        {
            get { return _AllergyDescriptionLabel; }
            set { _AllergyDescriptionLabel = value; RaisePropertyChanged(); }
        }



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

    }
}
