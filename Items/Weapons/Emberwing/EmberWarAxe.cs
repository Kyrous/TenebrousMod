using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenebrousMod.TenebrousModSystem;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Emberwing
{
    public class EmberWarAxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 140; // Increased damage for post-Plantera
            Item.DamageType = DamageClass.Melee;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 25; // Reduced use time for faster attacks
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10; // Increased knockback
            Item.value = Item.sellPrice(gold: 8); // Increased value
            Item.rare = ItemRarityID.LightPurple; // Updated rarity for post-Plantera
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 2.5f; // Slightly increased scale
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
