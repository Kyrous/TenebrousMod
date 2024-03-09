using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TenebrousMod.Items.Bars;
using Microsoft.Xna.Framework.Graphics;
using TenebrousMod.TenebrousModSystem;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace TenebrousMod.Items.Weapons.Ranger
{
    public class EmberArrowProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.FireArrow);
            Projectile.width = 18;
            Projectile.height = 42;
            AIType = ProjectileID.FireArrow;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 1800;
            Projectile.penetrate = 1;
            Projectile.damage = 16;
            Projectile.knockBack = 3;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 600);
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.FlameBurst, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.FlameBurst, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position); // Play the arrow collision sound
            Projectile.Kill(); // Destroy the projectile after colliding with tiles
            return false; // Return false to stop the projectile from moving
        }
    }
    public class EmberArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.knockBack = 3;
            Item.width = 18;
            Item.height = 42;
            Item.maxStack = 9999;
            Item.FitsAmmoSlot();
            Item.ammo = AmmoID.Arrow;
            Item.rare = ItemRarityID.LightPurple;
            Item.consumable = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<ObscurumBar>(), 1)
            .AddTile(TileID.AdamantiteForge)
            .Register();
        }
        WeaponLighting weaponLighting = new WeaponLighting();

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return weaponLighting.LightingOnGround(Item, 1, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
        public override void PostUpdate()
        {
            weaponLighting.PostLighting(Item, 1);
        }
    }
}
