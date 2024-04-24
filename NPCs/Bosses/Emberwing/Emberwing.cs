using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using TenebrousMod.Items.Weapons.Melee;
using TenebrousMod.Items.Bars;
using Terraria.Graphics.CameraModifiers;
using TenebrousMod.Items.TreasureBags;
using TenebrousMod.Buffs;
using Terraria.GameContent.Bestiary;
using TenebrousMod.Items.Weapons.Ranger;
using TenebrousMod.Items.Weapons.Summoner;
using TenebrousMod.Items.Placeable.Furniture.Relic;
using TenebrousMod.Items.Placeable.Furniture.Trophy;

namespace TenebrousMod.NPCs.Bosses.Emberwing
{
    [AutoloadBossHead]
    public class Emberwing : ModNPC
    {
        private enum ActionState
        {
            Normal,
            Circle,
            Dash
        }
        public ref float AI_State => ref NPC.ai[0];
        //public ref float AI_Timer => ref NPC.ai[1];
        private int attackTimer = 0;
        //private int attackDuration = 300; // Adjust this value as needed
        private Vector2 offset = Vector2.Zero;
        Vector2 Acceleration = Vector2.Zero;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            Main.npcFrameCount[Type] = 5;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            NPC.width = 200;
            NPC.height = 200;
            NPC.lavaImmune = true;
            NPC.damage = 82;
            NPC.defense = 12;
            NPC.lifeMax = 35000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.MoonLord;
            NPC.knockBackResist = 0f;
            NPC.offSetDelayTime = 3333; // TODO: is this intended? its a static field
            NPC.noGravity = true;
            NPC.netAlways = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 10);
            NPC.boss = true;
            AnimationType = NPCID.CaveBat;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.OnFire3] = true;
            //FindPlayer();
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/EmberwingThemeStage1");
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (Main.expertMode) npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<EmberwingTreasureBag>()));
            if (!Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberwingTrophyI>(), 10));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberScythe>(), 2));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGreatEmber>(), 4));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberWarAxe>(), 3));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObscurumBar>(), 1, 24, 36));
            }
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<EmberwingRelicI>()));
            //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            //notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

            npcLoot.Add(notExpertRule);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
            // FIXME: the code here will cause a nullref so its commented out, but it missmatches with the ModifyNPCLoot contents
            // which should be kept? 
            /*
            NPCLoot npcLoot = new NPCLoot();
            if (Main.masterMode)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberwingTreasureBag>(), 1));
            else
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberWarAxe>(), 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberScythe>(), 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGreatEmber>(), 2));
            */
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				// Sets your NPC's flavor text in the bestiary.
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Emberwing, a majestic phoenix, was originally a creature of light and purity, associated with Virlina's blessings. However, Sinorus corrupted Emberwing's essence, turning it into a fiery harbinger of destruction. Now, it soars through the underworld, leaving trails of flames in anyone who gets in its way."),
            });


        }
        public override void AI()
        {
            // FIXME: this is incorrect
            Player player = new Player();
            if (player.dead)
            {
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.4f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }
            FindPlayer();
            switch (AI_State)
            {
                case (float)ActionState.Normal:
                    NormalAI();
                    break;
                case (float)ActionState.Circle:
                    CircleAttack(); 
                    break;
            }
            if (NPC.life <= 0.5 * NPC.lifeMax)
            {
                if (!Main.dedServ)
                {
                    Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/EmberwingThemeStage2");
                }
            }
        }
        public override void OnKill()
        {
            int HeadGore = Mod.Find<ModGore>("EmberwingGore1").Type;
            int FootGore = Mod.Find<ModGore>("EmberwingGore2").Type;

            var entitySource = NPC.GetSource_Death();

            for (int i = 0; i < 2; i++)
            {
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), HeadGore);
            }
            for (int i = 0; i < 2; i++)
            {
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), FootGore);
            }
            PunchCameraModifier modifier = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, FullName);
            Main.instance.CameraModifiers.Add(modifier);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<InternalImploding>(), 320);
        }
        private void FindPlayer()
        {
            float distance = 99999;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (NPC.Center.Distance(player.Center) < distance && player.active && !player.dead)
                {
                    NPC.target = i;
                    distance = NPC.Center.Distance(player.Center);
                }
            }
        }
        private bool checkTowardsPlayerX(Player player, Vector2 off)
        {
            float target = player.Center.X + off.X;
            // TODO: is -target doing something here? since its being subtracted from both sides
            if (Math.Abs(NPC.Center.X + NPC.velocity.X - target) <= Math.Abs(NPC.Center.X - target))
                return true;
            return false;
        }
        private bool checkTowardsPlayerY(Player player, Vector2 off)
        {
            float target = player.Center.Y + off.Y;
            if (Math.Abs(NPC.Center.Y + NPC.velocity.Y - target) <= Math.Abs(NPC.Center.Y - target))
                return true;
            return false;
        }
        private Vector2 Alignment(Vector2 current, Vector2 target, float distance, float pull)
        {
            Vector2 NewVelocity = Vector2.Zero;
            Vector2 vector1 = Vector2.Zero;
            vector1 = current - target;
            float magnitude = Vector2.Zero.Distance(vector1);
            vector1 = Vector2.Normalize(vector1);
            distance -= magnitude;
            pull *= distance;
            NewVelocity += vector1 * pull;
            return NewVelocity;
        }
        public bool a;
        private void NormalAI()
        {
            a = false;
            NPC.rotation = (float)Math.Sqrt(NPC.velocity.X) / 100;
            offset = Vector2.Zero;
            NPC npc = NPC;
            Player player = Main.player[NPC.target];
            NPC.FaceTarget();
            npc.velocity.X += 0.1f * npc.direction;
            if (!checkTowardsPlayerX(player, offset))
            {
                npc.velocity.X *= 0.99f;
            }
            if (npc.Center.Y > player.Center.Y)
            {
                npc.velocity.Y += -0.1f;
            }
            else
            {
                npc.velocity.Y += 0.1f;
            }
            if (!checkTowardsPlayerY(player, offset))
            {
                npc.velocity.Y *= 0.875f;
            }
            if (attackTimer == 600)
            {
                AI_State = (float)ActionState.Circle;
            }
            attackTimer++;
        }

        private void CircleAttack()
        {
            NPC npc = NPC;
            Player player = Main.player[NPC.target];//new Player();
            // Check if the boss should switch attack phases
            attackTimer++;



            // Implement movement and attacks based on the current attack phase
            // Adjust the dragon's position, velocity, and behavior accordingly
            offset = new Vector2(0, -240).RotatedBy(MathHelper.ToRadians(attackTimer));
            // Example: Move towards the player
            Vector2 movement = Alignment(NPC.Center, (Main.player[NPC.target].Center + offset), 0, 0.05f)/*(NPC.position - (Main.player[NPC.target].position + offset))*/;
            npc.velocity = movement;
            //npc.position -= npc.velocity;


            // Example: Fire breath attack
            if (attackTimer % 60 == 0) // Fire breath every 60 frames
            {
                int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * 0.1f, ModContent.ProjectileType<EmberBall>(), 48, 3, Main.myPlayer);
                NPC.netUpdate = true;
                attackTimer = 301;
                // Implement fire breath attack logic here
                // Create fire projectiles, play effects, etc.
                //int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * 0.1f, ProjectileID.DD2BetsyFlameBreath, 64, 2, Main.myPlayer);
            }
            if (attackTimer == 1200)
            {
                a = true;
            }
            if (a == true)
            {
                attackTimer = 0;
                AI_State = (float)ActionState.Normal;
            }

        }

    }
}
