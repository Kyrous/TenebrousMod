using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TenebrousMod.Items;
using TenebrousMod.Content.Items;

namespace TenebrousMod.TenebrousModSystem
{
    public class TenebrousModRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            /*
            // Here is an example of a recipe
            var Item1 = ModContent.GetInstance<Items.Item1>();
            
            Item1.CreateRecipe() // notice that theres no semicolon
                .AddIngredient(ItemID.Dirt, 10)
                .AddTile(TileID.WorkBenches)
                .Register(); // but here there is a semicolon
            // and with this you get a recipe for Item1 that cost 10 dirt and requires being near any workbench
            */

            var FeatherFan = ModContent.GetInstance<Items.Weapons.Space.FeatherFan>();

            FeatherFan.CreateRecipe()
                .AddIngredient(ItemID.Feather, 5)
                .Register();

            var FeatherStorm = ModContent.GetInstance<Items.Weapons.Space.FeatherStorm>();

            FeatherStorm.CreateRecipe() 
                .AddIngredient(ItemID.Feather, 5)
                .Register();

            var WingedJavelin = ModContent.GetInstance<Items.Weapons.Space.WingedJavelin>();

            WingedJavelin.CreateRecipe()
                .AddIngredient(ItemID.Feather, 5)
                .Register();

            var BronzeIngot = ModContent.GetInstance<Items.Materials.BronzeIngot>();

            BronzeIngot.CreateRecipe()
                .AddIngredient(ItemID.CopperOre, 5)
                .AddIngredient(ItemID.TinOre, 5)
                .AddTile(TileID.SkyMill)
                .Register();

            var Layer1Warp = ModContent.GetInstance<Items.Warp.Layer1Warp>();

            Layer1Warp.CreateRecipe()
                .AddIngredient<BronzeIngot>(10)
                .Register();
        }
    }
}