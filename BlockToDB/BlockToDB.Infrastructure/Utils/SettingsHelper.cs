using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockToDB.Utils
{
    public class SettingsHelper
    {
        #region secureAppSettings

        public static string CredentailUserName
        {
            get
            {
                return ((System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["CredentailUserName"];
            }
        }

        public static string Email
        {
            get
            {
                return ((System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["Email"];
            }
        }

        public static string PwdEmail
        {
            get
            {
                return ((System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["PwdEmail"];
            }
        }

        public static string SmtpServer
        {
            get
            {
                return ((System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["SmtpServer"];
            }
        }

        public static int SmtpPort
        {
            get
            {
                int result = 0;
                int.TryParse(((System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["SmtpPort"], out result);
                return result;
            }
        }

        public static string StoragePath
        {
            get
            {
                return ((System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["StoragePath"];
            }
        }

        public static string StorageURL
        {
            get
            {
                return ((System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["StorageURL"];
            }
        }

        public static string EvoLicense
        {
            get
            {
                return ((System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["EvoLicense"];
            }
        }

        public static string HappyFoxCredentials
        {
            get
            {
                return ((System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["HappyFoxCredentials"];
            }
        }

        #endregion secureAppSettings

        #region appSettings

        public static bool SavePrintingPartsToFile
        {
            get
            {
                bool result = false;
                bool.TryParse(ConfigurationManager.AppSettings["SavePrintingPartsToFile"], out result);
                return result;
            }
        }

        public static string AccountingDocumentsSubDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["AccountingDocumentsSubDirectory"];
            }
        }

        public static string DocumentsSubDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["DocumentsSubDirectory"];
            }
        }

        public static string ServiceRequestsSubDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceRequestsSubDirectory"];
            }
        }
        public static string InsuranceDocumentsSubDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["InsuranceDocumentsSubDirectory"];
            }
        }
        public static string BankTransferImportFilesSubDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["BankTransferImportFilesSubDirectory"];
            }
        }
        public static string AnnexAttachmentsSubDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["AnnexAttachmentsSubDirectory"];
            }
        }
        public static string EmailAttachmentsSubDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailAttachmentsSubDirectory"];
            }
        }

        public static string FormatPLN
        {
            get
            {
                return ConfigurationManager.AppSettings["FormatPLN"];
            }
        }
        public static string FormatPLNFractional
        {
            get
            {
                return ConfigurationManager.AppSettings["FormatPLNFractional"];
            }
        }

        public static string AgreementsSubdirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["AgreementsSubdirectory"];
            }
        }

        public static string HappyFoxApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["HappyFoxApiUrl"];
            }
        }

        public static string HappyFoxPolicyFieldName
        {
            get
            {
                return ConfigurationManager.AppSettings["HappyFoxPolicyFieldName"];
            }
        }

        public static int HappyFoxCategoryIdAsysta
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HappyFoxCategoryId_Asysta"], out result);
                return result;
            }
        }

        public static int HappyFoxRiskIdAsysta
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HappyFoxRiskId_Asysta"], out result);
                return result;
            }
        }

        public static int HappyFoxCategoryIdOchrona
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HappyFoxCategoryId_Ochrona"], out result);
                return result;
            }
        }
        public static int HappyFoxRiskIdOchrona
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HappyFoxRiskId_Ochrona"], out result);
                return result;
            }
        }


        public static int HappyFoxStatusForValidPolicy
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HappyFoxStatusForValidPolicy"], out result);
                return result;
            }
        }
        public static int HappyFoxStatusForInvalidPolicy
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HappyFoxStatusForInvalidPolicy"], out result);
                return result;
            }
        }

        public static int HappyFoxAssagneeIdForValidPolicy
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HappyFoxAssagneeIdForValidPolicy"], out result);
                return result;
            }
        }
        public static int HappyFoxAssagneeIdForInvalidPolicy
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HappyFoxAssagneeIdForInvalidPolicy"], out result);
                return result;
            }
        }
        public static int HappyFoxStaffId
        {
            get
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HappyFoxStaffId"], out result);
                return result;
            }
        }

        public static bool HangFireOn
        {
            get
            {
                bool result = true;
                bool.TryParse(ConfigurationManager.AppSettings["HangFireOn"], out result);
                return result;
            }
        }

        public static bool EFConfigurationInCode
        {
            get
            {
                bool result = true;
                bool.TryParse(ConfigurationManager.AppSettings["EFConfigurationInCode"], out result);
                return result;
            }
        }
        public static string InsuranceSummaryDocumentsSubDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["InsuranceSummaryDocumentsSubDirectory"];
            }
        }

        public static bool TemplatesPrecompilerOn
        {
            get
            {
                bool result;
                bool.TryParse(ConfigurationManager.AppSettings["TemplatesPrecompilerOn"], out result);
                return result;
            }
        }

        public static double LogoutTimespan
        {
            get
            {
                double result = 20;
                double.TryParse(ConfigurationManager.AppSettings["LogoutTimespan"], out result);
                return result;
            }
        }

        public static IReadOnlyList<string> RolesToLogInLeef
        {
            get
            {
                var rawValue = ConfigurationManager.AppSettings["RolesToLogInLEEF"] ?? string.Empty;
                return rawValue.Split(',').ToList();
            }
        }

        public static bool ShowExceptions
        {
            get
            {
                bool result;
                bool.TryParse(ConfigurationManager.AppSettings["ShowExceptions"], out result);
                return result;
            }
        }

        public static string Theme => ConfigurationManager.AppSettings[nameof(Theme)];
        public static string PolicyExportServiceUserName => ConfigurationManager.AppSettings[nameof(PolicyExportServiceUserName)];
        public static string PolicyExportServicePassword => ConfigurationManager.AppSettings[nameof(PolicyExportServicePassword)];

        public static string ConnectionString(string connectionName = "DefaultConnection") => System.Configuration
            .ConfigurationManager.ConnectionStrings[connectionName]
            .ConnectionString;

        #endregion appSettings
    }
}
