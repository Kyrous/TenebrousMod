using Microsoft.Xna.Framework;
using TenebrousMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Ranger
{
    public class RiptideBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 28;
            Item.useAmmo = AmmoID.Arrow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 32;
            Item.height = 52;
            Item.value = Item.sellPrice(silver: 80);
            Item.rare = ItemRarityID.Orange;
            Item.shootSpeed = 5f;
            Item.useAnimation = 16;
            Item.useTime = 16; 
            Item.shoot = ModContent.ProjectileType<WaterArrow>();
            Item.UseSound = SoundID.Item5;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ModContent.ProjectileType<WaterArrow>();
        }
    }
}
