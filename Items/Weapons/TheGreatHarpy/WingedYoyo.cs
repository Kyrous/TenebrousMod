using Terraria.ModLoader;
using System;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TenebrousMod.Items.Weapons.TheGreatHarpy
{
    public class WingedYoyo : ModItem
    {


        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodYoyo);
            Item.shoot = ModContent.ProjectileType<WingedYoyoProjectile>();
            Item.damage = 10;
            Item.width = 34;
            Item.height = 38;



        }




    }

    public class WingedYoyoProjectile : ModProjectile
    {


        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 450f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1;
        }
        public override void SetDefaults()
        {

            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = -1;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WingedYoyoFeathers>(), 15, 1f, Projectile.owner, Projectile.whoAmI);
        }

        public override void PostAI()
        {

        }

    }

    public class WingedYoyoFeathers : ModProjectile
    {

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.HarpyFeather;

        public override void SetDefaults()
        {

            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Default;
            Projectile.damage = 15;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 240;
        }

        public override void AI()
        {

            Projectile.velocity = Projectile.Center.DirectionTo(new Vector2(MathF.Sin(Projectile.timeLeft * 0.4f) * 300, MathF.Cos(Projectile.timeLeft * 0.4f) * 300) + Main.projectile[(int)Projectile.ai[0]].Center) * 25;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

            if (!Main.projectile[(int)Projectile.ai[0]].active)
            {

                Projectile.penetrate = 0;

            }
        }
    }
}