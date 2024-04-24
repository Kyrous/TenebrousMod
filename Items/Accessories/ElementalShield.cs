using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace TenebrousMod.Items.Accessories
{
    public class ElementalShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 50;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife < 25%player.statLifeMax2)
                Item.defense = 8;
            else
                Item.defense = 0;
        }
    }
}
