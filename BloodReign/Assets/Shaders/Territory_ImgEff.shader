Shader "Custom/Territory_ImgEff"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	_ColorPurple ("Color Purple", Color) = (1,1,1,1)
	_ColorMain ("Color MAIN", Color) = (1,0,0,1)
		_ScrollSpeeds ("Scroll speed", vector) = (-5, -20, 0, 0)
	}
	SubShader
	{
		// No culling or depth
		Cull Back ZWrite On ZTest LEqual

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.uv = v.uv - 0.5f;

				//o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			fixed4 _ColorPurple;
			float4 _ScrollSpeeds;
			float4 _MainTex_ST;
			fixed4 _ColorMain;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 polar = float2(
					atan2(sin(i.uv.y*_Time.y), i.uv.x) / (2.0f * 3.141592653589f), // angle
					length(i.uv)                                    // radius
					);

				polar *= _MainTex_ST.xy;

				polar += _ScrollSpeeds.xy * (_Time.x /4);

				fixed4 col = tex2D(_MainTex, polar);
				col = lerp(col, fixed4(1, 1, 1, 1), 0.9);

				col *= lerp(_ColorPurple, _ColorMain, sin(_Time.x));

				// just invert the colors
				return col;
			}
			ENDCG
		}
	}
}
