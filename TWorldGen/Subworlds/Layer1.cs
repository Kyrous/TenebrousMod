using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.WorldBuilding;
using Terraria;

namespace TenebrousMod.TWorldGen.Subworlds
{
    public class Layer1 : Subworld
    {
        public override int Width => 1000;
        public override int Height => 1000;

        public override bool ShouldSave => false;
        public override bool NoPlayerSaving => true;

        public override List<GenPass> Tasks => new List<GenPass>()
    {
        new Layer1GenPass()
    };

        // Sets the time to the middle of the day whenever the subworld loads
        public override void OnLoad()
        {
            Main.dayTime = true;
            Main.time = 27000;
        }
    }
}
