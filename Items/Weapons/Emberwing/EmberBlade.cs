using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TenebrousMod.Items.Bars;
using TenebrousMod.Projectiles;
using TenebrousMod.TenebrousModSystem;
using TenebrousMod.NPCs.Bosses.Emberwing;

namespace TenebrousMod.Items.Weapons.Emberwing
{
    public class EmberBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 85;
            Item.DamageType = DamageClass.Melee;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 30;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<EmberBallFriendly>();
            Item.shootSpeed = 4f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ObscurumBar>(12)
                .AddTile(TileID.Adamantite)
                .Register();
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