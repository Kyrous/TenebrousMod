using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace TenebrousMod.Items.Weapons.Melee
{
    public class Riptide : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.rare = ItemRarityID.Orange;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 48;
            Item.height = 48;
            Item.shoot = ProjectileID.WaterBolt;
            Item.shootSpeed = 9f;
            Item.knockBack = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 1);

        }
    }
}