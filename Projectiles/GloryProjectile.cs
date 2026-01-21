using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoyalMod.Projectiles
{
    public class GloryProjectile : ModProjectile
    {
        public override string Texture => "RoyalMod/Projectiles/ExecuteBlade";

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            DrawOffsetX = -14;
            DrawOriginOffsetY = -14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.light = 0.3f;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

            // Golden dust trail
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GoldCoin);
                dust.noGravity = true;
                dust.scale = 1.0f;
                dust.velocity *= 0.2f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 215, 0, 180);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Execute threshold: 10% for normal enemies, 1% for bosses
            float executeThreshold = target.boss ? 0.01f : 0.10f;
            float healthPercent = (float)target.life / target.lifeMax;
            bool isExecute = healthPercent <= executeThreshold && target.life > 0;

            // Always spawn 6 blades
            int projectileType = ModContent.ProjectileType<ExecuteBlade>();
            float spawnDistance = 100f;

            for (int i = 0; i < 6; i++)
            {
                // Execute: all blades deal target.life damage
                // Non-execute: only first blade deals 20 damage, others deal 0
                int bladeDamage;
                if (isExecute)
                {
                    bladeDamage = target.life;
                }
                else
                {
                    bladeDamage = (i == 0) ? 20 : 0;  // Only first blade deals damage
                }

                float angle = MathHelper.TwoPi / 6 * i;
                Vector2 offset = new Vector2(spawnDistance, 0).RotatedBy(angle);
                Vector2 spawnPos = target.Center + offset;
                Vector2 velocity = -offset;
                velocity.Normalize();
                velocity *= 15f;

                int proj = Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    spawnPos,
                    velocity,
                    projectileType,
                    bladeDamage,
                    0f,
                    Projectile.owner
                );
                Main.projectile[proj].ai[0] = target.whoAmI;
            }

            // Play sound
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item71, target.Center);
        }

        public override void OnKill(int timeLeft)
        {
            // Spawn particles on death
            for (int i = 0; i < 8; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, DustID.GoldCoin);
                dust.noGravity = true;
                dust.scale = 1.2f;
                dust.velocity = Main.rand.NextVector2Circular(4f, 4f);
            }
        }
    }
}
