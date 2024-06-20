using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace TenebrousMod.Items.Food
{
    public class BatteryJuice : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToFood(24, 24, BuffID.WellFed, 4500);
            Item.maxStack = 30;
            Item.value = Item.sellPrice(silver: 15);
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
        }
        public override void OnConsumeItem(Player player)
        {
            player.Heal(10);
        }
    }
}
