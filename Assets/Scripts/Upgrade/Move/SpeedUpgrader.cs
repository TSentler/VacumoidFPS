namespace Upgrade.Move
{
    public class SpeedUpgrader : Upgrader
    {
        private readonly string _upgradeName = "SpeedUpgrade";
        
        public float CalculateRunSpeed(float startSpeed)
        {
            return startSpeed + startSpeed * UpFactor;
        }

        protected override string GetUpgradeName()
        {
            return _upgradeName;
        }
    }
}
