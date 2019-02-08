using Compaid.Common.Ioc.TinyIoc;
using Compaid.Common.Serialization;
using Compaid.Security.Providers;
using Compaid.Web.Ioc;
using IMS.Business;
using IMS.Business.Interfaces;
using IMS.Business.Provider;
using IMS.Common.Data.Interfaces;
using IMS.Data;
using IMS.Web.Provider;
using System.Configuration;
using IEncryptionProvider = Compaid.Common.Providers.IEncryptionProvider;
using IExceptionProvider = Compaid.Common.Providers.IExceptionProvider;
using IPasswordProvider = Compaid.Common.Providers.IPasswordProvider;
using IUserIdentityProvider = IMS.Common.Data.Interfaces.IUserIdentityProvider;

namespace IMS.Web.UI
{
    public class Bootstrap : BaseBootstrap
    {
        private static readonly Bootstrap instance = new Bootstrap();

        private Bootstrap() { }

        public static Bootstrap Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Register()
        {
            base.Register();

            TinyIocContainer.Current.Register<IIMSDataUnit, IMSDataUnit>().AsPerRequestSingleton();
            TinyIocContainer.Current.Register<IEncryptionProvider, EncryptionProvider>();
            TinyIocContainer.Current.Register<IExceptionProvider, ElmahProvider>().AsPerRequestSingleton();
            TinyIocContainer.Current.Register<ISerializationProvider, JsonSerializationProvider>().AsPerRequestSingleton();            
            TinyIocContainer.Current.Register<IUserIdentityProvider, WebUserIdentityProvider>().AsPerRequestSingleton();
            TinyIocContainer.Current.Register<ICookieAuthentationProvider, CookieProvider>().AsPerRequestSingleton();
            TinyIocContainer.Current.Register<IPasswordProvider, PasswordProvider>().AsPerRequestSingleton();
            TinyIocContainer.Current.Register<ICacheProvider, WebCacheProvider>().AsPerRequestSingleton();
            TinyIocContainer.Current.Register<IConfigurationProvider, WebConfigurationProvider>().AsPerRequestSingleton();

            this.RegisterConfigurationBasedDependencies();
        }
        private void RegisterConfigurationBasedDependencies()
        {
            if(ConfigurationManager.AppSettings["EmailMode"] == "SMTP")
            { 
                    TinyIocContainer.Current.Register<IEmailProvider, WebEmailProvider>().AsPerRequestSingleton();
            }
        }
    }
}