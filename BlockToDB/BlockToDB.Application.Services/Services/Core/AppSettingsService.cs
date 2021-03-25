using BlockToDB.Data;
using BlockToDB.Dictionaries;
using BlockToDB.Domain;
using BlockToDB.Resources.Shared;
using Ninject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class AppSettingsService : ServiceBase, IAppSettingsService
    {
        #region Dependencies
        [Inject]
        public IAppSettingsRepository AppSettingsRepository { get; set; }
        #endregion


        private IList<AppSetting> _appSettings;

        private T GetSetting<T>(AppSettingEnum type)
        {
            if (_appSettings == null)
            {
                _appSettings = AppSettingsRepository.GetAll();
            }
            AppSetting element = _appSettings.FirstOrDefault(x => x.Type == type);
            if (element == null || element.Value == null)
            {
                throw new Exception(string.Format(ErrorResource.NoSetting, type.ToString()));
            }

            return (T)Convert.ChangeType(element.Value, typeof(T), CultureInfo.InvariantCulture);
        }

        private Dictionary<AppSettingEnum, T> GetSettingRange<T>(List<AppSettingEnum> types)
        {
            Dictionary<AppSettingEnum, T> result = new Dictionary<AppSettingEnum, T>();
            if (_appSettings == null)
            {
                _appSettings = AppSettingsRepository.GetAll();
            }
            foreach (var type in types)
            {
                AppSetting element = _appSettings.FirstOrDefault(x => x.Type == type);
                if (element == null || element.Value == null)
                {
                    throw new Exception(string.Format(ErrorResource.NoSetting, type.ToString()));
                }
                var convertedElement = (T)Convert.ChangeType(element.Value, typeof(T), CultureInfo.InvariantCulture);
                result.Add(type, convertedElement);
            }
            return result;
        }

        private void EditAppSetting(AppSettingEnum type, string value)
        {
            AppSetting setting = AppSettingsRepository.GetSingle(x => x.Type == type);
            if (setting == null)
                throw new BussinesException(3058, ErrorResource.NoData);

            setting.Value = value;
            AppSettingsRepository.Edit(setting);
        }

        public string GetApplicationWebAddress()
        {
            return GetSetting<string>(AppSettingEnum.ApplicationWebAddress);
        }
    }
}
