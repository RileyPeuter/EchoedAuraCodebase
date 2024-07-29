Shader "Unlit/MainMenuShader"{
    Properties{
       // _MainTex ("Texture", 2D) = "white" {}
    _ColorA("Color A", Color) = (1,1,1,1) 
    _ColorB("Color B", Color) = (1,1,1,1) 
    _ColorStart("Color Start", Range(0,1)) = 0
    _ColorEnd ("Color End", Range(0,1)) = 1 
   }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            float4 _ColorA;
            float4 _ColorB;
            float _ColorStart;
            float _ColorEnd;

            struct MeshData
            {
                
                float4 vertex : POSITION;
                float3 normals : NORMAL;
                float2 uv0 : TEXCOORD0;
            };

            struct Interpolators
            {
                float3 normal : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 uv : TEXCORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex =  UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normals);
                o.uv = v.uv0; //passthrough
                return o;
            }

            float InverseLerp(float a, float b, float v){
                return (v-a)/(b-a);
                }

            fixed4 frag (Interpolators i) : SV_Target
            {
                //float4 outColor = lerp(_ColorA, _ColorB, i.uv.x);
                float t = frac(i.uv.x * 5);
                return t;

                //return outColor ;
            }
            ENDCG
        }
    }
}
