using TenebrousMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Melee
{
    public class GraniteBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.rare = ItemRarityID.Green;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 38;
            Item.height = 42;
            Item.knockBack = 2;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.IceBolt;
            Item.shootSpeed = 6f;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 1);

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Granite, 15)
                .AddIngredient<GraniteShard>(1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
