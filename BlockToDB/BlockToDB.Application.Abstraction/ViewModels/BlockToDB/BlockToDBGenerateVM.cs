namespace BlockToDB.Application
{
    public class BlockToDBGenerateVM
    {
        public int? Id { get; set; }

        [RequiredShort]
        public string Json { get; set; }

        [RequiredShort]
        public string Name { get; set; }
    }
}