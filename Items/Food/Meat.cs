using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Food
{
    public class Meat : ModItem
    {
        public override void SetDefaults()
        {
            Item item = new Item();
            Item.DefaultToFood(34, 18, BuffID.WellFed, 18000);
            item.maxStack = 30;
            item.value = Item.sellPrice(silver: 5);
            item.consumable = true;
            item.rare = ItemRarityID.LightPurple;
        }
        public override void OnConsumeItem(Player player)
        {
            player.Heal(5);
            base.OnConsumeItem(player);
        }
    }
}