using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Food
{
    public class Meat : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToFood(34, 18, BuffID.WellFed, 18000);
            Item.maxStack = 30;
            Item.value = Item.sellPrice(silver: 5);
            Item.consumable = true;
            Item.rare = ItemRarityID.LightPurple;
        }
        public override void OnConsumeItem(Player player)
        {
            player.Heal(5);
        }
    }
}