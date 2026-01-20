using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoyalMod.Items.Weapons
{
    public class QiBing : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 9999;
            Item.DamageType = DamageClass.Melee;
            Item.width = 200;
            Item.height = 200;
            Item.scale = 5f;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10f;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.ArmorPenetration = 999;
        }
    }
}
