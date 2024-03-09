using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using TenebrousMod.Textures;

namespace TenebrousMod.TenebrousModSystem
{

    public class TModMenu : ModMenu
    {
        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("TenebrousMod/Textures/ModLogo");
        public override Asset<Texture2D> SunTexture => Asset<Texture2D>.Empty;
        public override ModSurfaceBackgroundStyle MenuBackgroundStyle { get => ModContent.GetInstance<Background>(); }
        public override Asset<Texture2D> MoonTexture => Asset<Texture2D>.Empty;

       

       public override int Music => MusicLoader.GetMusicSlot("TenebrousMod/Assets/Music/MainMenuMusic");
        public override void Load()
        {
            Main.menuBGChangedDay = true;
            base.Load();
        }
        public override string DisplayName => "Tenebrous Theme Day";

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            drawColor = Color.White;
            return base.PreDrawLogo(spriteBatch, ref logoDrawCenter, ref logoRotation, ref logoScale, ref drawColor);
        }




    }

    public class TModMenu2 : ModMenu
    {
        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("TenebrousMod/Textures/ModLogo");
        public override Asset<Texture2D> SunTexture => Asset<Texture2D>.Empty;
        public override ModSurfaceBackgroundStyle MenuBackgroundStyle { get => ModContent.GetInstance<Background2>(); }
        public override Asset<Texture2D> MoonTexture => Asset<Texture2D>.Empty;



        public override int Music => MusicLoader.GetMusicSlot("TenebrousMod/Assets/Music/MainMenuMusic2");
        public override void Load()
        {
            Main.menuBGChangedDay = true;
            base.Load();
        }
        public override string DisplayName => "Tenebrous Theme Night";

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            drawColor = Color.White;
            return base.PreDrawLogo(spriteBatch, ref logoDrawCenter, ref logoRotation, ref logoScale, ref drawColor);
        }




    }






}

