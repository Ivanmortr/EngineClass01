namespace UI
{
    public interface IScore
    {
        public int ScorePoints { get; set; }
        public void AddPoints(int points);
    }
}