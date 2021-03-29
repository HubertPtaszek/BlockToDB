using BlockToDB.Dictionaries;

namespace BlockToDB.Domain
{
    public class AppRole : AuditEntity
    {
        public AppRole()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public AppRoleType AppRoleType { get; set; }
    }
}