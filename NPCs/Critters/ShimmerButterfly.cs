using Terraria.ID;
using Terraria.ModLoader.Utilities;
using Terraria.ModLoader;
using Terraria;
using TenebrousMod.TenebrousModSystem;
using Terraria.GameContent.Bestiary;

namespace TenebrousMod.NPCs.Critters
{
    public class ShimmerButterflyItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 2);
            Item.useTime = 20;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.autoReuse = true;
            Item.bait = 15;
        }
        public override void PostUpdate()
        {
            CritterItemLighting.DropLighting(Item);
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<ShimmerCrittersPlayer>().shimmerBuff = true;
        }
        public override bool? UseItem(Player player)
        {
            int type = ModContent.NPCType<ShimmerButterfly>();
            NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, type);
            return true;
        }
    }
    public class ShimmerButterfly : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Butterfly);
            AnimationType = NPCID.Firefly;
            NPC.width = 24;
            NPC.height = 20;
            NPC.lifeRegen = 1;
            NPC.defense = 5;
            NPC.lifeMax = 50;
            NPC.catchItem = ModContent.ItemType<ShimmerButterflyItem>();
            NPC.aiStyle = 65;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.UndergroundJungle.Chance * 0.25f;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
				// Sets your NPC's flavor text in the bestiary.
                new FlavorTextBestiaryInfoElement("A really powerful butterfly powered by the shimmer capable of catching fish real easy."),
            });
        }
    }
}
