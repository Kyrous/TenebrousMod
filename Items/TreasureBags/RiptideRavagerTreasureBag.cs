using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenebrousMod.Items.Accessories;
using TenebrousMod.Items.Bars;
using TenebrousMod.Items.Placeable.Furniture;
using TenebrousMod.Items.Weapons.Melee;
using TenebrousMod.Items.Weapons.Ranger;
using TenebrousMod.Items.Weapons.Summoner;
using TenebrousMod.TenebrousModSystem;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.TreasureBags
{
    public class RiptideRavagerTreasureBag : ModItem
    { 
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 32;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.rare = ItemRarityID.LightPurple;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiptideBow>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiptideStaff>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiptideRavagerTrophyI>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Riptide>(), 1, 1, 1));
            if (Main.masterMode)
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiptideRavagerRelicI>(), 1));
            base.ModifyItemLoot(itemLoot);
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