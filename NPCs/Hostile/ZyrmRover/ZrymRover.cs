using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenebrousMod.Items.Weapons.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace TenebrousMod.NPCs.Hostile.ZyrmRover
{
    public class ZrymRover : ModNPC
    {
        private enum ActionState
        {
            Asleep,
            Notice,
            Jump,
            Hover,
            Fall
        }
        private enum Frame
        {
            Asleep,
            Notice,
            Falling,
            Flutter1,
            Flutter2,
            Flutter3
        }
        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];
        public ref float AI_FlutterTime => ref NPC.ai[2];
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.DebuffImmunitySets.Add(Type, new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned 
				}
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 54; 
            NPC.height = 36; 
            NPC.aiStyle = -1; 
            NPC.damage = 7; 
            NPC.defense = 2; 
            NPC.lifeMax = 25; 
            NPC.HitSound = SoundID.NPCHit1; 
            NPC.DeathSound = SoundID.NPCDeath1; 
            NPC.value = 25f; 
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDay.Chance * 0.1f;
        }
        private float MoveAmountCounter;
        private float AttackAmountCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(MoveAmountCounter);
            writer.Write(AttackAmountCounter);
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            MoveAmountCounter = reader.ReadInt32();
            AttackAmountCounter = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
        }
        public override void AI()
        {
            switch (AI_State)
            {
                case (float)ActionState.Asleep:
                    FallAsleep();
                    break;
                case (float)ActionState.Notice:
                    Notice();
                    break;
                case (float)ActionState.Jump:
                    Jump();
                    break;
                case (float)ActionState.Fall:
                    if (NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X = 0;
                        AI_State = (float)ActionState.Asleep;
                        AI_Timer = 0;
                    }

                    break;
            }
        }

       
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            switch (AI_State)
            {
                case (float)ActionState.Asleep:
                    NPC.frame.Y = (int)Frame.Asleep * frameHeight;
                    break;
                case (float)ActionState.Notice:
                    if (AI_Timer < 10)
                    {
                        NPC.frame.Y = (int)Frame.Notice * frameHeight;
                    }
                    else
                    {
                        NPC.frame.Y = (int)Frame.Asleep * frameHeight;
                    }

                    break;
                case (float)ActionState.Jump:
                    NPC.frame.Y = (int)Frame.Falling * frameHeight;
                    break;
                case (float)ActionState.Hover:
                    NPC.frameCounter++;

                    if (NPC.frameCounter < 10)
                    {
                        NPC.frame.Y = (int)Frame.Flutter1 * frameHeight;
                    }
                    else if (NPC.frameCounter < 20)
                    {
                        NPC.frame.Y = (int)Frame.Flutter2 * frameHeight;
                    }
                    else if (NPC.frameCounter < 30)
                    {
                        NPC.frame.Y = (int)Frame.Flutter3 * frameHeight;
                    }
                    else
                    {
                        NPC.frameCounter = 0;
                    }

                    break;
                case (float)ActionState.Fall:
                    NPC.frame.Y = (int)Frame.Falling * frameHeight;
                    break;
            }
        }

        public override bool? CanFallThroughPlatforms()
        {
            if (AI_State == (float)ActionState.Fall && NPC.HasValidTarget && Main.player[NPC.target].Top.Y > NPC.Bottom.Y)
            {
                return true;
            }

            return false;
        }

        private void FallAsleep()
        {
            Player player = new Player();
            NPC.TargetClosest(true);
                int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(NPC.direction * 5), ProjectileID.EyeBeam, 5, 0, Main.myPlayer);
                Main.projectile[projectile].timeLeft = 200;
                Main.projectile[projectile].damage = 15;
                Main.projectile[projectile].friendly = false;
                Main.projectile[projectile].hostile = true;
           
            if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < 1500f)
            {
                AI_State = (float)ActionState.Notice;
                AI_Timer = 0;
            }
        }

        private void Notice()
        {
            if (Main.player[NPC.target].Distance(NPC.Center) < 1500f)
            {
                AI_Timer++;

                if (AI_Timer >= 20)
                {
                    AI_State = (float)ActionState.Jump;
                    AI_Timer = 0;
                }
            }
            else
            {
                NPC.TargetClosest(true);

                if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > 1500f)
                {
                    AI_State = (float)ActionState.Asleep;
                    AI_Timer = 0;
                }
            }
        }

        private void Jump()
        {
            if (MoveAmountCounter > 0)
            {
                MoveAmountCounter /= 2;
            }
            AI_Timer++;

            if (AI_Timer == 1)
            {
                NPC.velocity = new Vector2(NPC.direction * 4);
                if (MoveAmountCounter < 0)
                {
                    AI_State = (float)ActionState.Fall;
                    MoveAmountCounter = 500;
                }
            }
            else if (AI_Timer > 40)
            {
                NPC.velocity.X = 0;
                AI_State = (float)ActionState.Asleep;
                AI_Timer = 0;
            }
        }

        public override bool ModifyCollisionData(Rectangle victimHitbox, ref int immunityCooldownSlot, ref MultipliableFloat damageMultiplier, ref Rectangle npcHitbox)
        {
            if (AI_State == (float)ActionState.Fall)
            {
                Rectangle extraDamageHitbox = new Rectangle(npcHitbox.X + 12, npcHitbox.Y + 18, npcHitbox.Width - 24, npcHitbox.Height - 18);
                if (victimHitbox.Intersects(extraDamageHitbox))
                {
                    damageMultiplier *= 2f;
                }
            }
            return true;
        }
    
}
}
