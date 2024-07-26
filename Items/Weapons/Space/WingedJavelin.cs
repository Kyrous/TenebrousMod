using Terraria.ModLoader;
using System;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ReLogic.Content;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TenebrousMod.Items.Weapons.Space
{
    public class WingedJavelin : ModItem
    {


        public override void SetDefaults()
        {

            Item.shoot = ModContent.ProjectileType<WingedJavelinProj>();
            Item.damage = 10;
            Item.width = 48;
            Item.height = 48;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 15;
            Item.UseSound = SoundID.Item1;
        }




    }

    public class WingedJavelinProj : ModProjectile
    {

        public override void SetDefaults()
        {

            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 300;
            Projectile.localNPCHitCooldown = 10;
            Projectile.penetrate = -1;
            Projectile.localNPCHitCooldown = 10;
            Projectile.usesLocalNPCImmunity = true;
        }

        public ref float AttackCounter => ref Projectile.ai[0];


        private static Asset<Texture2D> winglessTexture;
        private static Asset<Texture2D> wingTexture;
        public override void SetStaticDefaults()
        {
            winglessTexture = Mod.Assets.Request<Texture2D>("Items/Weapons/Space/WingedJavelinProj");
            wingTexture = Mod.Assets.Request<Texture2D>("Items/Weapons/Space/WingedJavelinProj2");
            Main.projFrames[Type] = 4;
        }
        private enum states { normal, spinning, empowered }
        private states state = states.normal;
        private float rot = MathHelper.ToRadians(90);
        private bool hitTarget = false;
        public override bool PreDraw(ref Color lightColor)
        {

            Asset<Texture2D> spark = TextureAssets.Extra[98];
            switch (state)
            {
                case states.normal:
                    Main.EntitySpriteDraw(winglessTexture.Value, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, new Vector2(24, 13), 1f, SpriteEffects.None);
                    break;

                case states.spinning:
                    Main.EntitySpriteDraw(winglessTexture.Value, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, new Vector2(24, 13), 1f, SpriteEffects.None);
                    Main.EntitySpriteDraw(spark.Value, Projectile.Center - Main.screenPosition, null, Color.DeepSkyBlue, 0, spark.Size() / 2f, new Vector2(0.7f, Math.Clamp(MathF.Sin(AttackCounter * 0.1f), 0, 1) * 4f), SpriteEffects.None);
                    Main.EntitySpriteDraw(spark.Value, Projectile.Center - Main.screenPosition, null, Color.DeepSkyBlue, MathHelper.ToRadians(90), spark.Size() / 2f, new Vector2(Math.Clamp(MathF.Sin(AttackCounter * 0.1f), 0, 1), 3f), SpriteEffects.None);

                    break;

                case states.empowered:
                    Main.EntitySpriteDraw(wingTexture.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame, 86, 48), lightColor, Projectile.rotation, new Vector2(43, 23), 1f, SpriteEffects.None);


                    break;
            }

            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            switch (state)
            {
                case states.normal:

                    state = states.spinning;
                    Projectile.velocity.Y = -10f;
                    npcHit = target;

                    break;

                case states.spinning:

                    modifiers.DisableCrit();

                    break;

                case states.empowered:

                    modifiers.ScalingBonusDamage += 2f;
                    hitTarget = true;

                    break;

            }
        }

        private Vector2 targetNPCPos;
        private NPC npcHit;
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter % 10 == 0)
            {
                Projectile.frame += 48;
                if (Projectile.frame > 48 * 3)
                {
                    Projectile.frame = 0;
                }

            }
            switch (state)
            {
                case states.normal:

                    Projectile.penetrate = -1;
                    Projectile.rotation = Projectile.velocity.ToRotation();

                    break;
                case states.spinning:

                    Projectile.velocity *= 0.92f;
                    Projectile.rotation += MathHelper.ToRadians(100);
                    AttackCounter++;



                    if (AttackCounter >= 40)
                    {
                        Projectile.timeLeft += 100;
                        state = states.empowered;
                        Terraria.Audio.SoundEngine.PlaySound(SoundID.Item102, Projectile.Center);
                    }

                    break;
                case states.empowered:

                    if (npcHit.active)
                    {
                        targetNPCPos = npcHit.Center;
                    }

                    if (!hitTarget)
                    {
                        Vector2 targetVel = targetNPCPos - Projectile.Center;
                        targetVel.Normalize();

                        rot = rot.AngleTowards(targetVel.ToRotation(), 0.2f);
                        Projectile.velocity = rot.ToRotationVector2() * 25;
                        Projectile.rotation = rot;

                        Projectile.penetrate = 15;

                    }




                    break;

            }
        }

    }
}