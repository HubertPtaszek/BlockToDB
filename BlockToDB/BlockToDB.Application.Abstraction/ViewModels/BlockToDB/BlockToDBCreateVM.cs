namespace BlockToDB.Application
{
    public class BlockToDBCreateVM
    {
        [RequiredShort]
        public string Json { get; set; }

        [RequiredShort]
        public string AddressIP { get; set; }

        [RequiredShort]
        public string User { get; set; }

        [RequiredShort]
        public string Password { get; set; }
    }
}