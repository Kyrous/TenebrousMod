using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TenebrousMod.Items.Summons;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TenebrousMod.TenebrousModSystem
{
    public class BossChecklistIntegration : ModSystem
    {
        Mod bossChecklistMod;
        public override void Load()
        {
        }
        public override void PostSetupContent()
        {
            try
            {
                ModLoader.TryGetMod("BossChecklist", out bossChecklistMod);

                if (bossChecklistMod != null)
                {
                    ModLoader.GetMod("BossChecklist");

                    //RegisterBoss<DesertBehemoth>(7.1f, () => false, new List<int> { 0 }, () => Mod.GetLocalization("Mods.TenebrousMod.BossChecklistIntegration.TheBehemoth.TheBehemothSpawnInfo"));
                    //RegisterBoss<IcerusBossHead>(7.2f, () => false, new List<int> { ModContent.ItemType<FrozenMoral>() }, () => Mod.GetLocalization("Mods.TenebrousMod.BossChecklistIntegration.Icerus.IcerusSpawnInfo"));
                    //RegisterBoss<Emberwing>(12.1f, () => false, new List<int> { ModContent.ItemType<EmberPact>() }, () => Mod.GetLocalization("Mods.TenebrousMod.BossChecklistIntegration.Emberwing.EmberwingSpawnInfo"));
                    return;
                }
            }
            finally
            {
                // the instance is not really required for anything else 
                // so it could just be set to null after all bosses have been registered
                // this also prevents this mod from keeping boss checklist alive in case this mod doesn't unload
                bossChecklistMod = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <inheritdoc cref="RegisterBoss"/>
        void RegisterBoss<T>(float progression, Func<bool> downed, List<int> spawnItems = null, Func<LocalizedText> spawnInfo = null, List<int> collectibles = null, Func<bool> isAvailable = null, string internalName = null) where T : ModNPC
        {
            if (bossChecklistMod == null)
            {
                return;
            }

            RegisterBoss(ModContent.NPCType<T>(), progression, downed, spawnItems, spawnInfo, isAvailable, collectibles, ModContent.GetInstance<T>().Name);
        }

        void RegisterBoss(int type,
            float progression,
            Func<bool> downed,
            List<int> spawnItems = null,
            Func<LocalizedText> spawnInfo = null,
            Func<bool> isAvailable = null,
            List<int> collectibles = null,
            string internalName = null)
        {
            if (bossChecklistMod == null)
            {
                return;
            }

            if (internalName == null)
            {
                if (NPCLoader.GetNPC(type)?.Name is not { } name)
                {
                    throw new InvalidOperationException("Possible attempt to register a vanilla npc on boss checklist? the Name or the ModNPC are null");
                }
                internalName = name;
            }
            Dictionary<string, object> extra = new();
            if (spawnItems != null)
                extra["spawnItems"] = spawnItems;
            if (collectibles != null)
                extra["collectibles"] = collectibles;
            if (isAvailable != null)
                extra["availability"] = isAvailable;

            bossChecklistMod.Call("LogBoss", Mod, internalName, progression, downed, new List<int>() { type }, extra);
        }
    }
}