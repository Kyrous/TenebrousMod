using TenebrousMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Accessories
{
    public class GraniteEmblem : ModItem
    {
            public override void SetDefaults()
            {
                Item.width = 24;
                Item.height = 24;
                Item.value = Item.sellPrice(gold: 2);
                Item.rare = ItemRarityID.Green;
                Item.defense = 2;
                Item.accessory = true;
            }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            base.UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Granite, 30)
                .AddIngredient<GraniteShard>(1)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
