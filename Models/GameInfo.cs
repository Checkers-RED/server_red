namespace server_red.Models
{
    public class GameInfo
    {
        public string? white_nick { get; set; }
        public int white_score { get; set; }
        public string? black_nick { get; set; }
        public int black_score { get; set; }
        public int move_time { get; set; }
        public int rules_id { get; set; }
    }
}
