using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    using Microsoft.Owin.Security.DataProtection;
    using System.Web.Security;
    using DataProtectionProviderDelegate = Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>>;
    using DataProtectionTuple = Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>;

    /// <summary>
    /// Used to provide the data protection services that are derived from the MachineKey API. It is the best choice of
    /// data protection when you application is hosted by ASP.NET and all servers in the farm are running with the same Machine Key values.
    /// </summary>
    internal class MachineKeyDataProtectionProvider : IDataProtectionProvider
    {
        /// <summary>
        /// Returns a new instance of IDataProtection for the provider.
        /// </summary>
        /// <param name="purposes">Additional entropy used to ensure protected data may only be unprotected for the correct purposes.</param>
        /// <returns>An instance of a data protection service</returns>
        //public virtual MachineKeyDataProtector Create(params string[] purposes)
        //{

        //}

        public DataProtectionProviderDelegate ToOwinFunction()
        {
            return purposes =>
            {
                IDataProtector dataProtecter = Create(purposes);
                return new DataProtectionTuple(dataProtecter.Protect, dataProtecter.Unprotect);
            };
        }

        public IDataProtector Create(params string[] purposes)
        {
            return new MachineKeyDataProtector(purposes);
        }
    }

    internal class MachineKeyDataProtector : IDataProtector
    {
        private readonly string[] _purposes;

        public MachineKeyDataProtector(params string[] purposes)
        {
            _purposes = purposes;
        }

        public virtual byte[] Protect(byte[] userData)
        {
            byte[] result = MachineKey.Protect(userData, _purposes);
            return result;
        }

        public virtual byte[] Unprotect(byte[] protectedData)
        {
            return MachineKey.Unprotect(protectedData, _purposes);
        }

    }
}
