namespace BlockToDB.Application
{
    public class AppUserData
    {
        public AppUserData()
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AppIdentityUserId { get; set; }
    }
}