namespace Source.UI.Scripts
{
    public class EggResourceIcon : ResourceIcon
    {
        private void OnEnable()
        {
            PlayerChecker.OnResearchEggsNeeded += ResearchRecources;
        }

        private void OnDisable()
        {
            PlayerChecker.OnResearchEggsNeeded -= ResearchRecources;
        }
    }
}
