Shader "Unlit/QuakeSky"
{
	Properties
	{
		_PrimaryTexture ("Texture", 2D) = "white" {}
		_SecondaryTexture ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 camera_dir : TEXCOORD0;
			};

			sampler2D _PrimaryTexture;
			float4 _PrimaryTexture_ST;
			sampler2D _SecondaryTexture;
			float4 _SecondaryTexture_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.camera_dir = v.vertex - float4(_WorldSpaceCameraPos, 1.0f);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 dir = i.camera_dir;

				dir.y *= 3;
				dir = normalize(dir) * 6*63 / 128.0f;

				float scroll = 0;

				float2 texCoordFront = float2(scroll + dir.x, scroll - dir.z);

				scroll = scroll / 2.0f;

				float2 texCoordBack = float2(scroll + dir.x, scroll - dir.z);

				fixed4 frontCol = tex2D(_SecondaryTexture, texCoordFront);
				fixed4 backCol = tex2D(_PrimaryTexture, texCoordBack);

				fixed4 color = frontCol;

				if(frontCol.x + frontCol.y + frontCol.z < .01f)
				{
					color = backCol;
				}

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return color;
			}
			ENDCG
		}
	}
}
