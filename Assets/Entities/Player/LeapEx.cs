using UnityEngine;
using System.Collections;
using Leap;

namespace Leap
{
	public static class LeapEx
	{
		public static Vector ToScreenPoint (this Vector v, Screen s)
		{
			var screenX = (int)(v.x * s.WidthPixels);
			var screenY = (int)(s.HeightPixels - v.y * s.HeightPixels);

			return new Vector (screenX, screenY, 0);
		}
		
		public static Vector3 ToVector3 (this Vector v)
		{
			return new Vector3(
				v.x,
				v.y,
				v.z
			);
		}
	}
}
