using Terraria.ModLoader;
using Terraria;

namespace TenebrousMod.Buffs
{
    public class Empowered : ModBuff
    {
        public static readonly float DamageBonus = 1.10f;
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= DamageBonus;
            player.statDefense *= 5;
        }
    }
}
