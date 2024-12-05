namespace Sixoclock.Onyx.ModelFeatures.Dto
{
    public class CreateOrUpdateModelFeatureInput
    {
        public ModelFeature ModelFeature { get; set; }
        public int ChargepointModelId { get; set; }
    }
}
