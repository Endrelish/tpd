namespace Model.Model
{
    public class Task
    {
        private int _duration;
        public string Label { get; set; }

        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                ProcessingTimeLeft = value;
            }
        }

        public int ProcessingTimeLeft { get; set; }
        public int Priority { get; set; }
        public int IncomeTime { get; set; }

        public override string ToString()
        {
            return Label + ", income time: " + IncomeTime;
        }
    }
}