using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.WOF
{
    public class PhotonBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            Item.color = Color.White;
        }

        public override void SetDefaults()
        {
            Item.damage = 33;
            Item.rare = ItemRarityID.LightPurple;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 50;
            Item.height = 46;
            Item.knockBack = 2;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<PhotonicSerpent>();
            Item.shootSpeed = 13.5f;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 3);

        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Lighting.AddLight(hitbox.Center.ToVector2(), Color.LightPink.ToVector3() * 1);
            if (Main.rand.NextBool(1, 5))
            {
                Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.GemDiamond, 0, 0, 120, default, 0.75f);
            }
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Lighting.AddLight(Item.Center, Color.LightPink.ToVector3() * 1);
            Texture2D texture = TextureAssets.Item[Item.type].Value;
            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
            {
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            }
            else
            {
                frame = texture.Frame();
            }

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.35f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(255, 175, 247, 40), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.30f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 2f).RotatedBy(radians) * time, frame, new Color(255, 155, 241, 55), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * 1);
        }

    }
    public class PhotonicSerpent : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.damage = 15;
            Projectile.knockBack = 1;
            Projectile.aiStyle = ProjAIStyleID.LunarProjectile;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, Main.rand.Next(30, 60));
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.LightPink.ToVector3() * 1);
            if (Projectile.frame <= 2)
            {
                Projectile.Center += new Vector2(4, 0).RotatedBy(Projectile.velocity.ToRotation());
            }
            if (Projectile.frame > 2)
            {
                Projectile.Center += new Vector2(-4, 0).RotatedBy(Projectile.velocity.ToRotation());
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[ProjectileID.PiercingStarlight].Value;
            Vector2 drawOrigin = new Vector2(24 * 0.5f, 24 * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Color.LightPink * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos - new Vector2(-12, 4.8f).RotatedBy(Projectile.rotation + MathHelper.ToRadians(90)), null, color, Projectile.rotation + MathHelper.ToRadians(90), drawOrigin, new Vector2(1.2f, 0.2f), SpriteEffects.FlipVertically, 0);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position, 24, 24, DustID.GemDiamond, 0, 0, 120, default, 0.75f);
                Dust.NewDust(Projectile.position + new Vector2(24, 0).RotatedBy(Projectile.rotation - MathHelper.ToRadians(90)), 24, 24, DustID.GemDiamond, 0, 0, 120, default, 0.75f);
                Dust.NewDust(Projectile.position + new Vector2(48, 0).RotatedBy(Projectile.rotation - MathHelper.ToRadians(90)), 24, 24, DustID.GemDiamond, 0, 0, 120, default, 0.75f);
            }

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}