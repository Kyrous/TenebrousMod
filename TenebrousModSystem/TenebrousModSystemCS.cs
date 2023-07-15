using ReLogic.OS.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TenebrousMod.TenebrousModSystem
{
    public class TenebrousModSystemCS : ModSystem
    {
        public override void OnWorldLoad()
        {
            NetMessage netMessage = new NetMessage();
            string text = Language.GetTextValue("Welcome to the Tenebrous Mod World", Lang.menu.ToString(), netMessage);
            Main.NewText(text, 150, 250, 150);
            base.OnWorldLoad();
        }
    }
}
