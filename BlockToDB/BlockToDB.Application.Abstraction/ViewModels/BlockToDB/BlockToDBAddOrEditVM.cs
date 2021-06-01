namespace BlockToDB.Application
{
    public class BlockToDBAddOrEditVM
    {
        public int? Id { get; set; }
        [RequiredShort]
        public string Name { get; set; }
        [RequiredShort]
        public string Json { get; set; }
    }
}