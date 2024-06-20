using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Skeletron
{
    public class BoneBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Melee;
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 30;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BoneBladeProj>();
            Item.shootSpeed = 5f;
        }
    }
    public class BoneBladeProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.light = 2f;
            Projectile.tileCollide = true;
            Projectile.scale = 1;
            Projectile.aiStyle = ProjAIStyleID.Beam;
        }

    }
}
