using TenebrousMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Space
{
    public class Starlight : ModItem
    {
        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 14;
            Item.useAmmo = AmmoID.Arrow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 26;
            Item.height = 38;
            Item.value = Item.sellPrice(silver: 75);
            Item.rare = ItemRarityID.Green;
            Item.shootSpeed = 6f;
            Item.useAnimation = 10;
            Item.useTime = 20;
            Item.shoot = ProjectileID.HellfireArrow;
            Item.UseSound = SoundID.Item5;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Star, 8)
                .AddIngredient<StarEssence>(1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}