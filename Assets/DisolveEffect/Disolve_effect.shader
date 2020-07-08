Shader "Custom/SHADER_blog_Dissolve_Texture"
{
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
 
        _DissolvePercentage("DissolvePercentage", Range(0,1)) = 0.5
        _ShowTexture("ShowTexture", Range(0,1)) = 0.0
    }
    SubShader{
        Tags{ "RenderType" = "Opaque" }
        LOD 200
        Pass {
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 position : POSITION;
                float2 uv: TEXCOORD0;
            };

            struct v2f {
                float4 position : SV_POSITION;
                float2 uv: TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert (appdata v) {
                v2f o;
                o.position = UnityObjectToClipPos(v.position);
                o.uv = v.uv;
                return o;
            }
            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input 
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        half _DissolvePercentage;
        half _ShowTexture;
        fixed4 _Color;
    
        void surf(Input IN, inout SurfaceOutputStandard o)
        {       
            // Albedo comes from a texture tinted by color
            half gradient = tex2D(_MainTex, IN.uv_MainTex).r;
            clip(gradient- _DissolvePercentage);
    
            fixed4 c = lerp(1, gradient, _ShowTexture) * _Color;
            o.Albedo = c.rgb;
    
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}