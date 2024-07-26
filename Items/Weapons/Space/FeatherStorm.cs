using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Space
{
    public class FeatherStorm : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToMagicWeapon(ModContent.ProjectileType<FeatherStormProjectile>(), 5, 5, true);
            Item.width = 42;
            Item.height = 44;
            Item.damage = 9;
            Item.useAnimation = 10;
            Item.ArmorPenetration = 10;
            Item.mana = 4;
            Item.UseSound = SoundID.Item39;
        }


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += new Vector2(Main.rand.Next(-16, 17), Main.rand.Next(-16, 17));
        }

    }

    public class FeatherStormProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.HarpyFeather;
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
            Projectile.extraUpdates = 7;
        }

        public override void PostDraw(Color lightColor)

        {
            Asset<Texture2D> spark = TextureAssets.Extra[98];

            Main.EntitySpriteDraw(spark.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 72, 72), Color.DeepSkyBlue, Projectile.rotation, spark.Size() * 0.5f, new Vector2(0.75f, 3), SpriteEffects.None);

        }


        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

        }


    }
}