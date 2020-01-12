using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GanttChart;
using Model.Model;

namespace View.Graphs
{
    public static class GanttChart
    {
        public static void Draw(Output output)
        {
            //create a form 
            var form = new Form();
            var chart = new Chart(); //Can also be added via the designer
            chart.StartDate = DateTime.Today;
            chart.EndDate = DateTime.Today.AddDays(1);
            chart.StartHourInDay = 0;
            chart.EndHourInDay = output.Cycles.Count;

            //associate the viewer with the form 
            form.SuspendLayout();
            form.Controls.Add(chart); //Add the chart to the form
            form.ResumeLayout();
            form.Width = 800;
            form.Height = 500;
            chart.Dock = DockStyle.Fill; //Expand the chart to fill the form
            chart.DefaultDayLabelFormat = " ";

            var tasksCycles = output.Cycles.GroupBy(c => c.ProcessedTask).ToList();
            foreach (var task in output.ProcessedTasks.OrderBy(t => t.IncomeTime))
            {
                var taskCycles = tasksCycles.Single(tc => tc.Key.Equals(task.Label, StringComparison.OrdinalIgnoreCase));
                chart.Rows.Add(
                    new Row(task.Label)
                    {
                        TimeBlocks = new List<TimeBlock>
                        {
                            new TimeBlock(string.Empty,
                                DateTime.Today.AddHours(task.IncomeTime),
                                DateTime.Today.AddHours(taskCycles.Max(c => c.StartTime) + 1)) { Color = Color.Gray }
                        }.Concat(taskCycles.Select(tc => new TimeBlock(string.Empty, DateTime.Today.AddHours(tc.StartTime), DateTime.Today.AddHours(tc.StartTime+1)) {Color = Color.Black})).ToList()
                    });
            }
            chart.UpdateView();
            //show the form 
            form.ShowDialog();
        }
    }
}