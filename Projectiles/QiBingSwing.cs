using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace RoyalMod.Projectiles
{
    public class QiBingSwing : ModProjectile
    {
        public override string Texture => "RoyalMod/Items/Weapons/QiBing";

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ArmorPenetration = 999;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            
            Projectile.Center = player.Center;
            Projectile.spriteDirection = player.direction;
            
            float progress = Projectile.ai[0] / Projectile.ai[1];
            if (player.direction == 1)
            {
                Projectile.rotation = MathHelper.TwoPi * progress - MathHelper.PiOver2;
            }
            else
            {
                Projectile.rotation = -MathHelper.TwoPi * progress + MathHelper.PiOver2;
            }
            
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= Projectile.ai[1])
            {
                Projectile.Kill();
            }
            
            if (player.itemAnimation > 0)
            {
                Projectile.timeLeft = 2;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            float scale = 5f;
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            
            Vector2 origin;
            SpriteEffects effects;
            
            if (Projectile.spriteDirection == 1)
            {
                origin = new Vector2(0, texture.Height);
                effects = SpriteEffects.None;
            }
            else
            {
                origin = new Vector2(texture.Width, texture.Height);
                effects = SpriteEffects.FlipHorizontally;
            }
            
            Main.EntitySpriteDraw(
                texture,
                drawPos,
                null,
                lightColor,
                Projectile.rotation,
                origin,
                scale,
                effects,
                0
            );
            
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float radius = 200f;
            float tipAngle;
            
            if (Projectile.spriteDirection == 1)
            {
                tipAngle = Projectile.rotation + MathHelper.PiOver4;
            }
            else
            {
                tipAngle = Projectile.rotation - MathHelper.PiOver4;
            }
            
            Vector2 swordTip = Projectile.Center + tipAngle.ToRotationVector2() * radius;
            
            float point = 0f;
            return Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(),
                targetHitbox.Size(),
                Projectile.Center,
                swordTip,
                40f,
                ref point
            );
        }
    }
}
