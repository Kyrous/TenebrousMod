using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.TheBehemoth
{
    public class WizenedGlave : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightRed;
            Item.width = 80;
            Item.height = 96;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 32;
            Item.useStyle = 1;
            Item.useAnimation = 30;
            Item.reuseDelay = 60;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shootSpeed = 15;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<GlaveHeldProjectile>();

        }


    }

    public class GlaveHeldProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Spear;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LastPrism);
            Projectile.width = Projectile.height = 64;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        private static Asset<Texture2D> WizenedGlaveTexture;
        public override void Load()
        {
            WizenedGlaveTexture = Mod.Assets.Request<Texture2D>("Items/Weapons/TheBehemoth/WizenedGlave");
        }

        public override bool PreAI()
        {
            Player holder = Main.player[Projectile.owner];
            Projectile.timeLeft = 2;
            Projectile.velocity.Normalize();
            holder.heldProj = Projectile.whoAmI;
            Projectile.spriteDirection = holder.direction;
            holder.itemAnimation = 2;
            holder.itemTime = 2;
            Projectile.rotation += 0.45f;


            if (holder.channel)
            {
                if (Projectile.Center.X > holder.Center.X)
                {
                    holder.direction = 1;

                }
                else
                {
                    holder.direction = -1;
                }

                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.velocity = (Main.MouseWorld - Projectile.Center) / 20;

                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Projectile.velocity = Projectile.DirectionTo(holder.Center) * 20;
                if (Projectile.Distance(holder.Center) < 50)
                {
                    Projectile.Kill();
                }

            }




            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center - new Vector2(0, target.height), (Vector2.UnitY * 15).RotatedByRandom(1), ModContent.ProjectileType<WizenedGlaveProjectile>(), (int)(damageDone * 0.75f), 0, Projectile.owner);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D spark = TextureAssets.Projectile[ProjectileID.HallowBossRainbowStreak].Value;

            Vector2 bladePos = new Vector2(25, -45);

            Main.EntitySpriteDraw(WizenedGlaveTexture.Value, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, WizenedGlaveTexture.Value.Size() * 0.5f, Projectile.scale, SpriteEffects.None);
            Main.EntitySpriteDraw(spark, (Projectile.Center + bladePos).RotatedBy(Projectile.rotation, Projectile.Center) - Main.screenPosition, null, Color.Aquamarine * 0.5f, 0, spark.Size() * 0.5f, new Vector2(0.5f, 3f), SpriteEffects.None);
            Main.EntitySpriteDraw(spark, (Projectile.Center + bladePos).RotatedBy(Projectile.rotation, Projectile.Center) - Main.screenPosition, null, Color.Aquamarine * 0.5f, MathHelper.ToRadians(90), spark.Size() * 0.5f, new Vector2(0.5f, 2f), SpriteEffects.None);

            for (int i = 1; i < Projectile.oldPos.Length; i++)
            {
                Main.EntitySpriteDraw(spark, (Projectile.Center + bladePos).RotatedBy(Projectile.oldRot[i], Projectile.Center) - Main.screenPosition, null, Color.Aquamarine * 0.5f, (Projectile.Center + bladePos).RotatedBy(Projectile.oldRot[i], Projectile.Center).DirectionTo(Projectile.Center).ToRotation(), spark.Size() * 0.5f, new Vector2(0.5f, 1f), SpriteEffects.None);


            }

            return false;


        }

    }
    public class WizenedGlaveProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.HallowBossRainbowStreak;


        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D spark = TextureAssets.Projectile[Projectile.type].Value;

            Main.EntitySpriteDraw(spark, Projectile.Center - Main.screenPosition, null, Color.Aquamarine, Projectile.rotation, spark.Size() * 0.5f, new Vector2(0.5f, 3f), SpriteEffects.None);

            return false;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SuperStarSlash);
            Projectile.width = Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Default;
            Projectile.timeLeft = 10;
            Projectile.ArmorPenetration = 15;
            Projectile.penetrate = 5;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.ToRadians(90);
        }

    }
}