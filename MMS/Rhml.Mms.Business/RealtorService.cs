using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Republic.Core.Data.Interfaces;
using Republic.Core.Interfaces;
using Rhml.Mms;
using Rhml.Mms.Business.Interfaces;
using Rhml.Mms.Interfaces;


namespace Rhml.Mms.Business
{
    public class RealtorService : BusinessBase, IRealtorService
    {
        private IRepository<Realtors_Master> realtorData;

        private Lazy<IAppConfiguration> lazyconfig;
        private IAppConfiguration Config { get { return this.lazyconfig.Value; } }

        private Lazy<IRealtorValidator> lazyRealtorvalidator;
        private IRealtorValidator realtorValidator { get { return this.lazyRealtorvalidator.Value; } }

        public RealtorService(
            IServiceLocator service,
            IRepository<Realtors_Master> realtor,
            Lazy<IAppConfiguration> config,
            Lazy<IRealtorValidator> realtorvalitor)
            : base(service)
        {
            realtorData = realtor;
            this.lazyconfig = config;
            this.lazyRealtorvalidator = realtorvalitor;
        }

        public void SaveRealtor(Realtors_Master realtor)
        {
            if (realtor == null) throw new InvalidOperationException("realtor cannot be null.");
            
           
            if (!realtorValidator.Validate(realtor)) return;

            if (realtor.user_id == Guid.Empty)
            {
                realtor.user_id = Guid.NewGuid();//only this needs to be populated, all other fields are in the model
                realtorData.Insert(realtor);
            }
            else
            {
                var dbRealtor = GetRealtor(realtor.user_id);
                if (dbRealtor == null) throw new InvalidOperationException("Cannot find the realtor specified.");

                
                dbRealtor.preferred = realtor.preferred;
                dbRealtor.phone = realtor.phone;
                dbRealtor.email = realtor.email;
                dbRealtor.company = realtor.company;
                dbRealtor.avatar = realtor.avatar;

            }
            realtorData.GetContext().Save();
        }

        public IEnumerable<Realtors_Master> GetRealtors()
        {
            return realtorData.AsEnumerable();
        }

        public Realtors_Master GetRealtor(Guid userId)
        {
            return realtorData.SingleOrDefault(x => x.user_id == userId);
        }

        public void DeleteRealtor(Guid userId)
        {
            Realtors_Master realtor = GetRealtor(userId);
            
            if (realtor == null) throw new ArgumentNullException("Realtor does not exist");
            
            realtorData.Delete(realtor);
            realtorData.GetContext().Save();
        }
    }
}
