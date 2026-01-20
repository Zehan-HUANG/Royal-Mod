using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace crown.Items.Weapons
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
            Item.value = Item.sellPrice(silver: 18);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            // Execute threshold: 10% for normal enemies, 1% for bosses
            float executeThreshold = target.boss ? 0.01f : 0.10f;
            float healthPercent = (float)target.life / target.lifeMax;

            if (healthPercent <= executeThreshold)
            {
                modifiers.FlatBonusDamage += target.life;
                modifiers.ArmorPenetration += 9999;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBroadsword, 1);
            recipe.AddIngredient(ModContent.ItemType<Materials.RoyalSoul>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
