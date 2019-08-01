namespace DeleteAuditLog
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Client;
    using NLog;
    using System;
    using System.Configuration;
    using System.ServiceModel.Description;

    class CRMConnector
    {
        public static IOrganizationService _service;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Methods
        public static IOrganizationService ConnectToCRM()
        {
            try
            {
                if (_service == null)
                {
                    ClientCredentials credential = new ClientCredentials();
                    credential.UserName.UserName = ConfigurationManager.AppSettings["USERNAME"].ToString();
                    credential.UserName.Password = ConfigurationManager.AppSettings["PASSWORD"].ToString();// "Pa$$w0rd";// Cryptographer.Decrypt(ConfigurationManager.AppSettings["OrgPassword"].ToString(), "futuregroup");

                    Uri OrganizationUri = new Uri(ConfigurationManager.AppSettings["CRM_CONNECTION_STRING"].ToString());// ConfigurationManager.AppSettings["OrgURI"].ToString());
                    Uri HomeRealmUri = null;

                    OrganizationServiceProxy orgservice = new OrganizationServiceProxy(OrganizationUri, HomeRealmUri, credential, null);
                    orgservice.Timeout = new TimeSpan(4, 0, 0);
                    _service = orgservice;
                }
                return (_service);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in connectin " + ex.Message);
                Console.WriteLine("Exception in connectToCRM " + ex.Message + " " + ex.StackTrace);
                return null;
            }
        }
        #endregion
    }
}
