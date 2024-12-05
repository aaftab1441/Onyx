namespace Sixoclock.Onyx.ChargepointFeatures.Dto
{
    public class ChargepointFeatureListDto
    {
        public int Id { get; set; }
        public int ChargepointId { get; set; }
        public string FeatureName { get; set; }
        public int ModelFeatureId { get; set; }
        public int OCPPFeatureId { get; set; }
    }
}
