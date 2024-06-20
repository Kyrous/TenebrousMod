using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TenebrousMod.Projectiles;
using TenebrousMod.TenebrousModSystem;

namespace TenebrousMod.Items.Weapons.Emberwing
{
    public class EmberScythe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 105;
            Item.DamageType = DamageClass.Melee;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 40;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 6);
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
