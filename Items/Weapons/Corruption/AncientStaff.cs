using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Corruption
{
    public class AncientStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 7));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            Item.staff[Item.type] = true;
            Item.color = Color.White;
        }
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.mana = 10;
            Item.DamageType = DamageClass.Magic;
            Item.width = 42;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 2, silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.CursedFlameFriendly;

            Item.shootSpeed = 11f;
        }
    }
}
