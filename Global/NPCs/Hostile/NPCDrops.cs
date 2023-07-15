using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenebrousMod.Items.Food;
using TenebrousMod.Items.Weapons.Melee;
using TenebrousMod.Items.Weapons.Ranger;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Global.NPCs.Hostile
{
    public class NPCDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.DemonEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DemonEye2)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DemonEyeOwl)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DemonEyeSpaceship)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.CataractEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.CataractEye2)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DialatedEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DialatedEye2)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.WanderingEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.GraniteGolem && NPC.downedMechBossAny)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Trisma>(), 4, 1, 1));
            }
            if (npc.type == NPCID.GraniteGolem)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGreatGranite>(), 4, 1, 1));
            }
            if (npc.type == NPCID.GraniteFlyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGreatGranite>(), 16, 1, 1));
            }

            base.ModifyNPCLoot(npc, npcLoot);
        }
    }
}
