using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;

namespace AspNet.Identity.Repository
{
    /// <summary>
    /// Wrapper class for UserLoginInfo.
    /// Needed because UserLoginInfo cannot be decorated with DataContract and DataMember Attibutes and is sealed.
    /// </summary>
    [DataContract]
    public sealed class UserLoginInfoWrapper
    {
        public readonly UserLoginInfo UserLoginInfo;

        public UserLoginInfoWrapper()
        {
            // Needed for RethinkDB Provider deserialization, values will be overwritten
            UserLoginInfo = new UserLoginInfo(null, null, null);
        }

        public UserLoginInfoWrapper(string loginProvider, string providerKey, string providerDisplayName)
        {
            UserLoginInfo = new UserLoginInfo(loginProvider, providerKey, providerDisplayName);
        }

        /// <summary>
        /// Provider for the linked login, i.e. Facebook, Google, etc.
        /// </summary>
        [DataMember]
        public string LoginProvider
        {
            get
            {
                return UserLoginInfo.LoginProvider;
            }
            set
            {
                UserLoginInfo.LoginProvider = value;
            }
        }

        /// <summary>
        /// User specific key for the login provider
        /// </summary>
        [DataMember]
        public string ProviderKey
        {
            get
            {
                return UserLoginInfo.ProviderKey;
            }
            set
            {
                UserLoginInfo.ProviderKey = value;
            }
        }
    }
}
