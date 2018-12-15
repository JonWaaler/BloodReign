Shader "Custom/PostProcess"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_offset("offset float", float) = 0.002
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _offset;

			fixed4 frag (v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv);
				//// just invert the colors
				//fixed4 col1 = clamp(0,1,sin(cos(sin(col)*_Time) * sin(_Time)));
				//col.rgb = col1 * col1 / col *2;				
				
				fixed4 colR = tex2D(_MainTex, i.uv - float2(-_offset,0));
				fixed4 colG = tex2D(_MainTex, i.uv - float2(_offset, 0));
				fixed4 colB = tex2D(_MainTex, i.uv - float2(0, -_offset));

				return fixed4(colR.r, colG.g, colB.b, 1);
			}
			ENDCG
		}
	}
}
