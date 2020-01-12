namespace Model.Model
{
    public class Cycle
    {
        public int StartTime { get; set; }
        public string ProcessedTask { get; set; }

        public override string ToString()
        {
            return $"Start time: {StartTime}, processed task: {ProcessedTask}";
        }
    }
}