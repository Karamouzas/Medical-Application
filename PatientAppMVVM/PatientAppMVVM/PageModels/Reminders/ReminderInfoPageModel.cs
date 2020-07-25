using FreshMvvm;
using System;

namespace PatientAppLib
{
    public class ReminderInfoPageModel : FreshBasePageModel
    {
        private string _testLabel;

        public string TestLabel
        {
            get { return _testLabel; }
            set { _testLabel = value; RaisePropertyChanged(); }
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
