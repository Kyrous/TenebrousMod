//using TenebrousMod.Items.Accessories;
//using TenebrousMod.Items.Food;
//using TenebrousMod.Items.Materials;
//using TenebrousMod.Items.Placeable;
//using TenebrousMod.Items.Weapons.Corruption;
//using TenebrousMod.Items.Weapons.Crimson;
// TenebrousMod.Items.Weapons.Granite;
//using TenebrousMod.Items.Weapons.Snow;
//using TenebrousMod.Items.Weapons.Space;
//using TenebrousMod.Items.Weapons.Surface;
//using TenebrousMod.Items.Weapons.Underground;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Global.NPCs.Hostile
{
    public class NPCDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            /*
            if (npc.type == NPCID.DemonEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxOne>(), 400));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DemonEye2)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxOne>(), 400));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DemonEyeOwl)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxOne>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DemonEyeSpaceship)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxOne>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.CataractEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxOne>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.CataractEye2)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxOne>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DialatedEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxOne>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DialatedEye2)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxOne>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.WanderingEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxOne>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meat>(), 10, 1, 1));
            }
            if (npc.type == NPCID.GraniteGolem && Main.hardMode)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Trisma>(), 4, 1, 1));
            }
            if (npc.type == NPCID.GraniteGolem)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxTwo>(), 400));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGreatGranite>(), 4, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GraniteShard>(), 2, 1, 2));
            }
            if (npc.type == NPCID.GraniteFlyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxTwo>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGreatGranite>(), 16, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GraniteShard>(), 2, 1, 2));
            }

            if (npc.type == NPCID.IceSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFive>(), 400));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceScepter>(), 100, 1, 1));
            }
            if (npc.type == NPCID.IceBat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFive>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceScepter>(), 100, 1, 1));
            }
            if (npc.type == NPCID.IceTortoise)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFive>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceScepter>(), 25, 1, 1));
            }
            if (npc.type == NPCID.IceElemental)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFive>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceScepter>(), 2, 1, 1));
            }
            if (npc.type == NPCID.SpikedIceSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFive>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceScepter>(), 100, 1, 1));
            }
            if (npc.type == NPCID.ArmedZombieEskimo)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFive>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceScepter>(), 75, 1, 1));
            }
            if (npc.type == NPCID.ZombieEskimo)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFive>(), 400));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceScepter>(), 100, 1, 1));
            }
            if (npc.type == NPCID.Crimslime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 50, 1, 1));
            }
            if (npc.type == NPCID.Herpling)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 50, 1, 1));
            }
            if (npc.type == NPCID.BloodFeeder)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 50, 1, 1));
            }
            if (npc.type == NPCID.BloodJelly)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 50, 1, 1));
            }
            if (npc.type == NPCID.BloodMummy)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 50, 1, 1));
            }
            if (npc.type == NPCID.DarkMummy)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 50, 1, 1));
            }
            if (npc.type == NPCID.LittleCrimslime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 50, 1, 1));
            }

            if (npc.type == NPCID.BloodCrawler)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 100, 1, 1));
            }
            if (npc.type == NPCID.BloodCrawlerWall)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 100, 1, 1));
            }
            if (npc.type == NPCID.CrimsonGoldfish)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 100, 1, 1));
            }
            if (npc.type == NPCID.FaceMonster)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 100, 1, 1));
            }
            if (npc.type == NPCID.Crimera)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 100, 1, 1));
            }
            if (npc.type == NPCID.BigCrimera)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 100, 1, 1));
            }
            if (npc.type == NPCID.LittleCrimera)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodlust>(), 100, 1, 1));
            }

            if (npc.type == NPCID.EaterofSouls)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStaff>(), 100, 1, 1));
            }
            if (npc.type == NPCID.BigEater)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStaff>(), 100, 1, 1));
            }
            if (npc.type == NPCID.LittleEater)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStaff>(), 100, 1, 1));
            }
            if (npc.type == NPCID.CorruptGoldfish)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStaff>(), 100, 1, 1));
            }
            if (npc.type == NPCID.DevourerHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStaff>(), 100, 1, 1));
            }

            if (npc.type == NPCID.Corruptor)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStaff>(), 50, 1, 1));
            }
            if (npc.type == NPCID.Slimeling)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStaff>(), 50, 1, 1));
            }
            if (npc.type == NPCID.Slimer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStaff>(), 50, 1, 1));
            }
            if (npc.type == NPCID.RedSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StaffOfTheStones>(), 100, 1, 1));
                if (Main.CurrentFrameFlags.ActivePlayersCount > 2)
                {
                    npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal, 150, 1, 2));
                }
            }
            if (npc.type == NPCID.YellowSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StaffOfTheStones>(), 100, 1, 1));
                if (Main.CurrentFrameFlags.ActivePlayersCount > 2)
                {
                    npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal, 150, 1, 2));
                }
            }
            if (npc.type == NPCID.PurpleSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StaffOfTheStones>(), 100, 1, 1));
                if (Main.CurrentFrameFlags.ActivePlayersCount > 2)
                {
                    npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal, 150, 1, 2));
                }
            }
            if (npc.type == NPCID.Skeleton)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StaffOfTheStones>(), 100, 1, 1));
                if (Main.CurrentFrameFlags.ActivePlayersCount > 2)
                {
                    npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal, 150, 1, 2));
                }
            }
            if (npc.type == NPCID.UndeadMiner)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StaffOfTheStones>(), 100, 1, 1));
                if (Main.CurrentFrameFlags.ActivePlayersCount > 2)
                {
                    npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal, 150, 1, 2));
                }
            }
            if (npc.type == NPCID.Harpy)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementalBreaker>(), 50, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarEssence>(), 2, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WingedYoyo>(), 25, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WingedJavelin>(), 25, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FeatherFan>(), 25, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FeatherStorm>(), 25, 1, 1));

            }
            if (npc.type == NPCID.WyvernHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementalBreaker>(), 50, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarEssence>(), 2, 1, 1));
            }
            if (npc.type == NPCID.Wraith)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WraithBlade>(), 50, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxThree>(), 400));

            }
            if (npc.type == NPCID.PossessedArmor)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxThree>(), 400));
            }

            if (npc.type == NPCID.ZombieMushroom)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFour>(), 400));
            }
            if (npc.type == NPCID.SporeSkeleton)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFour>(), 400));
            }
            if (npc.type == NPCID.FungiBulb)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFour>(), 400));
            }
            if (npc.type == NPCID.AnomuraFungus)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecretMusicBoxFour>(), 400));
            }
            */
        }
    }
}