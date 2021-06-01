namespace BlockToDB.Application
{
    public class BlockToDBGenerateRemoteVM
    {
        public int? Id { get; set; }

        [RequiredShort]
        public string Json { get; set; }

        [RequiredShort]
        public string Name { get; set; }

        [RequiredShort]
        public string Url { get; set; }

        [RequiredShort]
        public string UserName { get; set; }

        [RequiredShort]
        public string Password { get; set; }
    }
}