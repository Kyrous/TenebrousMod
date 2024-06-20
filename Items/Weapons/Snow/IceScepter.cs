using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Snow
{
    public class IceScepter : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.mana = 10;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 2, silver: 85);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.FrostBlastFriendly;
            Item.shootSpeed = 3f;
        }
    }
}