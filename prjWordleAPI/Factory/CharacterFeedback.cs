namespace prjWordleAPI.Factory
{
    public class CharacterFeedback
    {
        public char Character { get; set; }
        public string Feedback { get; set; } // "correct", "present", "absent"
    }

    public class CheckWordResponse
    {
        public bool IsValid { get; set; }
        public List<CharacterFeedback> Feedback { get; set; }
        public string Message { get; set; }
    }
    public class GameState
    {
        public string TargetWord { get; set; }
        public int Attempts { get; set; }
    }
}
