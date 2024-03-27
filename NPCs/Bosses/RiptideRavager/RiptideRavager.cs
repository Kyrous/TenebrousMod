using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TenebrousMod.Items.Bars;
using TenebrousMod.Items.Placeable.Furniture;
using TenebrousMod.Items.TreasureBags;
using TenebrousMod.Items.Weapons.Melee;
using Terraria.GameContent.ItemDropRules;
using TenebrousMod.Items.Weapons.Summoner;
using TenebrousMod.Items.Weapons.Ranger;
using Terraria.GameContent.Bestiary;

namespace TenebrousMod.NPCs.Bosses.RiptideRavager
{
    [AutoloadBossHead]
    public class RiptideRavager : ModNPC
    {
        public ref float AI_State => ref NPC.ai[0];
        public ref float BounceTimer => ref NPC.ai[1];
        public ref float SwitchTimer => ref NPC.ai[2];

        private const int SwitchInterval = 600;

        private bool bouncing = false;

        private bool splash = false;

        private bool wasWet = false;

        private float dashAccel = 5f;
        private float dashSpeed = 0f;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            Main.npcFrameCount[Type] = 6;
            NPC.ai[1] = 20;
        }
        public override void SetDefaults()
        {
            NPC.width = 156;
            NPC.height = 82;
            NPC.damage = 18;
            NPC.defense = 8;
            NPC.lifeMax = 6400;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.MoonLord;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.value = Item.buyPrice(gold: 7);
            NPC.waterMovementSpeed = 1f;
            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;
            NPC.GravityIgnoresLiquid = true;
            AnimationType = NPCID.CaveBat;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/RiptideRavagerTheme");
            }
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				// Sets your NPC's flavor text in the bestiary.
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Riptide Ravager, once a guardian of the seas under Sinorus and Virlina's protection, fell victim to Sinorus's descent into darkness. Cursed by Sinorus, Riptide Ravager now roams the oceans, wreaking havoc and unleashing chaotic storms upon any who cross its path."),
            });


        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if(Main.expertMode)
                npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<RiptideRavagerTreasureBag>()));

            if (!Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiptideRavagerTrophyI>(), 10));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiptideBow>(), 2));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiptideStaff>(), 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Riptide>(), 1));
            }
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<RiptideRavagerRelicI>()));
            //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            //notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

            npcLoot.Add(notExpertRule);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Frostburn, 15);
        }
        private enum ActionState
        {
            Normal,
            Charge
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
            BasicAIChecks();
            Bounce();
            FindPlayer();
            ChangeActionState();
            ExecuteActionState();
        }
        private void lookAtPlayer(NPC npc, Player player)
        {
            if (npc.Center.X > player.Center.X)
            {
                npc.direction = -1;
            }
            else
            {
                npc.direction = 1;
            }
        }
        private void ChangeActionState()
        {
            NPC.ai[2]++;
            int nextState = (int)AI_State + 1;
            if (NPC.ai[2] == SwitchInterval)
            {
                if (nextState > (int)ActionState.Charge)
                {
                    nextState = (int)ActionState.Normal;
                    NPC.ai[2] = 0;
                }
            }
            AI_State = nextState;
        }
        private void ExecuteActionState()
        {
            NPC npc = NPC;
            Player player = Main.player[NPC.target];
            splash = NPC.wet && !wasWet;
            wasWet = NPC.wet;
            switch (AI_State)
            {
                case (int)ActionState.Normal:
                    lookAtPlayer(NPC, player);
                    MoveTowardsPlayer();
                    break;
                case (int)ActionState.Charge:
                    //LookAtPlayer(NPC, player);
                    Charge();
                    break;
            }
        }
        private void MoveTowardsPlayer()
        {
            Vector2 moveTo = Main.player[NPC.target].Center - NPC.Center;
            moveTo.Normalize();
            NPC.velocity = moveTo * GetSpeed();
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
        private float GetSpeed()
        {
            const float MaxSpeed = 50f;
            const int MaxSpeedDistance = 50 * 32;
            float distance = Vector2.Distance(Main.player[NPC.target].Center, NPC.Center);
            float speedMultiplier = MathHelper.Clamp(distance / MaxSpeedDistance, 0f, 1f);
            return MaxSpeed * speedMultiplier;
        }
        private void BasicAIChecks()
        {
            NPC npc = NPC;
            Player player = Main.player[NPC.target];
            Vector2 moveTo = player.Center - NPC.Center;
            if (!NPC.wet)
            {
                NPC.velocity.Y += 0.2f;
                return;

            }
            else
            {
                NPC.velocity *= 0.99f;
            }
            lookAtPlayer(NPC, player);
        }
        private void Bounce()
        {
            NPC.ai[1]++;
            if (NPC.ai[1] >= 12)
            {
                bouncing = false;
                NPC.ai[1] = 0;
            }
            else if (NPC.ai[1] % 10 == 0)
            {
                if (NPC.velocity.Y < 1f)
                {
                    NPC.velocity.Y += 0.025f;
                }
            }
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

        private void LookAtPlayer(NPC NPC, Player player)
        {
            NPC.direction = NPC.Center.X > player.Center.X ? -1 : 1;
        }
        private void NormalAI()
        {
            NPC npc = NPC;
            Player player = Main.player[NPC.target];
            //lookAtPlayer(NPC, player);
            NPC.velocity.X = 4f * NPC.direction;
            /*if (NPC.position.X > player.position.X)
                NPC.velocity.X = 4f;
            if (NPC.position.X < player.position.X)
                NPC.velocity.X = -4f;*/
        }
       
        private void Charge()
        {
            NPC npc = NPC;
            Player player = Main.player[NPC.target];
            Vector2 moveTo = player.Center - NPC.Center;
            float distance = moveTo.Length();
            //LookAtPlayer(NPC, player);
            if (distance <= 640)
            {
                float speedChunk = dashAccel * 0.1f;

                dashSpeed += speedChunk;
                dashAccel -= speedChunk;

                Vector2 dashDirection = player.Center - NPC.Center;
                dashDirection.Normalize();

                NPC.velocity = dashDirection * dashSpeed;
                NPC.damage = 32;
            }
            else
            {
                NPC.damage = 18;
            }
        } 
    }
}