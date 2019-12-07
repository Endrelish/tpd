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
        public static void Draw(Graph graph, Path criticalPath)
        {
            //create a form 
            var form = new Form();
            var ganttChart = new Chart(); //Can also be added via the designer
            ganttChart.StartDate = DateTime.Today;
            ganttChart.EndDate = DateTime.Today.AddDays(1);
            ganttChart.StartHourInDay = 0;
            ganttChart.EndHourInDay = criticalPath.Duration;

            //associate the viewer with the form 
            form.SuspendLayout();
            form.Controls.Add(ganttChart); //Add the chart to the form
            form.ResumeLayout();
            form.Width = 800;
            form.Height = 500;
            ganttChart.Dock = DockStyle.Fill; //Expand the chart to fill the form
            ganttChart.DefaultDayLabelFormat = " ";

            foreach (var v in graph.Vertices.Values.OrderBy(v => v.StartTime))
            {
                ganttChart.Rows.Add(
                    new Row(v.Name)
                    {
                        TimeBlocks = new List<TimeBlock>
                        {
                            new TimeBlock(v.Name,
                                DateTime.Today.AddHours(v.StartTime),
                                DateTime.Today.AddHours(v.StartTime + v.Duration)) { Color = criticalPath.Vertices.Contains(v) ? Color.MediumVioletRed : Color.CornflowerBlue }
                        }
                    });
            }
            ganttChart.UpdateView();
            //show the form 
            form.ShowDialog();
        }
    }
}