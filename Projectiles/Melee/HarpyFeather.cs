
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Projectiles.Melee
{
    public class HarpyFeather : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.HarpyFeather}"; 
        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Harpy, Alpha: 100, Scale: 0.75f);
                dust.velocity *= 0.3f;
                dust.noGravity = true;
            }
        }
    }
}
