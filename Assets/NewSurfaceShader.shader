Shader "Custom/NewSurfaceShader" {
	Properties {
		

		_MainTex("Main Tex",2D) = "white" {}
		_FlagColor("Flag Color",Color) = (1,1,1,1)
		_Frequency("Frequenecy", float) = 1
		_AmplitudesStrength("Amplitude Strength",float) = 1
		_InvWaveLength("Inverse wave Length",float) = 1
		_Fold("Fold",Range(0.0,2.0)) = 0.5
		_RampTex("Ramp Texture",2D) = "white"{}
	}
		SubShader{
			Tags{"DisableBatching" = "True" "RenderType" = "Opaque"}

			Pass{
				Tags{"LightMode" = "ForwardBase"}
				Cull Off

				CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag

	#include "UnityCG.cginc"

		sampler2D _MainTex;
		float4 _MainTex_ST;
		float4 _FlagColor;
		float _Frequency;
		float _AmplitudeStrength;
		float _InvWaveLength;
		float _Fold;
		sampler2D _RampTex;

		struct a2v {
			float4 vertex : POSITION;
			float4 texcoord : TEXCOORD0;

		};

		struct v2f {
			float4 pos :SV_POSITION;
			float2 uv:TEXCOORD0;
		};

		v2f vert(a2v v) {
			v2f o;
			o.uv = v.texcoord.xy;
			float4 offset;
			offset.xyzw = float4(0.0, 0.0, 0.0, 0.0);
			float4 v_before = mul(_Object2World, v.vertex);
			offset.y = _AmplitudeStrength * sin(_Frequency * _Time.y + (v_before.x + v_before.y * _Fold) * _InvWaveLength) * o.uv.x;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex + offset);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			return o;
		}
		
		fixed4 frag(v2f i) :SV_Target{
			fixed4 col = tex2D(_MainTex,i.uv);
		col.rgb *= _FlagColor.rbg;
		return col;
		
		}
			ENDCG
		}


	}
	FallBack "VertexLit"
}
