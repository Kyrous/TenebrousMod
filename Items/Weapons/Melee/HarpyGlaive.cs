using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenebrousMod.Projectiles.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Melee
{
    public class HarpyGlaive : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Spears[Type] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults() 
        {
            Item.Size = new(74);
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item71 with { Pitch = 0.25f };
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.shoot = ModContent.ProjectileType<HarpyGlaiveProj>();
            Item.shootSpeed = 40f;
            Item.noMelee = true;
            Item.noUseGraphic = true;            
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 newVelocity = Vector2.UnitY * 32f;
                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI, ai1: 1f);
                return false;
            }
            else if (player.altFunctionUse != 2)
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                return false;
            }

            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return player.velocity.Y != 0f;
            }

            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool CanShoot(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return player.velocity.Y != 0f;
            }

            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool AltFunctionUse(Player player) => true;
    }
}
