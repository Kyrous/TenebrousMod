using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenebrousMod.TenebrousModSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Global.NPCs.Bosses
{
    public class BossStats : GlobalNPC
    {
        public override void SetDefaults(NPC entity)
        {
            Config config = new Config();
            NPC nPC = new NPC();
            if (nPC.type == NPCID.EyeofCthulhu)
            {
                if(config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.KingSlime)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.EaterofWorldsHead)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.EaterofWorldsBody)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.EaterofWorldsTail)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.BrainofCthulhu)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.Creeper)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.ServantofCthulhu)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.Deerclops)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.QueenBee)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.WallofFleshEye)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.Spazmatism)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.Retinazer)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.SkeletronHand)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.SkeletronHead)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.SkeletronPrime)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.PrimeCannon)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.PrimeLaser)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.PrimeSaw)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.PrimeVice)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.TheDestroyer)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.TheDestroyerBody)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.TheDestroyerTail)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.LunarTowerVortex)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.LunarTowerStardust)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.LunarTowerSolar)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.LunarTowerNebula)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.CultistBoss)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.Golem)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.GolemFistLeft)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.GolemFistRight)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.GolemHead)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.GolemHeadFree)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.MoonLordCore)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.MoonLordHead)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.MoonLordLeechBlob)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.MoonLordHand)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.QueenSlimeBoss)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.QueenSlimeMinionBlue)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.QueenSlimeMinionPink)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.QueenSlimeMinionPurple)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            if (nPC.type == NPCID.HallowBoss)
            {
                if (config.SupremacyMode == true)
                {
                    nPC.lifeMax *= 2;
                }
                nPC.lifeMax *= config.BossHealthMultiplier;
            }
            base.SetDefaults(entity);
        }
    }
}
