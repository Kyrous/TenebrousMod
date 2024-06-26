using TenebrousMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Surface
{
    public class WraithBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 64;
            Item.rare = ItemRarityID.LightRed;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 50;
            Item.height = 54;
            Item.knockBack = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 2);
            Item.scale = 2f;

        }
    }
}