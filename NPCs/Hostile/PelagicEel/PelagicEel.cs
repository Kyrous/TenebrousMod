using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TenebrousMod.NPCs;
using TenebrousMod.Biomes;
using Terraria.ModLoader.Utilities;

namespace TenebrousMod.NPCs.Hostile.PelagicEel
{
    internal class PelagicEelHead : WormHead
    {
        public override int BodyType => ModContent.NPCType<PelagicEelBody>();

        public override int TailType => ModContent.NPCType<PelagicEelTail>();

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
            NPC.lifeMax = 100;
            NPC.defense = 1;
            NPC.damage = 25;
            NPC.width = 26;
            NPC.height = 42;
            NPC.value = Item.buyPrice(silver: 10);

        }
        public override void OnKill()
        {
            base.OnKill();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.InModBiome<PelagicSea>())
            {
                return SpawnCondition.Underground.Chance * 2f; // Spawn with 1/10th the chance of a regular zombie.
            }
            return 0f;
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
                    NPC.netUpdate = true;
                }
            }
        }
    }

    internal class PelagicEelBody : WormBody
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
            NPC.width = 20;
            NPC.height = 16;
            NPC.defense = 2;
            NPC.damage = 15;

        }

        public override void Init()
        {
            PelagicEelHead.CommonWormInit(this);
        }
        public override void AI()
        {
            base.AI();
        }
    }

    internal class PelagicEelTail : WormTail
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
            NPC.width = 12;
            NPC.height = 16;
            NPC.defense = 0;
            NPC.damage = 10;
        }

        public override void Init()
        {
            PelagicEelHead.CommonWormInit(this);
        }
        public override void AI()
        {
        }
    }
}
