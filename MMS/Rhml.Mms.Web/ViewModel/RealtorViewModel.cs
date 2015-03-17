using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rhml.Mms;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Rhml.Mms.Web.ViewModel
{
    public class RealtorViewModel
    {
        [ScaffoldColumn(true)]
        public Guid UserId { get; set; }

        //[ScaffoldColumn(true)]
        [Required]
        [Display(Name = "Unique User Key")]
        public string UniqueUserKey { get; set; }

        [Display(Name = "User Name"), Required]
        public string UserName { get; set; }


        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$|^[2-9]\\d{2}\\d{3}\\d{4}$")]
        public string Phone { get; set; }

        [Display(Name = "Name"), Required]
        public string Name { get; set; }

        [Display(Name = "First Name"), Required]
        public string FirstName { get; set; }

        
        [Display(Name = "Last Name"), Required]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password"), Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Avator")]
        public string Avator { get; set; }

        [Display(Name = "Preffered")]
        public bool IsPreffered { get; set; }


         

        public static RealtorViewModel ToView(Realtors_Master realtor)
        {
            RealtorViewModel realtorViewModel = new RealtorViewModel();
            realtorViewModel.UserId = realtor.user_id;
            realtorViewModel.CompanyName = realtor.company;
            realtorViewModel.Name = realtor.name;
            realtorViewModel.Email = realtor.email;
            realtorViewModel.IsPreffered = Convert.ToBoolean((realtor.preferred));
            realtorViewModel.FirstName = realtor.first_name;
            realtorViewModel.LastName = realtor.last_name;
            realtorViewModel.Password = realtor.password;
            realtorViewModel.Avator = realtor.avatar;
            realtorViewModel.Password = realtor.password;
            realtorViewModel.UniqueUserKey = realtor.unique_user_key;
            realtorViewModel.UserName = realtor.last_name;
            return realtorViewModel;
        }

        public Realtors_Master ToModel()
        {
            Realtors_Master realtor = new Realtors_Master();
            realtor.user_id = this.UserId;
            realtor.company = this.CompanyName;
            realtor.name = this.Name;
            realtor.email = this.Email;
            realtor.preferred = this.IsPreffered;
            realtor.first_name = this.FirstName;
            realtor.last_name = this.LastName;
            realtor.password = this.Password;
            realtor.avatar = this.Avator;
            realtor.unique_user_key = this.UniqueUserKey;
            realtor.user_name = this.UserName;
            return realtor;
        }
    }
}