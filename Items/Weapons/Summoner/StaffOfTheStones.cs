using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.IO;

namespace TenebrousMod.Items.Weapons.Summoner
{
    public class StaffOfTheStones : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.mana = 5;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Summon;
            // Item.buffType = ModContent.BuffType<ExampleSimpleMinionBuff>();
            Item.shoot = ModContent.ProjectileType<StoneProj>();
            Item.damage = 13;
            Item.width = 30;
            Item.height = 38;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            // TODO: is this multiplayer friendly?
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            projectile.originalDamage = Item.damage;

            return false;
        }
    }
    public class StoneProj : ModProjectile
    {
        private int attackTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true;

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.damage = 9;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = ProjAIStyleID.FloatInFrontPet;
            Projectile.scale = 1;
        }
        public override void AI()
        {
            attackTimer++;
            NPC target = GetClosestHostileNPC(Projectile.Center, 500);

            if (attackTimer % 15 == 0)
            {
                if (target != null)
                {
                    Vector2 velocity = Vector2.Normalize(target.Center - Projectile.Center) * 0.1f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity * 6, ProjectileID.NailFriendly, 9, 1, Projectile.owner);
                    attackTimer = 301;
                }
            }
        }
        private NPC GetClosestHostileNPC(Vector2 position, float maxDistance)
        {
            NPC closestNPC = null;
            float closestDistSquared = float.MaxValue;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.life > 0)
                {
                    float distSquared = Vector2.DistanceSquared(position, npc.Center);

                    if (distSquared < maxDistance * maxDistance && distSquared < closestDistSquared)
                    {
                        closestNPC = npc;
                        closestDistSquared = distSquared;
                    }
                }
            }

            return closestNPC;
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

    }
}