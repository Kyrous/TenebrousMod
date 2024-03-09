using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Accessories
{
    public class EtherealVitalityCore : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.value = Item.sellPrice(gold: 8);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 4;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 2;
            player.GetDamage<GenericDamageClass>() *= 1.10f;
            if (player.ZoneGranite)
            {
                player.GetDamage<GenericDamageClass>() *= 1.05f;
                player.statDefense += 4;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<EtherealEmblem>(1)
                .AddIngredient<GraniteEmblem>(1)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
