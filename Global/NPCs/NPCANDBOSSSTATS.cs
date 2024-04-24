using System;
using TenebrousMod.BossBars;
using Terraria;
using Terraria.ModLoader;

namespace TenebrousMod.Global
{
    public class Npcbossstats : GlobalNPC
    {
        // TODO: ?
        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc)
        {
            npc.lifeMax = (int)Math.Round(1.1f * npc.lifeMax);
        }
    }
}