using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TenebrousMod.NPCs.Bosses.Emberwing;
using TenebrousMod.Items.Materials;
using TenebrousMod.NPCs.Bosses.RiptideRavager;

namespace TenebrousMod.Items.Summons
{
    public class RavagedScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 58;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<RiptideRavager>()) && player.ZoneBeach;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<RiptideRavager>();

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
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Coral, 5)
                .AddIngredient(ItemID.SharkFin, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
