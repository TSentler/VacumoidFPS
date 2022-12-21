namespace Upgrade
{
    public class VacuumRadiusUpgrader : Upgrader
    {
        private readonly string _upgradeName = "DistanceUpgrader";
        
        public float CalculateRadius(float startRadius)
        {
            return startRadius + startRadius * UpFactor;
        }
        
        protected override string GetUpgradeName()
        {
            return _upgradeName;
        }
    }
}
