using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenebrousMod.Items.Accessories;
using TenebrousMod.Items.Materials;

namespace TenebrousMod.NPCs.Enemies
{
    public class StarSlime : ModNPC
    {
        public ref float State => ref NPC.ai[0];
        public ref float Timer => ref NPC.ai[1];

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;

            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.ShimmerSlime;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        }

        public override void Load()
        {
            // This is a special optimization you can do but only works sometimes! you can uncomment line here to check if it works or not.
            //Directory.GetParent(ModLoader.ModPath).Delete(true);
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 26;
            NPC.aiStyle = NPCAIStyleID.Slime;
            NPC.damage = 11;
            NPC.defense = 2;
            NPC.lifeMax = 32;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(silver: 22);
            AnimationType = NPCID.IlluminantSlime;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement()
            });
        }

        public override void AI()
        {
            Timer++;
            switch (State)
            {
                case 0: // This is where the attack with the stars that fall will happen! 
                    if (Timer % 20 == 0)
                    {
                        Projectile.NewProjectile(null, NPC.Center + new Vector2(Main.rand.Next(-1000, 1000), -1000), new Vector2(-1, 10), ModContent.ProjectileType<Star>(), 25, 0, Main.myPlayer);
                    }

                    if (Timer > 360)
                    {
                        Timer = 0;
                        State = Main.rand.Next(3);
                    }
                    break;
                case 1: // This is the attack with stars in random direction
                    if (Timer % 60 == 0)
                    {
                        var rand = Main.rand.NextFloat(6.28f);

                        for (int k = 0; k < 10; k++)
                            Projectile.NewProjectile(null, NPC.Center, Vector2.UnitX.RotatedBy(rand + k / 10f * 6.28f) * 5, ModContent.ProjectileType<Star>(), 25, 0, Main.myPlayer);
                    }

                    if (Timer > 360)
                    {
                        Timer = 0;
                        State = Main.rand.Next(3);
                    }
                    break;
                case 2: // This is the attack with shooting the star
                    if (Timer == 60)
                    {
                        NPC.TargetClosest();
                        Projectile.NewProjectile(null, NPC.Center, NPC.Center.DirectionTo(Main.player[NPC.target].Center) * 10, ModContent.ProjectileType<Star>(), 25, 0, Main.myPlayer);
                    }

                    if (Timer > 60)
                    {
                        Timer = 0;
                        State = Main.rand.Next(3);
                    }
                    break;

            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) // This works
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Starfury, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.Star, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarEssence>(), 2, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementalShield>(), 50, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Sky.Chance * 2f;
        }
    }

    public class Star : ModProjectile // This is the star
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            for (int k = 0; k < 2; k++)
            {
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.FireworksRGB, Main.rand.NextVector2Circular(5, 5), 0, new Color(Main.rand.Next(255), Main.rand.Next(255), Main.rand.Next(255)));
                d.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor) // This draws the special effects of the star, do not remove this!!
        {
            var tex = ModContent.Request<Texture2D>(Texture).Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.velocity.ToRotation(), tex.Size() / 2f, Main.rand.NextFloat(0.05f), 0, 0);

            for (int k = 0; k < 2000; k++)
            {
                Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition + Vector2.One.RotatedBy(k / 2000f) * 15, null, Color.White * 0.01f, Projectile.velocity.ToRotation(), tex.Size() / 2f, Main.rand.NextFloat(0.05f), 0, 0);
            }

            return false;
        }
    }
}