using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenebrousMod.TenebrousModSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Emberwing
{
    public class EmberWarAxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 120;
            Item.DamageType = DamageClass.Melee;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 2f;
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
