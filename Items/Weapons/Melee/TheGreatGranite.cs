using TenebrousMod.Items.Weapons.Ranger;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Melee
{
    public class TheGreatGranite : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.rare = ItemRarityID.Orange;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 48;
            Item.height = 54;
            Item.knockBack = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GraniteOrbProj>();
            Item.shootSpeed = 9f;
        }
    }
}
