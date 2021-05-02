namespace BlockToDB.Domain
{
    public class DatabaseSchema : AuditEntity
    {
        public string Name { get; set; }
        public string Json { get; set; }
        public string Script { get; set; }
    }
}
