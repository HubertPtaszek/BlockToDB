using BlockToDB.Dictionaries;

namespace BlockToDB.Domain
{
    public class AppSetting : Entity
    {
        public string Value { get; set; }
        public AppSettingEnum Type { get; set; }
    }
}
