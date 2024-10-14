using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;
using SubworldLibrary;

/*
 * This is where Layer1's world gen will be.
*/

namespace TenebrousMod.TWorldGen.Subworlds
{
    public class Layer1GenPass : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public Layer1GenPass() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating terrain"; // Sets the text displayed for this pass
            Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
            Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY)); // Controls the progress bar, should only be set between 0f and 1f
                    Tile tile = Main.tile[i, j];
                    tile.HasTile = true;
                    tile.TileType = TileID.Dirt;
                }
            }
        }
    }
}
