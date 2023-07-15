using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.ID;
using Terraria.GameContent.Generation;
using static tModPorter.ProgressUpdate;
using TenebrousMod.Items.Placeable;

namespace TenebrousMod.WorldGen
{
    public class WorldSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(t => t.Name.Equals("Shinies"));
            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, new Fossils());
                tasks.Insert(shiniesIndex + 1, new PelagicSeaBiome());
            }
            base.ModifyWorldGenTasks(tasks, ref totalWeight);
        }
    }
    public class Fossils : GenPass
    {
        public Fossils() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Adding Desert Fossils";

            int maxToSpawn = (int)(Main.maxTilesX * Main.maxTilesY * 6E-05);
            for (int i = 0; i < maxToSpawn; i++)
            {
                int x = Terraria.WorldGen.genRand.Next(100, Main.maxTilesX - 200);
                int y = Terraria.WorldGen.genRand.Next((int)Terraria.WorldGen.gen.ToInt(), Main.maxTilesY - 400);



                Terraria.WorldGen.TileRunner(x, y, Terraria.WorldGen.genRand.Next(6, 12), Terraria.WorldGen.genRand.Next(2, 3), TileID.DesertFossil);
            }
            maxToSpawn = Terraria.WorldGen.genRand.Next(100, 250);


        }

    }

    public class PelagicSeaBiome : GenPass
    {
        public PelagicSeaBiome() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Adding the Pelagic Sea";
            int centerX = Main.maxTilesX / 2; // X coordinate of the center of the water patch
            int centerY = Main.maxTilesY / 2; // Y coordinate of the center of the water patch
            int radius = 200; // Radius of the circular water patch

            // Create the circular water patch
            for (int i = centerX - radius; i <= centerX + radius; i++)
            {
                for (int j = centerY - radius; j <= centerY + radius; j++)
                {
                    if (IsInsideCircle(i, j, centerX, centerY, radius))
                    {
                        Main.tile[i, j].LiquidType.CompareTo(0);
                        Main.tile[i, j].LiquidAmount = byte.MaxValue;
                    }
                }
            }

            int grassRadius = radius + 1; // Radius of the circular grass border

            // Create the circular grass border and delete non-water tiles inside
            for (int i = centerX - grassRadius; i <= centerX + grassRadius; i++)
            {
                for (int j = centerY - grassRadius; j <= centerY + grassRadius; j++)
                {
                    if (IsInsideCircle(i, j, centerX, centerY, grassRadius))
                    {
                        if (!IsInsideCircle(i, j, centerX, centerY, radius))
                        {
                            if (Main.tile[i, j].LiquidAmount == 0) // Skip if tile is part of the water patch
                            {
                                Main.tile[i, j].TileType = 0;
                                Main.tile[i, j].ClearTile();
                            }
                        }
                        else // Tile is part of the water patch
                        {
                            Main.tile[i, j].TileType = (ushort)ModContent.TileType<PelagicVeldTile>();
                            Terraria.WorldGen.SquareTileFrame(i, j, true);
                        }
                    }
                }
            }

            // Create the center circle without blocks
            int centerRadius = 30; // Radius of the center empty circle
            for (int i = centerX - centerRadius; i <= centerX + centerRadius; i++)
            {
                for (int j = centerY - centerRadius; j <= centerY + centerRadius; j++)
                {
                    if (IsInsideCircle(i, j, centerX, centerY, centerRadius))
                    {
                        Main.tile[i, j].ClearTile();
                        Main.tile[i, j].TileType = 0;
                    }
                }
            }

            // Create the smaller grass circle within the center circle
            int smallerCircleRadius = 100; // Radius of the smaller grass circle
            for (int i = centerX - smallerCircleRadius; i <= centerX + smallerCircleRadius; i++)
            {
                for (int j = centerY - smallerCircleRadius; j <= centerY + smallerCircleRadius; j++)
                {
                    if (IsInsideCircle(i, j, centerX, centerY, smallerCircleRadius))
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<PelagicVeldTile>();
                        Terraria.WorldGen.SquareTileFrame(i, j, true);
                    }
                }
            }

            // Update the world generation progress
            progress.Value = 1f;
        }

        private bool IsInsideCircle(int x, int y, int centerX, int centerY, int radius)
        {
            int dx = x - centerX;
            int dy = y - centerY;
            return dx * dx + dy * dy <= radius * radius;
        }
    }
}