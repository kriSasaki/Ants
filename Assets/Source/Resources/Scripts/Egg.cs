using System.Collections;
using Source.Player.Scripts;

namespace Source.Resources.Scripts
{
    public class Egg : Resource
    {
        public override IEnumerator GiveResource(Inventory inventory, int amount)
        {
            while (IsPickUpInProgress)
            {
                HandleTick();
                yield return null;
            }
        
            inventory.ChangeEggsAmount(amount);
        }
    }
}