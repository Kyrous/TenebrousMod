using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace TenebrousMod.TenebrousModSystem
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        [Header("GeneralGameplay")] 
        [Label("SupremacyMode")]
        [Tooltip("A whole nother difficulty for experts!")] 
        [DefaultValue(false)] 
        public bool SupremacyMode;
        [Label("BossHealthMultiplier")] 
        [Tooltip("Multiply the bosses health!")]
        [Increment(1)]
        [Range(1, 10)]
        [DefaultValue(1)]
        public int BossHealthMultiplier;
    }
}
