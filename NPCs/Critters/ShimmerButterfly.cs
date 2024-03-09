using Terraria.ID;
using Terraria.ModLoader.Utilities;
using Terraria.ModLoader;
using Terraria;
using TenebrousMod.TenebrousModSystem;

namespace TenebrousMod.NPCs.Critters
{
    public class ShimmerButterflyItem : ModItem
    {
        CritterItemLighting CIL = new CritterItemLighting();
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
            CIL.DropLighting(Item);
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


    }
}
