using Terraria.Audio;
using Terraria.ModLoader;

namespace TenebrousMod.TenebrousModSystem
{

    public class SoundPlayerTM : ModSystem
    {
        //
        //Summary:
        //file represents the file path to the SFX.
        public static SoundStyle? Sound(string file)
        {
            SoundStyle sound = new(file);
            SoundEngine.PlaySound(sound);
            return sound;
        }
    }
}
