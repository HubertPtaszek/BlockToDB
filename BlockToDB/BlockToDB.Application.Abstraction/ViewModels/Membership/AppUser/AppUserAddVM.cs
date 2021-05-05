namespace BlockToDB.Application
{
    public class AppUserAddVM
    {
        public string AppIdentityUserId { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
    }
}