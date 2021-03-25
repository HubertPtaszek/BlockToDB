namespace BlockToDB.Application
{
    public class AppUserEditVM
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
    }
}