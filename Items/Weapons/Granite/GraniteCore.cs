using System;
using TenebrousMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebrousMod.Items.Weapons.Granite
{
    public class GraniteCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.damage = 9;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 4;
            Item.channel = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 33);
            Item.shoot = ModContent.ProjectileType<GraniteCoreProj>();
            Item.shootSpeed = 16f;
        }
        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };
        public override bool AllowPrefix(int pre)
        {
            if (Array.IndexOf(unwantedPrefixes, pre) > -1)
            {
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Granite, 15)
                .AddIngredient<GraniteShard>(1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class GraniteCoreProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13f;
        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = -1;
        }
    }
}
