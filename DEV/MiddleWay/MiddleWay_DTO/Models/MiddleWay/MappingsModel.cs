namespace MiddleWay_DTO.Models.MiddleWay
{
    public class MappingsModel
    {
        public int MappingsUid { get; set; }
        public int ProcessUid { get; set; }
        public string StepName { get; set; }
        public string SourceColumn { get; set; }
        public string DestinationColumn { get; set; }
        public bool Enabled { get; set; }
    }
}
