using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Ranger
{
    public class HydroFury : ModItem
    {
        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 90;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAmmo = AmmoID.Arrow;
            Item.width = 32;
            Item.height = 70;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Lime;
            Item.shootSpeed = 7f;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.shoot = ModContent.ProjectileType<HydroArrowProj>();
            Item.UseSound = SoundID.Item5;
        }
    }
}