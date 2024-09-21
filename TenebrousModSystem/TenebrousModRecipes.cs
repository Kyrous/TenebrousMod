using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TenebrousMod.Items;

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

            
            // Here are some recipes for the harpy stuff (that refuse to work bcuz harpy folder doesnt exist??)
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
            
        }
    }
}