namespace Sixoclock.Onyx.ModelFeatures.Dto
{
    public class ModelFeatureListDto
    {
        public int Id { get; set; }
        public int ChargepointModelId { get; set; }
        public string FeatureName { get; set; }
        public int OCPPFeatureId { get; set; }
    }
}
