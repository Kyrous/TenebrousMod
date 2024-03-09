using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenebrousMod.Items.Accessories;
using TenebrousMod.Items.Bars;
using TenebrousMod.Items.Placeable.Furniture;
using TenebrousMod.Items.Weapons.Melee;
using TenebrousMod.TenebrousModSystem;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.TreasureBags
{
    public class EmberwingTreasureBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberwingTrophyI>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberWarAxe>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberScythe>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGreatEmber>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObscurumBar>(), 1, 28, 40));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObscurumGlove>(), 1));
            itemLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<EmberwingRelicI>()));
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