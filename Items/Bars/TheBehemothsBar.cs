using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.GameContent;

namespace TenebrousMod.Items.Bars
{

    public class TheBehemothsBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(silver: 60);
            Item.maxStack = 9999;
        }
    }
}