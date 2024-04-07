namespace Source.UI.Scripts
{
    public class MushroomResourceIcon : ResourceIcon
    {
        private void OnEnable()
        {
            PlayerChecker.OnResearchMushroomNeeded += ResearchRecources;
        }

        private void OnDisable()
        {
            PlayerChecker.OnResearchMushroomNeeded -= ResearchRecources;
        }
    }
}
