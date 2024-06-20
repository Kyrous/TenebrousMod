using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
namespace TenebrousMod.Items.Weapons.TheGreatHarpy
{


    public class FeatherFanBuff : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.LilHarpy;
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {

            if (player.ownedProjectileCounts[ModContent.ProjectileType<FeatherFanMinion>()] > 0)
            {

                player.buffTime[buffIndex] = 1800;

            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;


            }

        }


    }


    public class FeatherFan : ModItem
    {


        public override void SetStaticDefaults()
        {

            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
            ItemID.Sets.StaffMinionSlotsRequired[Item.type] = 1f;

        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.damage = 20;
            Item.useAnimation = Item.useTime = 30;
            Item.ArmorPenetration = 10;
            Item.mana = 4;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.buffType = ModContent.BuffType<FeatherFanBuff>();
            Item.shoot = ModContent.ProjectileType<FeatherFanMinion>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            if (player.HasBuff(ModContent.BuffType<FeatherFanBuff>()))
                return false;

            player.AddBuff(ModContent.BuffType<FeatherFanBuff>(), 2);

            for (int i = 0; i < 10; i++)
            {
                var proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer, i * 16);
                proj.originalDamage = Item.damage;

            }



            return false;
        }




    }

    public class FeatherFanMinion : ModProjectile
    {

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.HarpyFeather;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
            ProjectileID.Sets.MinionSacrificable[Type] = true;
            Main.projPet[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 16;
            Projectile.tileCollide = false;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.minionSlots = 0.1f;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override bool? CanCutTiles()
        {
            return true;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<FeatherFanBuff>());
                return;
            }

            if (player.HasBuff(ModContent.BuffType<FeatherFanBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            IdlePos(player);

        }

        private void IdlePos(Player player)
        {


            Projectile.ai[0]++;
            Projectile.position = (new Vector2(MathF.Sin(Projectile.ai[0] * 0.04f) * 455, MathF.Cos(Projectile.ai[0] * 0.04f) * 255) + player.Center);
            Projectile.rotation = Projectile.Center.DirectionTo(player.Center).ToRotation() + MathHelper.ToRadians(90);
        }
    }

}