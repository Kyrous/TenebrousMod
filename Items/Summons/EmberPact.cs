using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TenebrousMod.NPCs.Bosses.Emberwing;
using TenebrousMod.Items.Materials;

namespace TenebrousMod.Items.Summons
{
    public class EmberPact : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.LightPurple;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Emberwing>()) && player.ZoneUnderworldHeight && NPC.downedPlantBoss;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<Emberwing>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }

            return true;

        }
        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GraniteShard>(1)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.Obsidian, 25)
                .AddIngredient(ItemID.Fireblossom, 1)
                .AddIngredient(ItemID.AshBlock, 25)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
