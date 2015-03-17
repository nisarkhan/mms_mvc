using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Republic.Core.Data.Interfaces;
using Rhml.Mms;
using Rhml.Mms.Business.Interfaces;

namespace Rhml.Mms.Business.Validation
{
    public sealed class RealtorValidator : ValidatorBase<Realtors_Master>, Rhml.Mms.Business.Interfaces.IRealtorValidator
    {
        readonly IRepository<Realtors_Master> realtorData;

        public RealtorValidator(IBusinessState state, IRepository<Realtors_Master> realtorData)
            : base(state)
        {
            this.realtorData = realtorData;
        }

        public override bool Validate(Realtors_Master instance)
        {
            ValidateRequiredFields(instance);

            ValidateUniqueFirstName(instance);

            ValidateUniqueLastName(instance);

            ValidateUniqueUserName(instance);

            ValidateUniquePassword(instance);

            ValidateUniqueName(instance);
            
            return IsValid;
        }

        private void ValidateRequiredFields(Realtors_Master realtor)
        {
            // Required fields
            if (string.IsNullOrWhiteSpace(realtor.first_name)) AddError("First Name is required.");
            if (string.IsNullOrWhiteSpace(realtor.last_name)) AddError("Last Name is required.");
            if (string.IsNullOrWhiteSpace(realtor.password)) AddError("Password is required.");
            if (string.IsNullOrWhiteSpace(realtor.unique_user_key)) AddError("User key required.");
            if (string.IsNullOrWhiteSpace(realtor.user_name)) AddError("User name required.");
        }

        private void ValidateUniqueUserName(Realtors_Master realtor)
        {
            // Duplicate name
            if (realtorData.Any(a => a.user_name == realtor.user_name && a.user_id != realtor.user_id))
                AddError("An application with the name '{0}' already exists.", realtor.user_name);
        }

        private void ValidateUniqueFirstName(Realtors_Master realtor)
        {
            // Duplicate First name
            if(realtorData.Any(a => a.first_name == realtor.first_name && a.user_id != realtor.user_id))
                AddError("An realtor with the first name '{0}' already exists.", realtor.first_name);
        }

        private void ValidateUniqueLastName(Realtors_Master realtor)
        {
            // Duplicate Last name
            if (realtorData.Any(a => a.last_name == realtor.last_name && a.user_id != realtor.user_id))
                AddError("An realtor with the last name '{0}' already exists.", realtor.last_name);
        }

        private void ValidateUniqueName(Realtors_Master realtor)
        {
            // Duplicate name
            if (realtorData.Any(a => a.name == realtor.name && a.user_id != realtor.user_id))
                AddError("An realtor with the name '{0}' already exists.", realtor.name);
        }

        private void ValidateUniquePassword(Realtors_Master realtor)
        {
            // Duplicate password
            if (realtorData.Any(a => a.password == realtor.password && a.user_id != realtor.user_id))
                AddError("An realtor with the password '{0}' already exists.", realtor.password);
        }
    }
}
