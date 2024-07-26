using TenebrousMod.Items.Accessories;
using TenebrousMod.Items.Bars;
using TenebrousMod.Items.Placeable;
using TenebrousMod.Items.Placeable.Furniture.Relic;
using TenebrousMod.Items.Placeable.Furniture.Trophy;
using TenebrousMod.Items.Weapons.Emberwing;
using TenebrousMod.Items.Weapons.Icerus;
using TenebrousMod.Items.Weapons.TheBehemoth;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.TreasureBags
{
    public class IcerusTreasureBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.rare = ItemRarityID.Pink;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberwingTrophyI>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DominicsLostBlade>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DominicsHorizon>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<IcerusMusicBox>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFive>(), 400));

        }

    }
}
