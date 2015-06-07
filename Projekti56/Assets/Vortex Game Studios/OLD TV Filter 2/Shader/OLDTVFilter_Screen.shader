/*
 * 
 * OLDTVFilter_Screen.shader 
 * Version 2.00
 *
 * [01/03/2015]
 * We recoded all the shader, lots of fixes and bug
 * 
 * Developed by Vortex Game Studios LTDA ME. (http://www.vortexstudios.com)
 * Authors:		Alexandre Ribeiro de Sa (@themonkeytail)
 * 
 */


Shader "Vortex Game Studios/OLD TV Filter 2/Screen Filter" {
	Properties {
		_ScreenHorizontal("Screen Horizontal Orientation", float) = 1.0
		_Mobile("Is on mobile", float) = 0.0

		_MainTex ("Base (RGB)", 2D) = "white" {}

		_Saturation ("Saturation", Range(0.0, 1.0)) = 0.0

		_ChromaticAberrationTex ("Chromatic Aberration (RGB)", 2D) = "black" {}
		_ChromaticAberrationMagnetude ("Chromatic Aberration Magnetude", Range(-1.0, 1.0)) = 0

		_NoiseTex ("Noise (RGB)", 2D) = "black" {}
		_NoiseMagnetude ("Noise Magnetude", Range(-1.0, 1.0)) = 0.5

		_Static ("Static (RGB)", 2D) = "black" {}
		_StaticMask ("Static Mask (RGB)", 2D) = "white" {}
		_StaticMagnetude ("Static Magnetude", Range(-1.0, 1.0)) = 0.5
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
			fixed _Mobile;

			// The main texture
			sampler2D _MainTex;

			// Color saturation
			fixed _Saturation;

			// Chromatic Aberration
			sampler2D _ChromaticAberrationTex;
			fixed _ChromaticAberrationMagnetude;

			// Static and Noise
			sampler2D _NoiseTex;
			fixed _NoiseMagnetude;

			sampler2D _StaticTex;
			sampler2D _StaticMask;
			fixed _StaticMagnetude;

			//fixed2 uvOrientation = fixed2( 1.0, 0.0 );

			struct v2f {
			    fixed4  pos : SV_POSITION;
			    fixed2  uv : TEXCOORD0;										// Main
				fixed2  uvChromaticAberration : TEXCOORD1;					// Chromatic Aberration
				fixed2  uvNoise : TEXCOORD2;								// Noise
				fixed2  uvStatic : TEXCOORD3;								// Static
				fixed2  uvStaticMask : TEXCOORD4;							// Static Mask
				//float2	uvNoise : TEXCOORD1;			
				//float2	uvAberration : TEXCOORD2;
				//float2	uvStaticMask : TEXCOORD3;
				fixed2  uvOrientation : TEXCOORD5;
			};

			float4 _MainTex_ST;
			float4 _ChromaticAberrationTex_ST;
			float4 _NoiseTex_ST;
			float4 _StaticTex_ST;
			float4 _StaticMask_ST;

			v2f vert(appdata_full v) {
    			v2f o;
    			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
    			o.uv = o.uvNoise = TRANSFORM_TEX (v.texcoord, _MainTex);

				o.uvChromaticAberration = TRANSFORM_TEX (v.texcoord, _ChromaticAberrationTex);
				o.uvNoise = TRANSFORM_TEX (v.texcoord, _NoiseTex);
				o.uvStatic = TRANSFORM_TEX (v.texcoord, _StaticTex);
				o.uvStaticMask = TRANSFORM_TEX (v.texcoord, _StaticMask);
				
				o.uvOrientation = fixed2( 1.0, 0.0 );

				// When we rotate the screen we will need to re-calculate some uvs things...
				if( _ScreenHorizontal == 0.0 ) {
					o.uvNoise = fixed2(o.uvNoise.y, o.uvNoise.x);
					o.uvStaticMask = fixed2(o.uvStaticMask.y, o.uvStaticMask.x);
					o.uvOrientation = fixed2(0.0, 1.0);
				}
    			return o;
			}

			fixed4 frag(v2f i) : COLOR {
				// Create the the chromatic aberration matrix
				fixed3 tvAberration = tex2D(_ChromaticAberrationTex, i.uvChromaticAberration)*_ChromaticAberrationMagnetude;

				// Screen orientation
				// Normally the green channel is less affected by compression , then use it when we just want a grayscale.
				fixed4 c = fixed4(1,1,1,1);
				fixed2 staticOffset = fixed2(0,0);
				fixed2 outUV;

				// Apply the static
				if( _ScreenHorizontal == 1.0 ) {
					staticOffset = fixed2( tex2D(_StaticTex, fixed2(i.uvStatic.y, 0.0)).g - 0.5, 0.0) * (_StaticMagnetude * tex2D(_StaticMask, -i.uvStaticMask).g);
				} else {
					staticOffset = fixed2( 0.0, tex2D(_StaticTex, fixed2(i.uvStatic.x, 0.0)).g - 0.5 ) * (_StaticMagnetude * tex2D(_StaticMask, -i.uvStaticMask).g);
				}
				outUV = i.uv+staticOffset;

				// Apply the chromatic aberration
				c.rgb = fixed3( tex2D(_MainTex, outUV-i.uvOrientation*tvAberration.r).r,
								tex2D(_MainTex, outUV).g,
								tex2D(_MainTex, outUV+i.uvOrientation*tvAberration.b).b );

				// Apply the noise
				c.rgb += tex2D(_NoiseTex, i.uvNoise) * _NoiseMagnetude;

				// Apply the saturation
				c.rgb = fixed3(lerp(c.rgb, dot(fixed3(0.2126,0.7152,0.0722), c.rgb), _Saturation));

				// Done \o/
				return c;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
