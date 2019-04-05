// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/outline2" {
	Properties{
		_Color("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(.002, 0.03)) = .005
		_MainTex("Base (RGB)", 2D) = "white" { }
	_ToonShade("ToonShader Cubemap(RGB)", CUBE) = "" { }
	}

		SubShader{
		Pass{
		Cull front
		Tags{
		"LightMode" = "ForwardBase"
	}

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdbase

#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "Lighting.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float3 normal: NORMAL;
	};

	struct v2f {
		float4 position : SV_POSITION;
	};

	v2f vert(appdata v) {
		v2f o;
		float4 position = UnityObjectToClipPos(v.vertex);
		o.position = position + 0.05f * UnityObjectToClipPos(normalize(v.normal));
		return o;
	}

	fixed4 _Color2;

	fixed4 frag(v2f i) : SV_Target{
		fixed4 col = _Color2;
	col.a = 1.0;
	return col;
	}
		ENDCG
	}
	}

		Fallback "Toon/Basic"
}
