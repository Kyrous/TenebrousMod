using TenebrousMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Melee
{
    public class StarKeeper : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.rare = ItemRarityID.Green;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 38;
            Item.height = 42;
            Item.knockBack = 2;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.Starfury;
            Item.shootSpeed = 8f;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 1);

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Star, 10)
                .AddIngredient<StarEssence>(1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
