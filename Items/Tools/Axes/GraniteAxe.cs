using TenebrousMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Tools.Axes
{
    public class GraniteAxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.rare = ItemRarityID.Green;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 15;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 40;
            Item.height = 36;
            Item.axe = 11;
            Item.knockBack = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(silver: 20);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Granite, 8)
                .AddIngredient<GraniteShard>(1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
