using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace TenebrousMod.Items.Weapons.Icerus
{
    public class DominicsLostBlade : ModItem
    {
        public override void SetDefaults()
        {

            Item.CloneDefaults(ItemID.WoodenSword);
            Item.damage = 52;
            Item.shoot = ModContent.ProjectileType<iceSlash>();
            Item.shootSpeed = 25;
            Item.useAnimation = Item.useTime = 15;
            Item.autoReuse = true;

        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(0.2);

        }

    }
    public class iceSlash : ModProjectile
    {

        public override void SetStaticDefaults()
        {

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;

        }
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.HallowBossRainbowStreak;
        public override void SetDefaults()
        {

            Projectile.width = Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 60;

        }


        public override bool PreDraw(ref Color lightColor)
        {

            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            for (int k = 1; k < Projectile.oldPos.Length; k++)
            {
                Vector2 pos = (Projectile.oldPos[k] - Main.screenPosition);
                Main.EntitySpriteDraw(texture, pos, null, Color.Lerp(Color.CornflowerBlue, Color.White, (float)k / (float)Projectile.oldPos.Length), Projectile.oldRot[k] - MathHelper.ToRadians(90), origin, new Vector2(0.4f, 1f), SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Color.White, 0, origin, new Vector2(0.5f, 1.25f), SpriteEffects.None, 0);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Color.White, MathHelper.ToRadians(90), origin, new Vector2(0.5f, 1.25f), SpriteEffects.None, 0);

            return false;
        }


        float rot = 0;
        public override void OnSpawn(IEntitySource source)
        {
            rot = Projectile.velocity.ToRotation();
        }

        public override void AI()
        {
            if (Projectile.timeLeft <= 50)
            {


                Vector2 targetVel = Main.player[Projectile.owner].Center - Projectile.Center;
                targetVel.Normalize();

                rot = rot.AngleTowards(targetVel.ToRotation(), 0.25f);
                Projectile.velocity = rot.ToRotationVector2() * 35;
                Projectile.rotation = rot - MathHelper.ToRadians(90);

                if (Projectile.Center.Distance(Main.player[Projectile.owner].Center) <= 30)
                {


                    Projectile.Kill();


                }





            }

        }

    }
}