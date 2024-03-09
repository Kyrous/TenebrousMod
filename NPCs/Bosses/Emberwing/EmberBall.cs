using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace TenebrousMod.NPCs.Bosses.Emberwing
{
    public class EmberBall : ModProjectile
    {
        private float AI_Time = 0;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            AI_Time++;
            if (AI_Time <= 30)
                Projectile.position -= Projectile.velocity;
            else
            {
                Projectile.velocity -= new Vector2(0, 0.25f).RotatedBy(Projectile.rotation);
            }
            Visuals();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle frame = new Rectangle(0, 0, 32, 32);
            Vector2 origin = new Vector2(16, 16);
            float rotation = Projectile.rotation;
            float scale = Projectile.scale;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, drawColor, rotation, origin, scale, SpriteEffects.None, 0);
            float opacity = 0;
            if (AI_Time >= 120)
                opacity = 0.5f;
            else
                opacity = (AI_Time - 30) / 90;
            frame = new Rectangle(32, 0, 32, 32);
            drawColor = Projectile.GetAlpha(Color.White);
            drawColor.A = 102;
            drawColor *= opacity;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, drawColor, rotation, origin, scale, SpriteEffects.None, 0);
            return false;
        }
        private void Visuals()
        {
            if (AI_Time >= 30)
            {
                for (var t = -1; t < 2; t++)
                {
                    Vector2 diection = Projectile.velocity.RotatedBy(Math.PI / 2 + Math.PI);
                    int pushDust = Dust.NewDust(Projectile.position, 8, 8, DustID.Flare, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[pushDust].noGravity = true;
                    Main.dust[pushDust].velocity += Vector2.Normalize(diection) * (float)Math.Sin(AI_Time * Math.PI / 15) * 4 * t;
                }
            }
        }
    }


    public class EmberBallFriendly : ModProjectile
    {
        private float AI_Time = 0;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.scale = 1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            AI_Time++;
            if (AI_Time <= 15)
                Projectile.position -= Projectile.velocity;
            else
            {
                Projectile.velocity -= new Vector2(0, 0.25f).RotatedBy(Projectile.rotation);
            }
            Visuals();
            FadeInAndOut();
        }
        public void FadeInAndOut()
        {
            // If last less than 50 ticks — fade in, than more — fade out
            if (Projectile.ai[0] <= 50f)
            {
                // Fade in
                Projectile.alpha -= 25;
                // Cap alpha before timer reaches 50 ticks
                if (Projectile.alpha < 100)
                    Projectile.alpha = 100;

                return;
            }

            // Fade out
            Projectile.alpha += 50;
            // Cal alpha to the maximum 255(complete transparent)
            if (Projectile.alpha > 255)
                Projectile.alpha = 255;

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle frame = new Rectangle(0, 0, 32, 32);
            Vector2 origin = new Vector2(16, 16);
            float rotation = Projectile.rotation;
            float scale = Projectile.scale;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, drawColor, rotation, origin, scale, SpriteEffects.None, 0);
            float opacity = 0;
            if (AI_Time >= 120)
                opacity = 0.5f;
            else
                opacity = (AI_Time - 30) / 90;
            frame = new Rectangle(32, 0, 32, 32);
            drawColor = Projectile.GetAlpha(Color.White);
            drawColor.A = 102;
            drawColor *= opacity;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, drawColor, rotation, origin, scale, SpriteEffects.None, 0);
            return false;
        }
        private void Visuals()
        {
            if (AI_Time >= 30)
            {
                for (var t = -1; t < 2; t++)
                {
                    Vector2 diection = Projectile.velocity.RotatedBy(Math.PI / 2 + Math.PI);
                    int pushDust = Dust.NewDust(Projectile.position, 8, 8, DustID.Flare, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[pushDust].noGravity = true;
                    Main.dust[pushDust].velocity += Vector2.Normalize(diection) * (float)Math.Sin(AI_Time * Math.PI / 15) * 4 * t;
                }
            }
        }
    }
}
