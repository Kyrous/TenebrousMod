using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using TenebrousMod.Items.Bars;
using TenebrousMod.Items.Placeable.Furniture.Relic;
using TenebrousMod.Items.Placeable.Furniture.Trophy;
using TenebrousMod.Items.TreasureBags;
using TenebrousMod.Items.Weapons.Emberwing;
using TenebrousMod.Items.Weapons.Icerus;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.NPCs.Bosses.Icerus
{
    [AutoloadBossHead]
    public class IcerusBossHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //if (Main.expertMode) npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<EmberwingTreasureBag>()));
            if (!Main.expertMode)
            {
                //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmberwingTrophyI>(), 10));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DominicsLostBlade>(), 2));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DominicsHorizon>(), 2));
            }
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<IcerusRelicI>()));

            //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            //notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

            npcLoot.Add(notExpertRule);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void SetDefaults()
        {
            NPC.width = 58;
            NPC.height = 66;
            NPC.damage = 95;
            NPC.defense = 9;
            NPC.lifeMax = 27000;
            NPC.boss = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.npcSlots = 10f;
            NPC.knockBackResist = 0;
            NPC.scale = 1.5f;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/IcerusTheme");
            }

        }

        private int[] body = new int[] { 0 };
        private NPC tail;
        private float frameCounter = 0f;
        private bool wentDown;
        private float velocityRotation;
        private float rotationSpeed = 0;
        private float speed = 15f;
        private int flyingMode = 0;
        private ref float attackMode => ref NPC.ai[0];
        private bool phase2 = false;
        public override void AI()
        {
            NPC.frameCounter++;
            Lighting.AddLight(NPC.Center, TorchID.Corrupt);
            Lighting.AddLight(NPC.Center, TorchID.Pink);
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            if (player.dead)
            {
                NPC.velocity += new Vector2(0, 1);
                NPC.EncourageDespawn(15);
                return;
            }

            if (!phase2 && NPC.life < NPC.lifeMax / 2)
                phase2 = true;


            Vector2 targetVelocity = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitY);
            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.ToRadians(90);

            switch (attackMode)
            {
                case (0):
                    rotationSpeed = 0.075f * (NPC.Center.Distance(player.Center) / 1000f);
                    velocityRotation = velocityRotation.AngleTowards(targetVelocity.ToRotation(), rotationSpeed);
                    NPC.velocity = velocityRotation.ToRotationVector2() * speed;
                    NPC.noTileCollide = true;
                    speed = 15;
                    if (phase2)
                        speed = 20;
                    AttackCooldown++;
                    if (AttackCooldown % 240 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, NPC.Center.DirectionTo(player.Center) * 5, ProjectileID.CultistBossIceMist, 45, 1, -1, 0, 1);

                    }

                    if (NPC.Center.Distance(player.Center) < 455 && AttackCooldown > 600)
                    {
                        AttackCooldown = 0;

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            attackMode = Main.rand.Next(1, 3);

                            NPC.netUpdate = true;
                        }

                        if (attackMode == 2)
                            NPC.noTileCollide = false;
                    }

                    break;

                case (1):
                    AttackCooldown++;
                    speed = 0.04f;
                    NPC.velocity = NPC.Center.DirectionTo(new Vector2(MathF.Sin(AttackCooldown * speed) * 500, MathF.Cos(AttackCooldown * speed) * 500) + player.Center) * 20;

                    if (AttackCooldown % 50 == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, NPC.Center.DirectionTo(player.Center), ModContent.ProjectileType<IceIndicatorProj>(), 70, 1);

                            if (phase2)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, NPC.Center.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(45)), ModContent.ProjectileType<IceIndicatorProj>(), 70, 1);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, NPC.Center.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(-45)), ModContent.ProjectileType<IceIndicatorProj>(), 70, 1);

                            }

                        }

                        AttackCounter++;
                        if (AttackCounter == 15)
                        {
                            attackMode = 0;
                            AttackCooldown = 0;
                            AttackCounter = 0;
                            velocityRotation = NPC.velocity.ToRotation();
                            NPC.netUpdate = true;

                        }

                    }
                    break;

                case 2:
                    AttackCooldown++;

                    targetVelocity = new Vector2(0, 25);
                    if (NPC.collideY && Main.netMode != NetmodeID.MultiplayerClient)
                    {

                        for (int i = 0; i < 7; i++)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(10, -1).RotatedByRandom(0.5f) * 10, ProjectileID.DeerclopsRangedProjectile, 70, 1, -1, 1);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(-10, -1).RotatedByRandom(0.5f) * 10, ProjectileID.DeerclopsRangedProjectile, 70, 1, -1, 1);

                            if (phase2)
                            {

                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(100 * i, -1000), Vector2.UnitY, ModContent.ProjectileType<IceIndicatorProj>(), 70, 1);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(-100 * i, -1000), Vector2.UnitY, ModContent.ProjectileType<IceIndicatorProj>(), 70, 1);

                            }
                        }
                        AttackCooldown = 0;
                        attackMode = 0;
                        PunchCameraModifier modifier = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, FullName);
                        Main.instance.CameraModifiers.Add(modifier);
                        NPC.netUpdate = true;
                    }
                    rotationSpeed = 0.1f;
                    velocityRotation = velocityRotation.AngleTowards(targetVelocity.ToRotation(), rotationSpeed);
                    NPC.velocity = velocityRotation.ToRotationVector2() * speed;



                    break;
            }

        }



        public ref float AttackCooldown => ref NPC.ai[2];
        public ref float AttackCounter => ref NPC.ai[3];

        public override void OnSpawn(IEntitySource source)
        {
            System.Collections.Generic.List<int> list = body.ToList();
            list.Add(NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<IcerusBossBody>(), 0, NPC.whoAmI, NPC.whoAmI));
            for (int i = 0; i < 13; i++)
            {
                list.Add(NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<IcerusBossBody>(), 0, list[list.Count - 1], NPC.whoAmI));
            }
            body = list.ToArray();
            tail = NPC.NewNPCDirect(NPC.GetSource_FromAI(), NPC.Center, ModContent.NPCType<IcerusBossTail>(), 0, body[body.Length - 1], NPC.whoAmI);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < body.Length; i++)
                {
                    Main.npc[body[i]].StrikeInstantKill();
                }
                tail.StrikeInstantKill();

                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                PunchCameraModifier modifier = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 60, 1000f, FullName);
                Main.instance.CameraModifiers.Add(modifier);


            }
        }


    }

    public class IcerusBossBody : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 28;
            NPC.damage = 50;
            NPC.defense = 18;
            NPC.lifeMax = 1;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0;
            NPC.scale = 1.5f;
        }

        private NPC owner;
        private NPC head;

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, TorchID.Corrupt);
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if (NPC.frameCounter == 0)
            {
                owner = Main.npc[(int)NPC.ai[0]];
                head = Main.npc[(int)NPC.ai[1]];
            }

            NPC.Center = owner.Center - Vector2.Normalize(owner.Center - NPC.Center) * ((owner.height + NPC.height) / 2);
            NPC.rotation = (float)((NPC.Center - owner.Center).ToRotation() - Math.PI / 2);

        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (!hit.InstantKill)
                NPC.life = NPC.lifeMax;

            head.StrikeNPC(NPC.CalculateHitInfo(hit.Damage, 0));


        }
    }

    public class IcerusBossTail : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 30;
            NPC.damage = 50;
            NPC.defense = 6;
            NPC.lifeMax = 1;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0;
            NPC.scale = 1.5f;
        }

        private NPC owner;
        private NPC head;

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, TorchID.Corrupt);
            if (NPC.frameCounter == 0)
            {
                owner = Main.npc[(int)NPC.ai[0]];
                head = Main.npc[(int)NPC.ai[1]];
            }
            NPC.Center = owner.Center - Vector2.Normalize(owner.Center - NPC.Center) * ((owner.height + NPC.height) / 2);
            NPC.rotation = (float)((NPC.Center - owner.Center).ToRotation() - Math.PI / 2);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (!hit.InstantKill)
                NPC.life = NPC.lifeMax;

            head.StrikeNPC(NPC.CalculateHitInfo(hit.Damage, 0));

        }
    }

    public class IceIndicatorProj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 2500;
        }
        bool fire = false;
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            fire = false;
            Projectile.rotation = Projectile.ai[0];


        }
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.HallowStar;
        private static Asset<Texture2D> texture;

        public override bool PreDraw(ref Color lightColor)
        {
            texture = TextureAssets.Extra[98];

            Vector2 spawner = Projectile.Center - Main.screenPosition;
            float maxDistance = 1700;
            if (!fire)
                for (float seg = 1; seg < maxDistance; seg++)
                {
                    Main.EntitySpriteDraw(texture.Value, spawner + (seg * 15) * (Projectile.rotation - MathHelper.ToRadians(90)).ToRotationVector2(), new Rectangle(0, 0, 72, 72), Color.Lerp(Color.LightBlue, Color.Blue, Math.Abs(MathF.Sin(seg * 0.08f + Projectile.timeLeft * 0.08f))), Projectile.rotation, texture.Size() / 2f, 0.75f, SpriteEffects.None);

                }
            Main.EntitySpriteDraw(texture.Value, spawner, new Rectangle(0, 0, 72, 72), Color.AntiqueWhite, Projectile.rotation, texture.Size() / 2f, 2, SpriteEffects.None);

            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {

            return new Color(1f, 1f, 1f, 1f);


        }

        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 25; i++)
            {
                Vector2 circleExplosion = Main.rand.NextVector2CircularEdge(5, 5);
                Dust.NewDustPerfect(Projectile.Center, DustID.IceTorch, circleExplosion).noGravity = true;

            }
        }

        public override void AI()
        {

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);


            if (Projectile.timeLeft == 250)
            {
                fire = true;
                Projectile.velocity = (Projectile.rotation - MathHelper.ToRadians(90)).ToRotationVector2() * 35;
            }

        }



    }

}