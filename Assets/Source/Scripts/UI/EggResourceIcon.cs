namespace Source.Scripts.UI
{
    public class EggResourceIcon : ResourceIcon
    {
        private void OnEnable()
        {
            PlayerChecker.OnResearchEggsNeeded += ResearchResources;
        }

        private void OnDisable()
        {
            PlayerChecker.OnResearchEggsNeeded -= ResearchResources;
        }
    }
}
