using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TenebrousMod.Items.TreasureBags;

namespace TenebrousMod
{
	public class TenebrousMod : Mod
	{
	}
    public class TenebrousModPlayer : ModPlayer
    {
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            return new[] {
                new Item(ModContent.ItemType<StarterBag>(), 1),
            };
        }
    }
}