using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace TenebrousMod.Projectiles
{
    public class EmberFlame : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.aiStyle = -1; // Custom AI
            Projectile.timeLeft = 600; // Projectile fades away after 1 second
            Projectile.penetrate = -1; // Penetrate indefinitely
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];



            if (Projectile.ai[0] == 0) // Initial circling phase
            {
                Vector2 newPosotion = Projectile.Center + new Vector2(0, 16).RotatedByRandom(Math.PI) * Projectile.scale;
                int fire = Dust.NewDust(newPosotion, 8, 8, DustID.Obsidian, 0f, 0f, 0, default(Color), 1f);
                Main.dust[fire].noGravity = true;
                //Main.dust[fire].velocity += Projectile.velocity * 0.25f;

                float rotationSpeed = MathHelper.TwoPi / 30f; // 0.5 seconds to complete one circle
                Projectile.ai[1] += rotationSpeed;

                // Circle around the player
                float orbitRadius = 5f;
                Vector2 orbitOffset = new Vector2(orbitRadius + Projectile.ai[1] * 1.2f, 0).RotatedBy(Projectile.ai[1]);
                Projectile.velocity = orbitOffset;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;
                Projectile.position = player.position + orbitOffset;
                Projectile.position -= Projectile.velocity;

                if (!player.channel)
                {
                    // TODO: is this multiplayer friendly?
                    Projectile.ai[0] = 1; // Switch to homing phase
                    Projectile.velocity *= 0.25f;
                    Projectile.penetrate = 1;
                    Projectile.timeLeft += 300;
                    Projectile.usesLocalNPCImmunity = true;
                    Projectile.localNPCHitCooldown = 0;
                    Projectile.light = 1f;

                    // Particles
                    for (int i = 0; i < 10; i++)
                    {

                        int spawn_dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.Obsidian, 0f, 0f, 0, default(Color), 1f);
                        Vector2 dustvelo = new Vector2(3, 0).RotatedBy(MathHelper.ToRadians(36 * i));
                        Main.dust[spawn_dust].noGravity = true;
                        Main.dust[spawn_dust].velocity = dustvelo;
                    }
                    var j = 0;
                    for (var i = 0; i < 10; i++)
                    {
                        int spawn_dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.Obsidian, 0f, 0f, 0, default(Color), 1f);
                        Vector2 dustvelo = new Vector2(j + 4, 0).RotatedBy(MathHelper.ToRadians(72 * i));
                        Main.dust[spawn_dust].noGravity = true;
                        Main.dust[spawn_dust].velocity = dustvelo;
                        if ((i % 5) == 0)
                            j++;
                    }
                }
            }
            else if (Projectile.ai[0] == 1) // Homing phase
            {
                Vector2 newPosotion = Projectile.Center + new Vector2(0, 16).RotatedByRandom(Math.PI) * Projectile.scale;
                int fire = Dust.NewDust(newPosotion, 8, 8, DustID.SolarFlare, 0f, 0f, 0, default(Color), 1f);
                Main.dust[fire].noGravity = true;
                Main.dust[fire].velocity += Projectile.velocity * 0.25f;

                int target = FindNearestEnemy(Projectile.position, 1200f); // Adjust the search range as needed

                Projectile.velocity *= 0.97f;
                Projectile.rotation += MathHelper.Pi / 4;

                if (target != -1)
                {
                    Vector2 targetPosition = Main.npc[target].Center;
                    Vector2 moveDirection = targetPosition - Projectile.Center;
                    float distance = moveDirection.Length();

                    if (distance > 0)
                    {
                        moveDirection.Normalize();
                        moveDirection *= 0.4f; // Adjust the speed as needed
                        Projectile.velocity += moveDirection;
                    }
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle frame = new Rectangle(32 * (int)Projectile.ai[0], 0, 32, 32);
            Vector2 origin = new Vector2(16, 16);
            float rotation = Projectile.rotation;
            float scale = Projectile.scale;
            Color drawColor = Projectile.GetAlpha(lightColor);
            if (Projectile.ai[0] == 1)
            {
                drawColor = Projectile.GetAlpha(Color.White);
            }
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, drawColor, rotation, origin, scale, SpriteEffects.None, 0);
            /*float opacity = 1;
            frame = new Rectangle(32, 0, 32, 32);
            drawColor = Projectile.GetAlpha(Color.White);
            drawColor.A = 102;
            drawColor *= opacity;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, drawColor, rotation, origin, scale, SpriteEffects.None, 0);*/
            return false;
        }

        private int FindNearestEnemy(Vector2 position, float maxDistance)
        {
            int closestNPC = -1;
            float closestDist = maxDistance;

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.CanBeChasedBy())
                {
                    float distance = Vector2.Distance(position, npc.Center);
                    if (distance < closestDist)
                    {
                        closestDist = distance;
                        closestNPC = i;
                    }
                }
            }

            return closestNPC;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 1)
            {
                target.AddBuff(323, 180);
                SoundEngine.PlaySound(SoundID.NPCHit18, Projectile.Center);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            }

        }
        public override void OnKill(int timeLeft)
        {
            if (Projectile.ai[0] == 1)
                for (var i = 0; i < 25; i++)
                {
                    Vector2 newPosotion = Projectile.Center + new Vector2(0, 16).RotatedByRandom(Math.PI) * Projectile.scale;
                    int fire = Dust.NewDust(newPosotion, 8, 8, DustID.SolarFlare, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[fire].noGravity = true;
                    Main.dust[fire].velocity += Projectile.velocity * 0.25f;
                }
        }
    }
}
