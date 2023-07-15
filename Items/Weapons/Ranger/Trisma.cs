using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Ranger
{
    public class Trisma : ModItem
    {
        public override void SetDefaults() 
        {
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 100;
            Item.useAmmo = AmmoID.Arrow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 32;
            Item.height = 56;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Purple;
            Item.shootSpeed = 10f;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.UseSound = SoundID.Item11;
            Item.shoot = ProjectileID.HellfireArrow;
        }
    }
}
