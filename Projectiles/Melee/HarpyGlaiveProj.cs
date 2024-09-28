using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Projectiles.Melee
{
    public class HarpyGlaiveProj : ModProjectile
    {
        public enum WeaponState : int
        {
            Normal = 0,
            Downwards = 1
        }

        public WeaponState CurrentState
        {
            get => (WeaponState)Projectile.ai[1];
            set => Projectile.ai[1] = (float)value;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
        }

        Player Player => Main.player[Projectile.owner];
        float travelSpeed = 22f;
        public override void AI()
        {
            Player.heldProj = Projectile.whoAmI;
            Projectile.spriteDirection = Projectile.direction = Player.direction;
            Projectile.Center = Player.Center;

            if (CurrentState == WeaponState.Normal)
            {
                NormalBehaviour();
            }
            else if (CurrentState == WeaponState.Downwards)
            {
                PointDownwards();
            }
        }

        public void NormalBehaviour()
        {
            if (!Player.frozen && !Player.CCed)
            {
                float invItemAnimation = 1f - Player.itemAnimation / (float)Player.itemAnimationMax;
                float projVelocity = Projectile.velocity.Length();
                float projVelocityRotation = Projectile.velocity.ToRotation();

                Projectile.ai[0]++;
                
                Vector2 spinningpoint = Vector2.UnitX.RotatedBy(MathHelper.Pi + invItemAnimation * MathHelper.Pi * 3f) * new Vector2(projVelocity, Projectile.ai[0]);
                Projectile.position += spinningpoint.RotatedBy(projVelocityRotation) + new Vector2(projVelocity + travelSpeed, 0f).RotatedBy(projVelocityRotation);
                Vector2 target = Player.Center + spinningpoint.RotatedBy(projVelocityRotation) + new Vector2(projVelocity + travelSpeed + 40f, 0f).RotatedBy(projVelocityRotation);
                Projectile.rotation = Player.Center.AngleTo(target) + MathHelper.PiOver4 * Player.direction;

                if (Projectile.spriteDirection == -1)
                {
                    Projectile.rotation += MathHelper.Pi;
                }
                if (Player.whoAmI == Main.myPlayer && Player.itemAnimation <= 2)
                {
                    Projectile.Kill();
                    Player.reuseDelay = 2;
                }
            }
        }

        public void PointDownwards()
        {
            if (!Player.frozen && !Player.CCed)
            {
                Projectile.tileCollide = true;

                Player.mount.Dismount(Player);
                Player.RemoveAllGrapplingHooks();

                // Makes the player play the holding animation constantly
                Player.SetDummyItemTime(2);

                Projectile.Center = Player.Center + Vector2.UnitY * 64f;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4 * Player.direction;

                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }

                Projectile.penetrate = 1;

                if (Projectile.spriteDirection == -1)
                {
                    Projectile.rotation += MathHelper.Pi;
                }

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Cyan, Alpha: 100, Scale: 0.75f);
                    dust.position = Projectile.Center - Projectile.velocity / 20f * i;
                    dust.velocity *= 3f;
                    dust.noGravity = true;
                }

                Lighting.AddLight(Projectile.Center, Color.Cyan.ToVector3() * 0.75f);

                Player.WaterCollision(fallThrough: true, ignorePlats: true);
                Player.portalPhysicsFlag = true;
                Player.velocity = Projectile.velocity;
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (CurrentState == WeaponState.Downwards && Projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, (-Projectile.oldVelocity * 0.3f).RotatedByRandom(MathHelper.ToRadians(45f)), ModContent.ProjectileType<HarpyFeather>(), Projectile.damage / 4, Projectile.knockBack, Projectile.owner);
                }
                Player.reuseDelay = 20;
                Projectile.Resize(64, 64);
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                for (int i = 0; i < 30; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Cyan);
                    dust.velocity *= 8f;
                    dust.noGravity = true;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Texture2D fireExtra = TextureAssets.Extra[ExtrasID.FallingStar].Value;
            Color drawColor = Projectile.GetAlpha(lightColor);
            float projRotation = Projectile.rotation;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 drawOrigin = new(Projectile.spriteDirection == 1 ? texture.Width : 0f, 0f);
            Vector2 drawOriginFire = new(fireExtra.Width / 2, 0f);

            if (Player.gravDir == -1f)
            {
                drawOrigin.Y = texture.Height;
                spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                projRotation += MathHelper.PiOver2 * -Projectile.spriteDirection;
                if (Projectile.spriteDirection == 1)
                {
                    drawOrigin *= 0f;
                    projRotation -= MathHelper.Pi;
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
            }

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, drawColor, projRotation, drawOrigin, Projectile.scale, spriteEffects, 0);

            if (CurrentState == WeaponState.Downwards)
            {
                Color fireColor = Color.Blue with { A = 0 } * 0.25f;
                float timer = (float)(Main.timeForVisualEffects / 45.0) % 0.5f / 0.5f;
                float rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

                for (float i = 0f; i < 1f; i += 0.5f)
                {
                    timer = (timer + i) % 1f;
                    float doubleTimer = timer * 2f;
                    if (doubleTimer > 1f)
                    {
                        doubleTimer = 2f - doubleTimer;
                    }
                    Main.EntitySpriteDraw(fireExtra, Projectile.Center - Main.screenPosition, null, fireColor * doubleTimer, rotation, drawOriginFire, 1.25f + timer * 0.25f, SpriteEffects.None);
                    Main.EntitySpriteDraw(fireExtra, Projectile.Center - Main.screenPosition, null, new Color(0, 200, 255, 100) * 0.5f * doubleTimer, rotation, drawOriginFire, Projectile.scale * 1.25f + timer * 0.25f, SpriteEffects.None);
                    Main.EntitySpriteDraw(fireExtra, Projectile.Center - Main.screenPosition, null, Color.White with { A = 0 } * 0.5f * doubleTimer, rotation, drawOriginFire, Projectile.scale * 0.75f + timer * 0.25f, SpriteEffects.None);
                }
            }
            return false;
        }
    }
}
