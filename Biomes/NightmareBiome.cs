using Microsoft.Xna.Framework;
using System;
using TenebrousMod.Biomes;
using TenebrousMod;
using Terraria;
using Terraria.ModLoader;
using TenebrousMod.Items.Placeable;

namespace TenebrousMod.Biomes {
public class NightmareBiomeTileCount : ModSystem
{
    public int PelagicVeldCount;

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
    {
        PelagicVeldCount = tileCounts[ModContent.TileType<PelagicVeldTile>()];
    }
}
public class NightmareBiomeBackground : ModUndergroundBackgroundStyle
{
    public override void FillTextureArray(int[] textureSlots)
    {
        textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot("TenebrousMod/Backgrounds/NMBGUnderground0");
        textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot("TenebrousMod/Backgrounds/NMBGUnderground1");
        textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot("TenebrousMod/Backgrounds/NMBGUnderground2");
        textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot("TenebrousMod/Backgrounds/NMBGUnderground3");
    }
}
public class PelagicSea : ModBiome
{
    public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<NightmareBiomeBackground>();

    public override int Music => MusicLoader.GetMusicSlot("TenebrousMod/Assets/Music/NMBMusic");
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override Color? BackgroundColor => base.BackgroundColor;

    public override bool IsBiomeActive(Player player)
    {
        bool b1 = ModContent.GetInstance<NightmareBiomeTileCount>().PelagicVeldCount >= 40;
        bool b2 = Math.Abs(player.position.ToTileCoordinates().X - Main.maxTilesX / 2) < Main.maxTilesX / 6;
        bool b3 = player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight;
        return b1 && b2 && b3;
    }
}
}