using Terraria.Audio;
using Terraria.ModLoader;

namespace TenebrousMod.TenebrousModSystem
{

    public class SoundPlayerTM : ModSystem
    {
        /// <summary>
        /// file represents the file path to the SFX.
        /// </summary>
        public static SoundStyle? Sound(string file)
        {
            SoundStyle sound = new(file);
            SoundEngine.PlaySound(sound);
            return sound;
        }
    }
}
