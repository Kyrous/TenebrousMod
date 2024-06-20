using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TenebrousMod.TWorldGen
{
    public class UndergroundBuildingsGen : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int BuildingsIndex = tasks.FindIndex(tasks => tasks.Name.Equals("Buildings"));
            if (BuildingsIndex != -1)
            {
                tasks.Insert(BuildingsIndex + 1, new PassLegacy("UndergroundBuildings", UndergroundBuildings));
            }
        }

        public void UndergroundBuildings(GenerationProgress progress, GameConfiguration configuration)
        {
            int worldWidth = Main.maxTilesX;
            int worldHeight = Main.maxTilesY;

            int x = worldWidth / 2;
            int y = worldHeight / 2;

            int houseWidth = 80; // 10 times bigger
            int houseHeight = 48; // 8 times bigger
            int interiorWidth = 64; // 8 times bigger
            int interiorHeight = 32; // 8 times bigger

            int topLeftX = x - (houseWidth / 2);
            int topLeftY = y - (houseHeight / 2);

            // Place walls for the house
            for (int i = 0; i < houseWidth; i++)
            {
                for (int j = 0; j < houseHeight; j++)
                {
                    if (i == 0 || i == houseWidth - 1 || j == 0 || j == houseHeight - 1)
                    {
                        WorldGen.PlaceTile(topLeftX + i, topLeftY + j, TileID.WoodBlock);
                    }
                }
            }

            // Clear interior space
            for (int i = 1; i < interiorWidth - 1; i++)
            {
                for (int j = 1; j < interiorHeight - 1; j++)
                {
                    WorldGen.KillTile(topLeftX + i, topLeftY + j);
                }
            }

            // Place chest in the center of the bottom row
            int chestX = x;
            int chestY = topLeftY + houseHeight - 1;
            int chestIndex = WorldGen.PlaceChest(chestX, chestY, TileID.Containers);
            if (chestIndex != -1)
            {
                Main.chest[chestIndex].item[1].SetDefaults(ItemID.GoldCoin);
            }
            progress.Value = 1f;
        }
    }
}