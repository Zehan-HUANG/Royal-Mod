using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace crown
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class crown : Mod
	{
		public override void PostSetupContent()
		{
			// Register RoyalSoul animation: 4 frames, 5 ticks per frame
			Main.RegisterItemAnimation(
				ModContent.ItemType<Items.Materials.RoyalSoul>(),
				new DrawAnimationVertical(5, 4)
			);
		}
	}
}
