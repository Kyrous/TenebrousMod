using TenebrousMod.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Accessories
{
    public class ObscurumGlove : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 42;
            Item.value = 60000;
            Item.rare = ItemRarityID.LightPurple;
            Item.expert = true;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense -= 4;
            player.GetCritChance(DamageClass.Generic) += 1.10f;
            player.GetDamage(DamageClass.Generic) *= 1.07f;
            if (player.statLife < 0.25 * player.statLifeMax2)
                player.AddBuff(ModContent.BuffType<Empowered>(), 2);
        }
    }
}
