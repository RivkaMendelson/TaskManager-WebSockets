using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTasksReact.Data
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }

    }
}
