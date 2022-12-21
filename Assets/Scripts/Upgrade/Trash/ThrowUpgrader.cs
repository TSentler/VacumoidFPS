namespace Upgrade
{
    public class ThrowUpgrader : Upgrader
    {
        private readonly string _upgradeName = "ThrowUpgrade";
        
        public float CalculateThrowSpeed(float startSpeed)
        {
            return startSpeed + startSpeed * UpFactor;
        }

        protected override string GetUpgradeName()
        {
            return _upgradeName;
        }
    }
}
