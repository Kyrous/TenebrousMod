using TenebrousMod.Items.Materials;
using TenebrousMod.Items.Weapons.Ranger;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Mage
{
    public class GraniteStaff : ModItem

    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.mana = 5;
            Item.DamageType = DamageClass.Magic;
            Item.width = 46;
            Item.height = 22;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(silver: 85);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GraniteOrbProj>();
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Granite, 25)
                .AddIngredient<GraniteShard>(1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
