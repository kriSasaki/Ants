namespace Source.Scripts.UI
{
    public class MushroomResourceIcon : ResourceIcon
    {
        private void OnEnable()
        {
            PlayerChecker.OnResearchMushroomNeeded += ResearchResources;
        }

        private void OnDisable()
        {
            PlayerChecker.OnResearchMushroomNeeded -= ResearchResources;
        }
    }
}
