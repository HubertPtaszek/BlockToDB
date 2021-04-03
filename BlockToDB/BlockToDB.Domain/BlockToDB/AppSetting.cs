namespace BlockToDB.Domain
{
    public class DatabaseSchema : Entity
    {
        public string Json { get; set; }
        public string Script { get; set; }
    }
}
