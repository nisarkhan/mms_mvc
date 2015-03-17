using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rhml.Mms;

namespace Rhml.Mms.Web.ViewModel
{
    public class RealtorListViewModel
    {
        [ScaffoldColumn(true)]
        public Guid UserId { get; set; }

        public string CompanyName { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }

        public bool? IsPreffered { get; set; }

        public string PrefferedStatus
        {
            get { return IsPreffered == true ? "Yes" : "No"; }
        }

        public static RealtorListViewModel ToView(Realtors_Master realtor)
        {
            RealtorListViewModel realtorListViewModel = new RealtorListViewModel();
            realtorListViewModel.UserId = realtor.user_id;
            realtorListViewModel.CompanyName = realtor.company;
            realtorListViewModel.Name = realtor.name;
            realtorListViewModel.Email = realtor.email;
            realtorListViewModel.UserName = realtor.user_name;
            realtorListViewModel.IsPreffered = realtor.preferred;
            realtorListViewModel.FirstName = realtor.first_name;
            realtorListViewModel.LastName = realtor.last_name;
            return realtorListViewModel;
        }

        public Realtors_Master ToModel()
        {
            Realtors_Master realtor = new Realtors_Master();
            realtor.user_id = this.UserId;
            realtor.company = this.CompanyName;
            realtor.name = this.Name;
            realtor.email = this.Email;
            realtor.user_name = this.UserName;
            realtor.preferred = this.IsPreffered;
            realtor.first_name = this.FirstName;
            realtor.last_name = this.LastName;
            return realtor;
        }
    }
}