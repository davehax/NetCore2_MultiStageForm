using System;
using System.Collections.Generic;

namespace MultiStageForm.Models
{
    public partial class Stage3
    {
        public Stage3()
        {
            Stagedform = new HashSet<Stagedform>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Stagedform> Stagedform { get; set; }
    }
}
