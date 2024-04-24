using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Materials
{
    public class StarEssence : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.value = Item.sellPrice(silver: 35);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 9999;
        }
    }
}
