using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Accessories
{
    public class ObsidianShell : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.LightPurple;
            Item.defense = 4;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Check if player has 1/8 health
            if (player.statLife <= player.statLifeMax2 / 2)
            {
                player.statDefense += 4;
                player.lifeRegen += 1; // Slowly heal the player
            }

            player.lavaImmune = true;
            base.UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Obsidian, 20)
                .AddIngredient(ItemID.ObsidianSkull, 1)
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddIngredient(ItemID.SoulofNight, 3)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
}
