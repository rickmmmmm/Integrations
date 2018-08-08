using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class Processes
    {
        public Processes()
        {
            Configurations = new HashSet<Configurations>();
            Mappings = new HashSet<Mappings>();
            ProcessTasks = new HashSet<ProcessTasks>();
            TransformationLookup = new HashSet<TransformationLookup>();
            Transformations = new HashSet<Transformations>();
        }

        public int ProcessUid { get; set; }
        public string Client { get; set; }
        public string ProcessName { get; set; }
        public int ProcessSourceUid { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }

        public ProcessSource ProcessSourceU { get; set; }
        public ICollection<Configurations> Configurations { get; set; }
        public ICollection<Mappings> Mappings { get; set; }
        public ICollection<ProcessTasks> ProcessTasks { get; set; }
        public ICollection<TransformationLookup> TransformationLookup { get; set; }
        public ICollection<Transformations> Transformations { get; set; }
    }
}
