using System;
using System.Collections.Generic;

namespace MultiStageForm.Models
{
    public partial class Stage2
    {
        public Stage2()
        {
            Stagedform = new HashSet<Stagedform>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Stagedform> Stagedform { get; set; }
    }
}
