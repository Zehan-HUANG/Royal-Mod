using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoyalMod.Projectiles
{
    public class ExecuteBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = -1;  // Infinite penetrate (we handle kill manually)
            Projectile.timeLeft = 90;   // Lifetime (1.5 sec)
            Projectile.tileCollide = false;  // No tile collision
            Projectile.ignoreWater = true;
            Projectile.light = 0.5f;  // Emit light
            Projectile.ArmorPenetration = 99999;  // Ignore armor
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            int targetIndex = (int)Projectile.ai[0];
            NPC target = Main.npc[targetIndex];
            
            Projectile.ai[1]++;  // Timer
            
            // First 30 frames: follow the target NPC
            if (Projectile.ai[1] < 30)
            {
                // Save offset from target center on first frame
                if (Projectile.ai[1] == 1)
                {
                    Projectile.localAI[0] = Projectile.Center.X - target.Center.X;
                    Projectile.localAI[1] = Projectile.Center.Y - target.Center.Y;
                    Projectile.velocity = Vector2.Zero;
                }
                
                // Follow target NPC (keep same offset)
                if (target.active)
                {
                    Projectile.Center = target.Center + new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
                }
            }
            // After 30 frames: launch toward target center (or original direction if target died)
            else if (Projectile.ai[1] == 30)
            {
                // Use saved offset direction (reversed) - works even if target died
                Vector2 direction = -new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
                direction.Normalize();
                Projectile.velocity = direction * 25f;  // Fast launch
                
                // Calculate total travel distance (spawn distance * 2)
                float spawnDistance = new Vector2(Projectile.localAI[0], Projectile.localAI[1]).Length();
                Projectile.ai[2] = spawnDistance * 2;  // Store total travel distance
            }
            // After launch: track distance traveled
            else if (Projectile.ai[1] > 30)
            {
                Projectile.ai[2] -= Projectile.velocity.Length();  // Subtract traveled distance
                
                // When traveled enough, dissipate
                if (Projectile.ai[2] <= 0)
                {
                    // Spawn light particles
                    for (int j = 0; j < 15; j++)
                    {
                        Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, DustID.GoldCoin);
                        dust.noGravity = true;
                        dust.scale = 1.5f;
                        dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
                    }
                    Projectile.Kill();
                    return;
                }
            }
            
            // Rotate to point toward target direction
            float rotation;
            if (Projectile.velocity == Vector2.Zero)
            {
                // When still, use saved offset direction (reversed = toward where target was)
                rotation = (-new Vector2(Projectile.localAI[0], Projectile.localAI[1])).ToRotation();
            }
            else
            {
                rotation = Projectile.velocity.ToRotation();
            }
            Projectile.rotation = rotation + MathHelper.PiOver4;
            
            // Golden dust trail
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GoldCoin);
                dust.noGravity = true;
                dust.scale = 1.2f;
                dust.velocity *= 0.3f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            // Golden glow
            return new Color(255, 215, 0, 150);
        }

        public override bool? CanHitNPC(NPC target)
        {
            // Only hit the target NPC stored in ai[0]
            int targetIndex = (int)Projectile.ai[0];
            if (target.whoAmI == targetIndex)
                return true;
            return false;
        }
    }
}
