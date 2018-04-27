using System;
using System.Collections.Generic;

namespace MultiStageForm.Models
{
    public partial class Stage1
    {
        public Stage1()
        {
            Stagedform = new HashSet<Stagedform>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Stagedform> Stagedform { get; set; }
    }
}
