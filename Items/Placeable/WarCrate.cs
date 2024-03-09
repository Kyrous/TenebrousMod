using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TenebrousMod.Items.Materials;
using Terraria.ObjectData;
using Terraria.GameContent.ItemDropRules;

namespace TenebrousMod.Items.Placeable
{
    public class WarCrate : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsFishingCrate[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.GoldenCrate);
            Item.width = 34;
            Item.height = 34;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 25);
            Item.createTile = ModContent.TileType<WarCrateTile>();
        }
        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Crates;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 1, 1, 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GraniteShard>(), 1, 5, 11));
            itemLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal, 4, 1, 2));
            itemLoot.Add(ItemDropRule.Common(ItemID.HealingPotion, 1, 1, 5));
            itemLoot.Add(ItemDropRule.Common(ItemID.ManaPotion, 1, 1, 5));
        }
    }
        public class WarCrateTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.addTile(Type);
        }
    }
}