using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenebrousMod.Projectiles;
using TenebrousMod.TenebrousModSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Emberwing
{
    public class TheGreatEmber : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 180;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.width = 62;
            Item.height = 70;
            Item.useTime = 40;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<EmberFlame>();
            Item.shootSpeed = 16f;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return WeaponLighting.LightingOnGround(Item, 1, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
        public override void PostUpdate()
        {
            WeaponLighting.PostLighting(Item, 1);
        }
    }
}
