using System.Collections;
using Source.Scripts.Player;

namespace Source.Scripts.Resources
{
    public class Egg : Resource
    {
        public override IEnumerator Give(Inventory inventory, int amount)
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