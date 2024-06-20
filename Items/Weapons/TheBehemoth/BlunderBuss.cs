using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.TheBehemoth
{
    public class BlunderBuss : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 76;
            Item.height = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.shoot = ProjectileID.Bullet;
            Item.damage = 20;
            Item.useStyle = 5;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 15;
            Item.ArmorPenetration = 20;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<BlunderbussHeldProjectile>()] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(SoundID.Item38);
            }
            return null;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BlunderbussHeldProjectile>(), 0, 0, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BlunderbussProjectile>(), damage, 0, player.whoAmI);
            for (int i = 0; i < 12; i++)
                Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.5f), type, damage, 0, player.whoAmI);

            return false;

        }
    }
    public class BlunderbussHeldProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.VortexBeater);
        }

        private static Asset<Texture2D> BlunderBussTexture;
        public override void Load()
        {
            BlunderBussTexture = Mod.Assets.Request<Texture2D>("Items/Weapons/TheBehemoth/BlunderBuss");
        }

        private float progress = 0;
        private float progressX = 0;
        private Vector2 scale = new Vector2(1f, 1f);
        public override bool PreAI()
        {
            Player holder = Main.player[Projectile.owner];
            float maxDuration = holder.itemAnimationMax;




            if (Projectile.timeLeft > maxDuration)
            {
                Projectile.timeLeft = (int)maxDuration;

            }

            progress = MathHelper.Lerp(0, MathHelper.PiOver4, MathHelper.Clamp(((float)Projectile.timeLeft - 15) / maxDuration, 0f, 1f));
            progressX = MathHelper.Lerp(25, 0, MathHelper.Clamp(((float)Projectile.timeLeft - 15) / maxDuration, 0f, 1f));
            Projectile.velocity.Normalize();
            holder.heldProj = Projectile.whoAmI;



            Projectile.spriteDirection = holder.direction;
            Projectile.Center = holder.MountedCenter + (Projectile.velocity * progressX);
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + progress;


            }
            else
            {
                Projectile.rotation = Projectile.velocity.ToRotation() - progress;
            }

            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.EntitySpriteDraw(BlunderBussTexture.Value, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, BlunderBussTexture.Value.Size() * 0.5f, Projectile.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
            return false;
        }

    }

    public class BlunderbussProjectile : ModProjectile
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
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;

        }


        public override bool PreDraw(ref Color lightColor)
        {

            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            for (int k = 1; k < Projectile.oldPos.Length; k++)
            {
                Vector2 pos = (Projectile.oldPos[k] - Main.screenPosition);
                Main.EntitySpriteDraw(texture, pos, null, Color.Aquamarine, Projectile.oldRot[k] - MathHelper.ToRadians(90), origin, Projectile.scale, SpriteEffects.None, 0);
            }

            return false;
        }
        private NPC npcHit;
        private float rot = 0;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            npcHit = target;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.ToRadians(90);
            if (npcHit != null)
            {
                if (npcHit.active)
                {
                    Vector2 targetVel = npcHit.Center - Projectile.Center;
                    targetVel.Normalize();

                    rot = rot.AngleTowards(targetVel.ToRotation(), 0.35f);
                    Projectile.velocity = rot.ToRotationVector2() * 25;
                    Projectile.rotation = rot - MathHelper.ToRadians(90);

                }


            }


        }

    }
}