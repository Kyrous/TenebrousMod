using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TenebrousMod.TWorldGen.Subworlds;
using SubworldLibrary;
using Steamworks;

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

            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool? UseItem(Player player)
        {
            SubworldSystem.Enter<Layer1>();
            return base.UseItem(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            SubworldSystem.Exit();
            return base.AltFunctionUse(player);
        }
    }
}
