using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.IO;
using TenebrousMod.Buffs;
using Microsoft.CodeAnalysis;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace TenebrousMod.Items.Weapons.Summoner
{
    public class RiptideStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.mana = 5;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<RavagedRiptide>();
            Item.shoot = ModContent.ProjectileType<RiptideStaffProj>();
            Item.rare = ItemRarityID.Orange;
            Item.damage = 21;
            Item.width = 52;
            Item.height = 52;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            projectile.originalDamage = Item.damage;

            return false;
        }
    }
    public class RiptideStaffProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }
        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.damage = 21;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }
        private float attackCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }
        public override void AI()
        {
            // TODO: this is not correct, the npc isn't one of the in-game ones
            Player player = Main.player[Projectile.owner];
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<RavagedRiptide>());
            }
            if (player.HasBuff(ModContent.BuffType<RavagedRiptide>()))
            {
                Projectile.timeLeft = 2;
            }

            NPC npc = Main.npc[player.MinionAttackTargetNPC];
            float between = Vector2.Distance(npc.Center, Projectile.Center);
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 48f;
            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -player.direction;
            idlePosition.X += minionPositionOffsetX;
            int startAttackRange = 700;
            int attackTarget = -1;
            float distanceFromTarget = 700f;
            Vector2 targetCenter = Projectile.position;
            bool foundTarget = false;
            Projectile.Minion_FindTargetInRange(startAttackRange, ref attackTarget, false);
            distanceFromTarget = between;
            targetCenter = npc.Center;
            foundTarget = true;
            bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
            bool inRange = between < distanceFromTarget;
            bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
            bool abovePlayer = player.Center.Y > npc.Center.Y;
            if (player.HasMinionAttackTargetNPC)
            {
                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }
            if (!foundTarget)
            {
                // Regular target finding loop
            }
        }
        }
    }
