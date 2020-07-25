using FreshMvvm;
using ModelsLib;
using ModelsLib.Models;
using PatientAppLib.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PatientAppLib
{
    public class AllergyPageModel : FreshBasePageModel
    {

        #region Properties
        private ObservableCollection<AllergyInfoPageModel> allergyList;

        public ObservableCollection<AllergyInfoPageModel> AllergyList
        {
            get { return allergyList; }
            set { allergyList = value; RaisePropertyChanged(); }
        }

        #endregion


        #region Default Override functions  
        public override async void Init(object initData)
        {
            base.Init(initData);
            var srv = new AllergyService();
            var allergies = await srv.GetPatientAllergiesAsync();
            AllergyList = new ObservableCollection<AllergyInfoPageModel>();
            foreach (Allergy al in allergies)
            {
                AllergyList.Add(new AllergyInfoPageModel 
                { 
                    AllergyIdLabel = al.AllergyId, 
                    AllergyDescriptionLabel = al.Description 
                });
            }
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
