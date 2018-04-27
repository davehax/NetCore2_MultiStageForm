using System;
using System.Collections.Generic;

namespace MultiStageForm.Models
{
    public partial class Stagedform
    {
        public int Id { get; set; }
        public int Stage1 { get; set; }
        public int Stage2 { get; set; }
        public int Stage3 { get; set; }
        public int? CurrentStage { get; set; }

        public Stage1 Stage1Navigation { get; set; }
        public Stage2 Stage2Navigation { get; set; }
        public Stage3 Stage3Navigation { get; set; }
    }
}
