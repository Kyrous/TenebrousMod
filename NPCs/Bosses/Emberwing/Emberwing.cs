using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using TenebrousMod.Items.Bars;
using Terraria.Graphics.CameraModifiers;
using TenebrousMod.Items.TreasureBags;
using TenebrousMod.Buffs;
using Terraria.GameContent.Bestiary;
using TenebrousMod.Items.Weapons.Emberwing;
using TenebrousMod.Items.Placeable.Furniture.Relic;
using TenebrousMod.Items.Placeable.Furniture.Trophy;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.Graphics.Effects;

namespace TenebrousMod.NPCs.Bosses.Emberwing
{
    [AutoloadBossHead]
    public class Emberwing : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            Main.npcFrameCount[Type] = 5;
            NPCID.Sets.TrailCacheLength[Type] = 5;
            NPCID.Sets.TrailingMode[Type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.width = 200;
            NPC.height = 200;
            NPC.lavaImmune = true;
            NPC.damage = 82;
            NPC.defense = 20;
            NPC.lifeMax = 32000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.MoonLord;
            NPC.knockBackResist = 0f;
            NPC.offSetDelayTime = 3333; // TODO: is this intended? its a static field
            NPC.noGravity = true;
            NPC.netAlways = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 26);
            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.OnFire3] = true;
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

        private int ticker;
        private float frameCounter = 0;
        private float frameIncrement = 1;
        private int currentStyle = 0; // 0 is glide around and drop 3 fireballs, 1 is dash
        private bool dashToPlayer = false;
        private bool needsToDie = false;
        private int deathTimer = 0;
        private int dashesMaxFirstPhase = 1;
        private int dashesMaxSecondPhase = Main.masterMode ? 3 : 2;
        private int dashesRemaining = 0;
        private int dashTimer = 0;
        private int waitTime = 300;

        public void Dash()
        {
            Player player = Main.player[NPC.target];
            float speed = 25;
            Vector2 velocity = Vector2.Normalize(player.Center - NPC.Center) * speed;
            NPC.velocity += (velocity - NPC.velocity) / 25;
            NPC.velocity = velocity;
            dashToPlayer = true;
            dashesRemaining--;
            dashTimer = 0;
        }

        public override void AI()
        {
            if (NPC.life > NPC.lifeMax / 2)
            {
                Lighting.AddLight(NPC.Center, TorchID.Yellow);
                Lighting.AddLight(NPC.Center, TorchID.Orange);
                Lighting.AddLight(NPC.Center, TorchID.Red);
            }
            else
            {
                NPC.defense = 20;
                Lighting.AddLight(NPC.Center, TorchID.Blue);
                Lighting.AddLight(NPC.Center, TorchID.Yellow);
                Lighting.AddLight(NPC.Center, TorchID.Red);
            }
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            NPC.spriteDirection = player.Center.X - NPC.Center.X < 0 ? -1 : 1;
            NPC.rotation = NPC.velocity.X * 0.03f;
            if (ticker % 5 == 0)
            {
                NPC.frameCounter += frameIncrement;
            }
            if (NPC.frameCounter == 4 || NPC.frameCounter == 0)
            {
                frameIncrement *= -1;
            }
            ticker++;

            if (player.dead)
            {
                NPC.velocity += new Vector2(0, -1);
                NPC.EncourageDespawn(20);
            }
            else
            {
                if (deathTimer == 0)
                {
                    frameCounter += 0.03f;
                    if (frameCounter > 7)
                    {
                        currentStyle = 1;
                    }
                    else
                    {
                        currentStyle = 0;
                        if (dashesRemaining == 0)
                        {
                            dashesRemaining = NPC.life >= NPC.lifeMax / 3 ? dashesMaxFirstPhase : dashesMaxSecondPhase;
                        }
                    }

                    if (currentStyle == 0)
                    {
                        float speed = 9;
                        Vector2 velocity = Vector2.Normalize(player.Center + new Vector2((float)Math.Sin(frameCounter) * 450, -250 + (float)Math.Sin(frameCounter * 3) * 75) - NPC.Center) * speed;
                        NPC.velocity += (velocity - NPC.velocity) / 10;
                        if (frameCounter > 4 && ticker > waitTime)
                        {
                            if (ticker % 25 == 0)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Normalize(((float)((player.Center - NPC.Center).ToRotation() + (Main.rand.Next(-10, 10) * (Math.PI / 180)))).ToRotationVector2()) * 5, ModContent.ProjectileType<EmberBall>(), NPC.damage / 3, 3f);
                                }
                            }
                        }
                        if (frameCounter > 1 && frameCounter < 3 && ticker % 15 == 0 && ticker > waitTime)
                        {
                            Projectile projectile = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Vector2.Normalize(((float)((player.Center - NPC.Center).ToRotation() + (Main.rand.Next(-10, 10) * (Math.PI / 180)))).ToRotationVector2()) * 15, ProjectileID.DD2PhoenixBowShot, NPC.damage / 3, 3f);
                            projectile.hostile = true;
                            projectile.friendly = false;
                            if (NPC.life < NPC.lifeMax / 2)
                            {
                                projectile = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Vector2.Normalize(((float)((player.Center - NPC.Center).ToRotation() + (Main.rand.Next(-10, 10) * (Math.PI / 180)))).ToRotationVector2()) * 15, ProjectileID.DD2BetsyFireball, NPC.damage / 2, 3f);
                                projectile.hostile = true;
                                projectile.friendly = false;
                            }
                        }
                    }
                    if (currentStyle == 1)
                    {
                        if (!dashToPlayer)
                        {
                            Dash();
                        }
                        dashTimer++;
                        if (dashToPlayer)
                        {
                            if (dashTimer > 30)
                            {
                                dashToPlayer = false;
                                if (dashesRemaining == 0)
                                {
                                    frameCounter = 0;
                                }
                                else
                                {
                                    frameCounter = 7;
                                }
                            }
                        }
                    }

                    if (NPC.life <= 0.5 * NPC.lifeMax)
                    {
                        if (!Main.dedServ)
                        {
                            //Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/EmberwingThemeStage2");
                        }
                    }
                }

                if (ticker < waitTime)
                {
                    NPC.velocity /= 1.1f;
                }

                if (NPC.life < 1000)
                {
                    NPC.life = 1;
                    NPC.velocity *= 0.97f;
                    NPC.immortal = true;
                    NPC.damage = 0;
                    if (deathTimer % 12 == 0)
                    {
                        float randomAngle = (float)(Main.rand.Next(360) * (Math.PI / 180));
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2((float)Math.Cos(randomAngle) * Main.rand.Next(100), (float)Math.Sin(randomAngle) * Main.rand.Next(100)), Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0f);
                    }
                    if (deathTimer % 8 == 0 && deathTimer > 100)
                    {
                        float randomAngle = (float)(Main.rand.Next(360) * (Math.PI / 180));
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2((float)Math.Cos(randomAngle) * Main.rand.Next(150), (float)Math.Sin(randomAngle) * Main.rand.Next(150)), Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0f);
                    }
                    if (deathTimer % 6 == 0 && deathTimer > 150)
                    {
                        float randomAngle = (float)(Main.rand.Next(360) * (Math.PI / 180));
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2((float)Math.Cos(randomAngle) * Main.rand.Next(200), (float)Math.Sin(randomAngle) * Main.rand.Next(200)), Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0f);
                    }
                    if (deathTimer % 2 == 0 && deathTimer > 200)
                    {
                        float randomAngle = (float)(Main.rand.Next(360) * (Math.PI / 180));
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2((float)Math.Cos(randomAngle) * Main.rand.Next(300), (float)Math.Sin(randomAngle) * Main.rand.Next(300)), Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0f);
                    }
                    if (deathTimer > 250)
                    {
                        needsToDie = true;
                        NPC.immortal = false;
                    }
                    deathTimer++;
                }
                if (needsToDie)
                {
                    NPC.StrikeInstantKill();
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
            //target.AddBuff(ModContent.BuffType<InternalImploding>(), 320);
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameHeight * (int)(NPC.frameCounter);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture;
            if (NPC.life <= NPC.lifeMax / 2)
            {
                texture = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Emberwing/EmberwingSecondPhase").Value;
            }
            else
            {
                texture = TextureAssets.Npc[NPC.type].Value;
            }
            for (int i = 0; i < NPCID.Sets.TrailCacheLength[Type]; i++)
            {
                spriteBatch.Draw(texture, NPC.oldPos[i] - screenPos, new Rectangle(0, (int)((texture.Height / 5) * NPC.frameCounter), texture.Width, texture.Height / 5), drawColor * (1 - ((float)i / NPCID.Sets.TrailCacheLength[Type])), NPC.oldRot[i], new Vector2(NPC.width / 2, NPC.height / 2), 1f, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            spriteBatch.Draw(texture, NPC.position - screenPos, new Rectangle(0, (int)((texture.Height / 5) * NPC.frameCounter), texture.Width, texture.Height / 5), drawColor, NPC.rotation, new Vector2(NPC.width / 2, NPC.height / 2), 1f, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }
    }
}