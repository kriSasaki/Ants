public class Egg : Resource
{
    protected override void NoticeResource(Inventory inventory, int amount)
    {
        inventory.ChangeEggsAmount(amount);
    }
}
