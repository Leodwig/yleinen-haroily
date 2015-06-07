/*
 * 
 * OLDTVFilter_Screen.shader 
 * Use this file to create a awesome old tv effect.
 * Horizontal scanlines
 * 
 * Version 2.00
 * 
 * Developed by Vortex Game Studios LTDA ME. (http://www.vortexstudios.com)
 * Authors:		Alexandre Ribeiro de Sa (@themonkeytail)
 * 
 */


Shader "Vortex Game Studios/OLD TV Filter 2/Tube Filter" {
	Properties {
		_ScreenHorizontal("Screen Horizontal Orientation", float) = 1.0

		_MainTex ("Base (RGB)", 2D) = "white" {}

		_Pixel ("Pixel (RGB)", 2D) = "white" {}
		_ScreenWidth ("Screen Width", float) = 0.0
		_ScreenHeight ("Screen Height", float) = 0.0

		_ScanLine ("ScanLine (RGB)", 2D) = "white" {}
		_ScanLineCount("ScanLine Count (Resolution Size)", float) = 100
		_ScanLineMin("ScanLine Min", float) = 0.0
		_ScanLineMax("ScanLine Max", float) = 1.0

		_Mask ("Mask (RGB)", 2D) = "white" {}
		_Reflex ("Reflex (RGB)", 2D) = "black" {}
		_ReflexMagnetude ("Reflex Magnetude", Range(0.0, 1.0)) = 0.5

		_Distortion("Distortion", Range(-1.0, 1.0)) = 0.1
	}
	
	SubShader {
		Tags { 
			"IgnoreProjector"="True"
			"RenderType"="Opaque"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		 }
		LOD 200
	
		pass { 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			// Set if we are hendering horizontal or vertical
			fixed _ScreenHorizontal;

			// The main texture
			sampler2D _MainTex;

			sampler2D _Pixel;
			float _ScreenWidth;
			float _ScreenHeight;
			// Tube scanlines
			sampler2D _ScanLine;				
			fixed _ScanLineCount;
			fixed _ScanLineMin;
			fixed _ScanLineMax;

			// Tube mask and reflection
			sampler2D _Mask;							// The TV rounded corner mask;
			sampler2D _Reflex;							// The TV Reflex on the screen;
			fixed _ReflexMagnetude;						// The TV Reflex on the screen magnetude;

			// Tube distortion
			fixed _Distortion;

			struct v2f {
			    float4  pos : SV_POSITION;
			    float2  uv : TEXCOORD0;
			};

			float4 _MainTex_ST;
			
			// Tube radial distortion
			fixed2 radialDistortion( fixed2 uv ) {
			    fixed2 cc = uv-0.5; 
			    fixed dt = dot(cc,cc)*_Distortion; 

				return uv+cc*(1.0+dt)*dt;; 
			}

			v2f vert(appdata_base v) {
    			v2f o;
    			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
    			o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);

    			return o;
			}

			fixed4 frag(v2f i) : COLOR {
				// Just a gohorse to keep the screen... on screen xD when you distort the screen)
				if( _Distortion != 0.0 ) {
					fixed minSize = _Distortion * 0.098;
					fixed maxSize = 1.0 - (minSize);

					i.uv *= (maxSize - minSize);
					i.uv += minSize;

					i.uv = radialDistortion(i.uv);
				}

				// Scanline stuff
				fixed scanLine;
				if( _ScreenHorizontal == 1.0 ) {
					scanLine = tex2D(_ScanLine, i.uv*_ScanLineCount).g;
				} else {
					scanLine = tex2D(_ScanLine, fixed2(i.uv.y, i.uv.x) *_ScanLineCount).g;
				}

				scanLine *= (_ScanLineMax - _ScanLineMin);
				scanLine += _ScanLineMin;

				// Apply tube filter
				// * (tex2D( _Pixel, fixed2( i.uv.x * _ScreenWidth, i.uv.y * _ScreenHeight ) ))
				fixed4 c = tex2D( _MainTex, i.uv ) * scanLine;

				fixed3 tvMask = tex2D(_Mask,i.uv).rgb;
				fixed3 tvReflex = tex2D(_Reflex,i.uv).rgb * _ReflexMagnetude;
				tvReflex = tvReflex*(1.0-((c.r+c.g+c.b)/3.0));
				c.rgb = (c.rgb + tvReflex) * tvMask;
				
				// Render everything \o/
				return c;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
