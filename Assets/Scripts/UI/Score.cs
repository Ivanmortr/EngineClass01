namespace UI
{
    public class Score: IScore
    {
        public int ScorePoints { get; set; }

        public void AddPoints(int points) => ScorePoints += points;
    }
}