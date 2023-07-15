using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TenebrousMod.Textures;

public class Background : ModSurfaceBackgroundStyle
{
    

    public override void ModifyFarFades(float[] fades, float transitionSpeed)
    {
        
       
    }
    public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(ModContent.Request<Texture2D>("TenebrousMod/Textures/Background", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, new Microsoft.Xna.Framework.Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Microsoft.Xna.Framework.Color.White);
        return true;
    }
    public override void Load()
    {
        
        
        base.Load();
    }
}
