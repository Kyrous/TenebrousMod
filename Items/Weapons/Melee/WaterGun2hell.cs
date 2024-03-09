using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TenebrousMod.Items.Weapons.Melee
{
    public class WaterGun2hell : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 600;
            Projectile.light = 0.25f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.scale = 1;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.005f;
            Visuals();
        }

        private void Visuals()
        {
            int water = Dust.NewDust(Projectile.Center, 8, 8, DustID.Water, 0f, 0f, 0, default(Color), 1f);
            Main.dust[water].noGravity = true;
            Main.dust[water].velocity = Vector2.Zero;
        }
    }
}
