using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoyalMod.Items.Weapons
{
    public class RoyalSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.scale = 1.5f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.5f;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Execute threshold: 10% for normal enemies, 1% for bosses
            float executeThreshold = target.boss ? 0.01f : 0.10f;
            float healthPercent = (float)target.life / target.lifeMax;

            if (healthPercent <= executeThreshold && target.life > 0)
            {
                // Spawn 6 golden blades from around the enemy toward its center
                int projectileType = ModContent.ProjectileType<Projectiles.ExecuteBlade>();
                int executeDamage = target.life;  // One hit kill
                float speed = 15f;
                float spawnDistance = 100f;  // Spawn distance from target

                for (int i = 0; i < 6; i++)
                {
                    float angle = MathHelper.TwoPi / 6 * i;  // 60 degrees apart
                    Vector2 offset = new Vector2(spawnDistance, 0).RotatedBy(angle);
                    Vector2 spawnPos = target.Center + offset;  // Spawn around the enemy
                    Vector2 velocity = -offset;  // Toward enemy center
                    velocity.Normalize();
                    velocity *= speed;
                    
                    int proj = Projectile.NewProjectile(
                        player.GetSource_ItemUse(Item),
                        spawnPos,
                        velocity,
                        projectileType,
                        executeDamage,
                        0f,
                        player.whoAmI
                    );
                    Main.projectile[proj].ai[0] = target.whoAmI;  // Store target NPC index
                }

                // Play execute sound
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item71, target.Center);
            }
        }

        public override void AddRecipes()
        {
            // Recipe with Gold Broadsword
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBroadsword, 1);
            recipe.AddIngredient(ModContent.ItemType<Materials.RoyalSoul>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            // Recipe with Platinum Broadsword
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.PlatinumBroadsword, 1);
            recipe2.AddIngredient(ModContent.ItemType<Materials.RoyalSoul>(), 1);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}
