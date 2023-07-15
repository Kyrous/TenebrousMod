using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Spirit
{
    public class WallOfFleshDefeat : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Purple;
            Item.width = 20; 
            Item.height = 20;
        }
    }
}
