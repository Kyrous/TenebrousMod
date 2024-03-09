using System;
using TenebrousMod.BossBars;
using Terraria;
using Terraria.ModLoader;

namespace TenebrousMod.Global
{
    public class Npcbossstats : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc)
        {
                npc.lifeMax = (int)Math.Round(1.2f * npc.lifeMax);
            if(npc.boss)
            {
                npc.BossBar = ModContent.GetInstance<TenebrousModBossBar>();
            }
        }
    }
}