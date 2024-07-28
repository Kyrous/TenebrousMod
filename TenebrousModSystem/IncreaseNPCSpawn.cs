using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TenebrousMod.TenebrousModSystem
{
    public class IncreaseNPCSpawn : ModSystem
    {
        public override void OnWorldLoad()
        {
            //TheWorldIsOver is activated when moonlord has ben defeated in that world you are using.
            if(TenebrousMod.TheWorldIsOver)
            {
                //Put increase NPC spawn code here
            }
        }
    }
}
