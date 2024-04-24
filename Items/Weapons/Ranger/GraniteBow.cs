using TenebrousMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Ranger
{
    public class GraniteBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 14;
            Item.useAmmo = AmmoID.Arrow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 26;
            Item.height = 42;
            Item.value = Item.sellPrice(silver: 40);
            Item.rare = ItemRarityID.Green;
            Item.shootSpeed = 4f;
            Item.useAnimation = 10;
            Item.useTime = 20;
            Item.shoot = ProjectileID.HellfireArrow;
            Item.UseSound = SoundID.Item5;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Granite, 18)
                .AddIngredient<GraniteShard>(1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
