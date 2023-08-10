using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealTimeTasksReact.Data
{
    public class User
    {
        public int Id { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public List<TaskItem> tasks { get; set; }
    }
}
