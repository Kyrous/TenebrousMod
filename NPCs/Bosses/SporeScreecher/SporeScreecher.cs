using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TenebrousMod.NPCs;

namespace TenebrousMod.NPCs.Bosses.SporeScreecher
{
    [AutoloadBossHead] // This attribute looks for a texture called "ClassName_Head_Boss" and automatically registers it as the NPC boss head icon

    internal class SporeScreecherHead : WormHead
    {
        public override int BodyType => ModContent.NPCType<SporeScreecherBody>();

        public override int TailType => ModContent.NPCType<SporeScreecherTail>();

        public override void SetStaticDefaults()
        {
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }

        public override void SetDefaults()
        {
            // Head is 10 defence, body 20, tail 30.
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.lifeMax = 40000;
            NPC.defense = 24;
            NPC.damage = 75;
            NPC.boss = true;
            NPC.width = 46;
            NPC.height = 60;
            NPC.value = Item.buyPrice(gold: 82);
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/TheTimeToDive");
            }

        }
        public override void OnKill()
        {
            base.OnKill();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            NPC npc = new NPC();
            base.ModifyNPCLoot(npcLoot);
            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChorlHeart>(), 50, 1, 1));
            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WormShard>(), 1, 1, 3));
            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheDiverTrophy>(), 10, 1, 1));
            //npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<TheDiverRelic>()));

        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Some worms are ugly, like this one.")
            });
        }


        public override void Init()
        {
            MinSegmentLength = 36;
            MaxSegmentLength = 36;

            CommonWormInit(this);
        }

        internal static void CommonWormInit(Worm worm)
        {
            worm.MoveSpeed = 15f;
            worm.Acceleration = 0.09f;
        }

        private int attackCounter;
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
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (attackCounter > 0)
                {
                    attackCounter--; // tick down the attack counter.
                }

                Player target = Main.player[NPC.target];
                if (attackCounter <= 0 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                {
                    Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(10));
                    int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction * 10, ModContent.ProjectileType<SporeBomb>(), 5, 0, Main.myPlayer);
                    Main.projectile[projectile].timeLeft = 180;
                    Main.projectile[projectile].damage = 40;
                    Main.projectile[projectile].friendly = false;
                    Main.projectile[projectile].hostile = true;
                    if (NPC.life <= 200000)
                    {
                        attackCounter = 5;
                    }
                    else
                    {
                        attackCounter = 10;
                    }
                    NPC.netUpdate = true;
                }
                if (NPC.life <= 200000)
                {
                    NPC.defense = 36;

                }
            }
        }
    }

    internal class SporeScreecherBody : WormBody
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Diver");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.aiStyle = -1;
            NPC.width = 46;
            NPC.height = 66;
            NPC.defense = 38;
            NPC.damage = 20;

        }

        public override void Init()
        {
            SporeScreecherHead.CommonWormInit(this);
        }
        public override void AI()
        {
            if (NPC.life <= 200000)
            {
                NPC.defense = 57;

            }
            base.AI();
        }
    }

    internal class SporeScreecherTail : WormTail
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Diver");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.aiStyle = -1;
            NPC.width = 46;
            NPC.height = 66;
            NPC.defense = 16;
            NPC.damage = 25;
        }

        public override void Init()
        {
            SporeScreecherHead.CommonWormInit(this);
        }
        public override void AI()
        {
            if (NPC.life <= 200000)
            {
                NPC.defense = 32;

            }
        }
    }
}
