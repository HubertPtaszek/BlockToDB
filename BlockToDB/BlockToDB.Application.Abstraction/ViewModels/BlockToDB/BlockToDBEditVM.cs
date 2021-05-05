namespace BlockToDB.Application
{
    public class BlockToDBEditVM
    {

        public int Id { get; set; }
        [RequiredShort]
        public string Name { get; set; }
        [RequiredShort]
        public string Json { get; set; }
    }
}