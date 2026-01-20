using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoyalMod.Items.Accessories
{
    public class JeweledCrown : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 1;           // +1 defense
            player.moveSpeed += 0.01f;         // +1% move speed
            player.GetDamage(DamageClass.Generic) += 0.01f;  // +1% damage
            player.GetCritChance(DamageClass.Generic) += 1;  // +1% crit chance
            player.GetAttackSpeed(DamageClass.Generic) += 0.01f;  // +1% attack speed
            player.lifeRegen += 1;             // +1 life regeneration
            player.manaCost -= 0.01f;          // -1% mana cost
        }

        public override void AddRecipes()
        {
            // Recipe with Gold Crown + 6 gems
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldCrown, 1);
            recipe.AddIngredient(ItemID.Amber, 1);       // Orange
            recipe.AddIngredient(ItemID.Topaz, 1);       // Yellow
            recipe.AddIngredient(ItemID.Diamond, 1);     // White
            recipe.AddIngredient(ItemID.Emerald, 1);     // Green
            recipe.AddIngredient(ItemID.Sapphire, 1);    // Blue
            recipe.AddIngredient(ItemID.Amethyst, 1);    // Purple
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            // Recipe with Platinum Crown + 6 gems
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.PlatinumCrown, 1);
            recipe2.AddIngredient(ItemID.Amber, 1);
            recipe2.AddIngredient(ItemID.Topaz, 1);
            recipe2.AddIngredient(ItemID.Diamond, 1);
            recipe2.AddIngredient(ItemID.Emerald, 1);
            recipe2.AddIngredient(ItemID.Sapphire, 1);
            recipe2.AddIngredient(ItemID.Amethyst, 1);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}
