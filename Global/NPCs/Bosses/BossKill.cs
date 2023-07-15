using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TenebrousMod.Global.NPCs.Bosses
{
    public class BossKill : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if(npc.type == NPCID.WallofFlesh)
            {
                NetMessage message = new NetMessage();
                if (Main.netMode != NetmodeID.Server)
                {
                    string text = Language.GetTextValue("I'LL GET YOU FOR THIS!!!", Lang.GetNPCNameValue(npc.type), message);
                    Main.NewText(text, 150, 250, 150);
                }
            }
            base.OnKill(npc);
        }
    }
}
