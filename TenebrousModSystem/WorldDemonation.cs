using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.TenebrousModSystem
{
    public class DemonationWaterFallStyle : ModWaterfallStyle
    {
        // Makes the waterfall provide light
        // Learn how to make a waterfall: https://terraria.wiki.gg/wiki/Waterfall
        public override void AddLight(int i, int j) =>
            Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), Color.Red.ToVector3() * 2f);
    }
    public class WorldDemonation : ModSystem
    {
        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            if (NPC.downedMoonlord)
            {
                TenebrousMod.TheWorldIsOver = true;
                tileColor = Color.Red; backgroundColor = Color.DarkRed;
            }

        }
    }

    public class DemonationWaterFall : ModWaterStyle
    {
        private Asset<Texture2D> rainTexture;
        public override void Load()
        {
            if (TenebrousMod.TheWorldIsOver == true)
            {
                rainTexture = Mod.Assets.Request<Texture2D>("TenebrousModSystem/DemonationRain");
            }
            rainTexture = null;
        }

        public override int ChooseWaterfallStyle()
        {
            if (TenebrousMod.TheWorldIsOver == true)
            {
                return ModContent.GetInstance<DemonationWaterFall>().Slot;

            }
            return 0;
        }

        public override int GetSplashDust()
        {
            if (TenebrousMod.TheWorldIsOver == true)
            {
                return DustID.Lava;

            }
            return 0;
        }

        public override int GetDropletGore()
        {
            if (TenebrousMod.TheWorldIsOver == true)
            {
                return ModContent.GoreType<DemonationDroplet>();
            }
            return 0;
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            if (TenebrousMod.TheWorldIsOver == true)
            {
                r = 2f;
                g = 0.1f;
                b = 0.1f;
            }
        }

        public override byte GetRainVariant()
        {
            if (TenebrousMod.TheWorldIsOver == true)
            {
                return (byte)Main.rand.Next(3);
            }
            return 0;
        }

        public override Asset<Texture2D> GetRainTexture() => rainTexture;
    }

    public class DemonationDroplet : ModGore
    {
        public override void SetStaticDefaults()
        {
            ChildSafety.SafeGore[Type] = true;
            GoreID.Sets.LiquidDroplet[Type] = true;

            // Rather than copy in all the droplet specific gore logic, this gore will pretend to be another gore to inherit that logic.
            UpdateType = GoreID.WaterDrip;
        }
    }
}