using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using TenebrousMod.TenebrousModSystem;
using Microsoft.Xna.Framework;
using TenebrousMod.Items.Materials;

namespace TenebrousMod.Items.Armor.SpaceSet
{
    [AutoloadEquip(EquipType.Body)]
    public class SpaceChestplate : ModItem
    {

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return WeaponLighting.LightingOnGround(Item, 1, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
        public override void PostUpdate()
        {
            WeaponLighting.PostLighting(Item, 1);
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 1.04f;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.Star, 20)
                .AddIngredient(ModContent.ItemType<StarEssence>(), 1)
                 .AddTile(TileID.Anvils)
                 .Register();
        }
        [AutoloadEquip(EquipType.Head)]
        public class SpaceHeadgear : ModItem
        {
            public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
            {
                return WeaponLighting.LightingOnGround(Item, 1, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
            }
            public override void PostUpdate()
            {
                WeaponLighting.PostLighting(Item, 1);
            }
            public override void SetStaticDefaults()
            {
                ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;

            }

            public override void SetDefaults()
            {
                Item.width = 32;
                Item.height = 24;
                Item.value = Item.sellPrice(gold: 1, silver: 25);
                Item.rare = ItemRarityID.Green;
                Item.defense = 5;
            }
            public override void UpdateEquip(Player player)
            {
                player.GetDamage(DamageClass.Magic) *= 1.10f;
            }
            public override bool IsArmorSet(Item head, Item body, Item legs)
            {
                return body.type == ModContent.ItemType<SpaceChestplate>() && legs.type == ModContent.ItemType<SpaceLeggings>();
            }
            public override void UpdateArmorSet(Player player)
            {

                player.setBonus = "Max Mana increased by 40";
                player.statManaMax2 += 40;
            }

            public override void AddRecipes()
            {
                CreateRecipe().AddIngredient(ItemID.Star, 15)
                     .AddIngredient(ModContent.ItemType<StarEssence>(), 1)
                     .AddTile(TileID.Anvils)
                     .Register();
            }
        }
        [AutoloadEquip(EquipType.Legs)]
        public class SpaceLeggings : ModItem
        {
            public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
            {
                return WeaponLighting.LightingOnGround(Item, 1, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
            }
            public override void PostUpdate()
            {
                WeaponLighting.PostLighting(Item, 1);
            }

            public override void SetDefaults()
            {
                Item.width = 22;
                Item.height = 18;
                Item.value = Item.sellPrice(gold: 1);
                Item.rare = ItemRarityID.Green;
                Item.defense = 5;
            }

            public override void UpdateEquip(Player player)
            {
                player.moveSpeed += 1.10f;
                player.noFallDmg = true;
            }
            public override void AddRecipes()
            {
                CreateRecipe().AddIngredient(ItemID.Star, 10)
                     .AddIngredient(ModContent.ItemType<StarEssence>(), 1)
                     .AddTile(TileID.Anvils)
                     .Register();
            }
        }   
    }
}
