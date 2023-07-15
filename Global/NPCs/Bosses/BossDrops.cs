using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenebrousMod.Items.Food;
using TenebrousMod.Items.Spirit;
using TenebrousMod.Items.Weapons.Melee;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Global.NPCs.Bosses
{
    public class BossDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WallOfFleshDefeat>(), 1, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PhotonBlade>(), 2, 1, 1));
            }

            base.ModifyNPCLoot(npc, npcLoot);
        }
    }
}
