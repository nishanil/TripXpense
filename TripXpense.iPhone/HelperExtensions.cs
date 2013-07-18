using System;
using MonoTouch.UIKit;

namespace TripXpense.iPhone
{
	public static class HelperExtensions
	{

		// stolen from: https://github.com/xamarin/monotouch-element-pack/blob/master/ElementPack/RowBadgeElement.cs

		public static UIColor FromHexString( UIColor color, string hexColor)
		{
			int red = 0, green = 0, blue = 0;
			if (hexColor.Length == 6) {					    
				red = int.Parse (hexColor.Substring (0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);			
				green = int.Parse (hexColor.Substring (2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);			
				blue = int.Parse (hexColor.Substring (4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);				
			} else if (hexColor.Length == 3) {					    
				red = int.Parse (
					hexColor.Substring (0, 1) + hexColor.Substring (0, 1),
					System.Globalization.NumberStyles.AllowHexSpecifier
					);	
				green = int.Parse (
					hexColor.Substring (1, 1) + hexColor.Substring (1, 1),
					System.Globalization.NumberStyles.AllowHexSpecifier
					);
				blue = int.Parse (
					hexColor.Substring (2, 1) + hexColor.Substring (2, 1),
					System.Globalization.NumberStyles.AllowHexSpecifier
					);		
			}

			return UIColor.FromRGB (red, green, blue);;
		}
	}
}

