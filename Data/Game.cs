namespace TA_Apricode.Data
{
    public class Game
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Developer { get; set; }
        public required List<string> Genres { get; set; }
    }
}
