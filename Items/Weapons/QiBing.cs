using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoyalMod.Items.Weapons
{
    public class QiBing : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 999999999;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 10f;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.ArmorPenetration = 999;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.QiBingSwing>();
            Item.shootSpeed = 1f;
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, 
            Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Vector2 velocity, int type, int damage, float knockback)
        {
            int proj = Projectile.NewProjectile(source, player.Center, Microsoft.Xna.Framework.Vector2.Zero, 
                type, damage, knockback, player.whoAmI);
            Main.projectile[proj].ai[0] = 0;
            Main.projectile[proj].ai[1] = Item.useAnimation;
            return false;
        }
    }
}
