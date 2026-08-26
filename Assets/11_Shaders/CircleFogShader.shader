Shader "UI/CircleFogShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Center ("Center (Screen UV)", Vector) = (0.5, 0.5, 0, 0)
        _Radius ("Radius", Range(0, 1)) = 0.25
        _Softness ("Softness", Range(0.001, 0.5)) = 0.05
        _Aspect ("Aspect Ratio (X/Y)", Float) = 1.777777
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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

            float4 _Center;
            float _Radius;
            float _Softness;
            float _Aspect;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 해상도 비율(Aspect Ratio) 보정으로 원이 타원형으로 찌그러지는 현상 방지
                float2 uvOffset = i.uv - _Center.xy;
                uvOffset.x *= _Aspect;

                // 중심점과의 거리 계산
                float dist = length(uvOffset);

                // 원 내부(dist < _Radius)는 alpha 0(투명), 원 외부(dist > _Radius)는 alpha 1(검은색)
                float alpha = smoothstep(_Radius, _Radius + _Softness, dist);

                return fixed4(0, 0, 0, alpha); // 검은색 배경 + 알파값 적용
            }
            ENDCG
        }
    }
}