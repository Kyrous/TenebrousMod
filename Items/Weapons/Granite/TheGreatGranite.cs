using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.ModLoader;
namespace TenebrousMod.Items.Weapons.Granite
{
    public class TheGreatGranite : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.rare = ItemRarityID.Orange;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 7;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = false;
            Item.width = 48;
            Item.height = 54;
            Item.knockBack = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GraniteSlash>();
            Item.shootSpeed = 18f;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Lighting.AddLight(hitbox.Center.ToVector2(), 0.34f, 0.45f, 0.81f);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.itemAnimation <= Item.useTime * 3)
            {
                damage = (int)(damage / 3.5f);
                position -= new Vector2(Main.rand.Next(10, 20) * -player.direction, 125);
                velocity = position.DirectionTo(player.Center + new Vector2(player.direction * Main.rand.Next(80, 120), 0)) * 18;
                Projectile.NewProjectile(player.GetSource_FromThis(), position, velocity, type, damage, knockback, Main.myPlayer);
                SoundEngine.SoundPlayer.Play(SoundID.DD2_LightningAuraZap, position);

            }
            else
            {
                player.oldDirection = player.direction;
            }
            return false;
        }
    }

    public class GraniteSlash : ModProjectile
    {
        public sealed override string Texture => "TenebrousMod/Assets/Textures/EmptyPixel";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.alpha = 1;
            Projectile.ArmorPenetration = 10;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 Direction = new Vector2(Main.rand.NextFloat(0, 1), Main.rand.NextFloat(0, 1));
            Direction.Normalize();
            FadingParticle j = new FadingParticle();
            j.SetBasicInfo(TextureAssets.Projectile[ProjectileID.PiercingStarlight], new Rectangle?(), Direction * 3, target.Center - (Direction * 30));
            j.SetTypeInfo(10);
            j.FadeInNormalizedTime = 0.1f;
            j.FadeOutNormalizedTime = 0.1f;
            j.ColorTint = new Color(0.42f, 0.53f, 1.0f);
            j.Scale = new Vector2(0.8f, 0.15f);
            j.Rotation = Direction.ToRotation();
            j.RotationAcceleration = 0.01f;
            j.AccelerationPerFrame = Direction;
            Main.ParticleSystem_World_OverPlayers.Add(j);
            target.AddBuff(BuffID.Electrified, 40);
            target.immune[Projectile.owner] = 5;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(ProjectileID.PiercingStarlight);
            Texture2D texture = TextureAssets.Projectile[ProjectileID.PiercingStarlight].Value;
            Rectangle frame = texture.Frame();
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Color j = new Color(87 - (Projectile.alpha / 24), 117 - (Projectile.alpha / 24), 208 - (Projectile.alpha / 24), Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 2.0f;
                Rectangle scaledk = new Rectangle((int)Projectile.oldPos[k].X - (int)Main.screenPosition.X, (int)Projectile.oldPos[k].Y - (int)Main.screenPosition.Y, 30, 10);
                Rectangle scaledk2 = new Rectangle((int)Projectile.oldPos[k].X - (int)Main.screenPosition.X, (int)Projectile.oldPos[k].Y - (int)Main.screenPosition.Y, 15, 10);
                Main.EntitySpriteDraw(texture, Projectile.oldPos[k] + new Vector2(7.5f, 7.5f) - Main.screenPosition - new Vector2(30, 10).RotatedBy(Projectile.rotation), frame, j, Projectile.rotation, Projectile.getRect().Size(), scaledk.Size() / 30, SpriteEffects.None);
                Main.EntitySpriteDraw(texture, Projectile.oldPos[k] + new Vector2(7.5f, 7.5f) - Main.screenPosition - new Vector2(14f, -10).RotatedBy(Projectile.rotation + MathHelper.ToRadians(90)), frame, j, Projectile.rotation + MathHelper.ToRadians(90), Projectile.getRect().Size(), scaledk2.Size() / 30, SpriteEffects.None);
            }
            return false;

        }
        public override void AI()
        {
            if (Projectile.timeLeft == 30)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
            Projectile.ai[2] -= 0.5f;
            if (Projectile.timeLeft <= 15)
            {
                if (Projectile.ai[1] == 1)
                {
                    Projectile.velocity -= Projectile.rotation.ToRotationVector2() / 1.5f;
                }
                Projectile.velocity /= 1.2f;
                Projectile.alpha += 15;
            }
            else
            {
                if (Main.rand.NextBool(1, 4))
                {
                    int j1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, -Projectile.velocity.X / 7, -Projectile.velocity.Y / 7);
                    int j2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, -Projectile.velocity.X / 9, -Projectile.velocity.Y / 9);
                    int j3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, -Projectile.velocity.X / 12, -Projectile.velocity.Y / 12);
                    Main.dust[j1].noGravity = true;
                    Main.dust[j2].noGravity = true;
                    Main.dust[j3].noGravity = true;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft == 30 || Projectile.ai[2] >= -0.5)
            {
                Projectile.velocity = oldVelocity;
                Projectile.ai[2] = 1;

            }
            else
            {
                if (Projectile.ai[1] == 0 || Projectile.ai[2] < 0)
                {
                    Projectile.ai[1] = 1;
                    if (Projectile.timeLeft > 15)
                    {
                        Projectile.timeLeft = 15;
                    }
                    Projectile.tileCollide = false;
                    Projectile.alpha += 15;
                }
                Projectile.velocity = oldVelocity / 2;
                int j1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, -oldVelocity.X / 1.5f, -oldVelocity.Y / 1.5f);
                int j2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, -oldVelocity.X, -oldVelocity.Y);
                int j3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, -oldVelocity.X / 0.7f, -oldVelocity.Y / 0.7f);
                Main.dust[j1].noGravity = true;
                Main.dust[j2].noGravity = true;
                Main.dust[j3].noGravity = true;
            }


            return false;
        }
    }
}