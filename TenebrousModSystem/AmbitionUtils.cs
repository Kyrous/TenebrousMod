using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.Physics;
using Terraria.GameContent.Liquid;
using Terraria.Enums;
using Terraria.GameContent.NetModules;

namespace TenebrousMod.TenebrousModSystem
{
    public class AmbitionUtils
    {
        /// <summary>
        /// This helps the enemy know what direction the player is from the NPC.
        /// </summary>
        public static int FaceTarget(NPC npc, Player player) => npc.direction = npc.spriteDirection = npc.Center.X < player.Center.X ? 1 : -1;
        /// <summary>
        /// The Boss Awake Message (You can change it in localization).
        /// </summary>
        public static void BossAwakenMessage(int npcIndex)
        {
            string typeName = Main.npc[npcIndex].TypeName;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(Language.GetTextValue("Announcement.HasAwoken", typeName), new Color(175, 75, 255));
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[] { Main.npc[npcIndex].GetTypeNetName() }), new Color(175, 75, 255));
            }
        }
        /// <summary>
        /// file represents the file path to the SFX.
        /// </summary>
        public static SoundStyle? Sound(string file)
        {
            SoundStyle sound = new(file);
            SoundEngine.PlaySound(sound);
            return sound;
        }
        /// <summary>
        /// LightAmount is how much light there will be.
        /// </summary>
        public static bool ItemLightingOnGround(Item Item, int LightAmount, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * LightAmount);
            Texture2D texture = TextureAssets.Item[Item.type].Value;
            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
            {
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            }
            else
            {
                frame = texture.Frame();
            }

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.35f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(255, 175, 247, 40), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.30f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 2f).RotatedBy(radians) * time, frame, new Color(255, 155, 241, 55), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }
}
}