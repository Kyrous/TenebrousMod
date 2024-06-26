using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;

namespace TenebrousMod.Items.Weapons.Icerus
{
    public class DominicsHorizon : ModItem
    {

        public override void SetDefaults()
        {

            Item.DefaultToMagicWeapon(ModContent.ProjectileType<HorizonProj>(), 20, 15, true);
            Item.mana = 25;
            Item.damage = 64;
            Item.useTime = 10;

            Item.channel = true;
        }



        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            for (float i = 0; i < 3; i++)
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, MathHelper.Lerp(MathHelper.TwoPi, 0f, i / 3f));
            return false;
        }

    }


    public class horizonLingerProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.CoolWhipProj;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 128;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 160;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> texture = TextureAssets.Extra[98];

            Asset<Texture2D> explosionTexture = TextureAssets.Projectile[ProjectileID.DD2ExplosiveTrapT2Explosion];
            Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.CornflowerBlue, Projectile.rotation, texture.Size() / 2f, 1f, SpriteEffects.None);
            Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.CornflowerBlue, -Projectile.rotation, texture.Size() / 2f, 1f, SpriteEffects.None);
            Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.White * MathHelper.Lerp(1, 0, (float)Projectile.timeLeft / 120f), 0, texture.Size() / 2f, MathHelper.Lerp(1, 2, (float)Projectile.timeLeft / 120f), SpriteEffects.None);

            if (Projectile.timeLeft < 5)
            {

                Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.White, MathHelper.ToRadians(45), texture.Size() / 2f, new Vector2(0.5f, 2f), SpriteEffects.None);
                Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.White, MathHelper.ToRadians(90), texture.Size() / 2f, new Vector2(0.5f, 2f), SpriteEffects.None);
                Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.White, MathHelper.ToRadians(135), texture.Size() / 2f, new Vector2(0.5f, 2f), SpriteEffects.None);
                Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.White, 0, texture.Size() / 2f, 2f, SpriteEffects.None);

            }


            return false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (!Main.dedServ)
            {

                for (int i = 0; i < 50; i++)
                {

                    Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2CircularEdge(30, 30), DustID.IceTorch);

                }


            }
        }



        public override void AI()
        {

            Projectile.rotation += 0.1f;
            if (Projectile.timeLeft == 2)
            {

                Projectile.friendly = true;
                SoundEngine.PlaySound(SoundID.Item27, Projectile.Center); ;

            }

        }


    }
    public class HorizonProj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.CoolWhipProj;


        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 48;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 120;


        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(ProjectileID.CoolWhipProj);
            Asset<Texture2D> texture = TextureAssets.Projectile[ProjectileID.CoolWhipProj];

            for (int i = 0; i < Projectile.oldPos.Length; i++)
                Main.EntitySpriteDraw(texture.Value, Projectile.oldPos[i] - Main.screenPosition + texture.Size() / 2f, null, Color.White * MathHelper.Lerp(1, 0, (float)i / (float)Projectile.oldPos.Length), Projectile.rotation, texture.Size() / 2f, MathHelper.Lerp(1, 0, (float)i / (float)Projectile.oldPos.Length), SpriteEffects.None);

            Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, texture.Size() / 2f, 1f, SpriteEffects.None);



            return false;
        }



        public override bool PreAI()
        {

            Projectile.ai[0] += 0.16f;




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

                    Projectile.position = (new Vector2(MathF.Sin(Projectile.ai[0]) * 125, MathF.Cos(Projectile.ai[0]) * 125) + Main.MouseWorld);
                    Projectile.rotation = Projectile.Center.DirectionTo(Main.MouseWorld).ToRotation() + MathHelper.ToRadians(90);

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


        public override void OnKill(int timeLeft)
        {

        }
    }

    public class miniIceProj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.NorthPoleSnowflake;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 48;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 120;


        }


        public override bool PreDraw(ref Color lightColor)
        {

            Main.instance.LoadProjectile(ProjectileID.CoolWhipProj);
            Asset<Texture2D> texture = TextureAssets.Projectile[ProjectileID.CoolWhipProj];



            for (int i = 0; i < Projectile.oldPos.Length; i++)
                Main.EntitySpriteDraw(texture.Value, Projectile.oldPos[i] - Main.screenPosition + texture.Size() / 2f, null, Color.White, Projectile.rotation, texture.Size() / 2f, MathHelper.Lerp(0.25f, 0, (float)i / (float)Projectile.oldPos.Length), SpriteEffects.None);

            Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, texture.Size() / 2f, 0.25f, SpriteEffects.None);



            return false;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.4f;
            if (Projectile.timeLeft % 10 == 0 && Main.myPlayer == Projectile.owner)
            {

                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<horizonLingerProj>(), Projectile.damage, 0, Projectile.owner);


            }
        }


    }
}