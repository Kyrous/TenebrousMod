using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.TreasureBags
{
    public class StarterBag : ModItem
    { 
        public override void SetDefaults()
        {
            Item item = new Item();
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.consumable = true;
            item.rare = ItemRarityID.LightPurple;
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemID.LesserHealingPotion, 1, 3, 3));
            itemLoot.Add(ItemDropRule.Common(ItemID.LesserManaPotion, 1, 3, 3));
            itemLoot.Add(ItemDropRule.Common(ItemID.IronskinPotion, 1, 3, 3));
            itemLoot.Add(ItemDropRule.Common(ItemID.Chest, 1, 3, 3));
            itemLoot.Add(ItemDropRule.Common(ItemID.Apple, 1, 10, 10));
            itemLoot.Add(ItemDropRule.Common(ItemID.IronBroadsword, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Wood, 1, 100, 100));
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodHelmet, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodBreastplate, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodGreaves, 1, 1, 1));
            base.ModifyItemLoot(itemLoot);
        }
    }
}