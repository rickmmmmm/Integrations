namespace MiddleWay_DTO.Models.MiddleWay
{
    public class TransformationsModel
    {
        public int TransformationUid { get; set; }
        public int ProcessUid { get; set; }
        public string StepName { get; set; }
        public string Function { get; set; }
        public string Parameters { get; set; }
        public string SourceColumn { get; set; }
        public string DestinationColumn { get; set; }
        public bool Enabled { get; set; }
        public int Order { get; set; }
    }
}
