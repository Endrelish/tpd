using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Model.Model;

namespace Model
{
    public class SelfishRoundRobin
    {
        private readonly List<Task> _availableTasks;
        private readonly List<Task> _newTasks;
        private readonly List<Task> _acceptedTasks;
        private readonly List<Task> _processedTasks;
        private readonly Random _random;
        private readonly int _acceptedRate;
        private double _incomeProbability;
        private readonly int _newRate;
        private int _currentlyProcessed;
        private int _currentTime;
        private List<Cycle> _cycles;

        public SelfishRoundRobin(Input input)
        {
            _random = new Random();
            _availableTasks = new List<Task>();
            for (var i = 0; i < input.TasksNumber; i++)
                _availableTasks.Add(new Task { Label = ((char)('a' + i)).ToString(), Duration = _random.Next(2, 8), Priority = 0 });
            _newTasks = new List<Task>();
            _acceptedTasks = new List<Task>();
            _processedTasks = new List<Task>();
            _acceptedRate = input.AcceptedRate;
            _newRate = input.NewRate;
            _incomeProbability = input.IncomeProbability;
            _currentTime = 0;
            _cycles = new List<Cycle>();
        }

        public Output Process()
        {
            TaskIncome(_random.Next(_availableTasks.Count));

            while (_availableTasks.Any() || _newTasks.Any() || _acceptedTasks.Any())
            {
                ProcessCycle();
            }

            return new Output{Cycles = _cycles, ProcessedTasks = _processedTasks};
        }

        private void ProcessCycle()
        {
            UpdatePriorities();
            AcceptTasks();
            ProcessTask();
            CheckForIncomingTasks();
        }

        private void CheckForIncomingTasks()
        {
            var index = _random.Next(Convert.ToInt32(Math.Ceiling(_availableTasks.Count / _incomeProbability)));
            if(index < _availableTasks.Count)
                TaskIncome(index);
        }

        private void ProcessTask()
        {
            _currentlyProcessed++;
            if (_currentlyProcessed >= _acceptedTasks.Count)
                _currentlyProcessed = 0;
            if (!_acceptedTasks.Any())
                return;
            var task = _acceptedTasks[_currentlyProcessed];
            _cycles.Add(new Cycle {ProcessedTask = task.Label, StartTime = _currentTime});
            task.ProcessingTimeLeft--;
            if(task.ProcessingTimeLeft == 0)
                TaskFinish(task);
            _currentTime++;
        }

        private void AcceptTasks()
        {
            if (_acceptedTasks.Any())
            {
                var acceptedPriority = _acceptedTasks.Min(t => t.Priority);
                foreach (var t in _newTasks.Where(t => t.Priority >= acceptedPriority).ToList())
                    TaskAccept(t);
            }
            else
            {
                TaskAccept(_newTasks.OrderByDescending(t => t.Priority).FirstOrDefault());
            }
        }

        private void UpdatePriorities()
        {
            _newTasks.ForEach(t => t.Priority += _newRate);
            _acceptedTasks.ForEach(t => t.Priority += _acceptedRate);
        }

        private void TaskIncome(int index)
        {
            var task = _availableTasks[index];
            task.IncomeTime = _currentTime;
            task.Priority = 0;
            _newTasks.Add(task);
            _availableTasks.RemoveAt(index);
        }

        private void TaskAccept(Task task)
        {
            if (task == null)
                return;
            _acceptedTasks.Add(task);
            _newTasks.Remove(task);
        }

        private void TaskFinish(Task task)
        {
            _processedTasks.Add(task);
            _acceptedTasks.Remove(task);
        }
    }
}