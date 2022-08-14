Shader "Unlit/BombİndicatorShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _StartColor("StarColor",Color)=(1,1,1,1)
        _EndColor("EndColor",Color)=(1,1,1,1)
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
          

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _StartColor;
            float4 _EndColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
              
               
                float4 col = tex2D(_MainTex, i.uv);
                float2 blackAndWhite=col.xy;
               // float3 s =step(0.000001,blackAndWhite);
               //if(blackAndWhite<float3(0.5,0.5,0.5))
               //{
               //  float4 finalColor=lerp(_StartColor,_EndColor,i.uv.y);
               //  return finalColor;
               //}
             if(any(blackAndWhite<0.01)){
                 float4 finalColor=lerp(_StartColor,_EndColor,i.uv.y);
                 return finalColor;
                 }
               return float4(blackAndWhite.xxx,1);
            }
            ENDCG
        }
    }
}
