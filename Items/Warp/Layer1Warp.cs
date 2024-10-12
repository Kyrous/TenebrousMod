using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TenebrousMod.TWorldGen.Subworlds;

namespace TenebrousMod.Items.Warp
{
    public class Layer1Warp : ModItem
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 0, 0, 0);

            Item.useStyle = 1;
            Item.consumable = false;
        }

        public override void OnConsumeItem(Player player)
        {
            base.OnConsumeItem(player);
            SubworldSystem.Enter<Layer1>();
        }
    }
}
