Shader "Unlit/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
 
		 // Диффузный
		_Diffuse ("Diffuse", COLOR) = (1,1,1,1)
 
		 // Эффект обводки
		_OutlineColor ("Outline Color", COLOR) = (0,0,0,1)
		_OutlineScale ("Outline Scale", Range(0,1)) = 0.001
    }
    SubShader
    {
		 // "Queue" = "Geometry + 1000" +1000, чтобы рендеринг скайбокса не перекрывал обводку в Game, или измените значение на "Queue" = "Transparent"
        Tags { "Queue" = "Geometry+1000" "RenderType"="Opaque" }
        LOD 100
 
        Pass{
			Name "Outline"
			 ZWrite off // Отключите глубокую запись, чтобы при нормальной записи следующего канала предыдущий внутренний штрих был перезаписан (вы можете увидеть внутренний штрих, закомментировав его), чтобы гарантировать, что отображается только контур
			Cull Front
			CGPROGRAM
 
			#pragma vertex vert
            #pragma fragment frag
			#include "UnityCG.cginc"
 
			float4 _OutlineColor;
			float _OutlineScale;
 
			struct v2f{
				float4 vertex : SV_POSITION;
			};
 
			v2f vert(appdata_base v){
				v2f o;
				 // Метод обводки 1: Нормальное расширение на объектной модели изменяет положение вершины
				v.vertex.xyz += v.normal * _OutlineScale;
				o.vertex = UnityObjectToClipPos(v.vertex);
 
				 // Метод обводки 2: Нормальное расширение изменяет положение вершины под полем обзора View
				//float4 pos = mul(UNITY_MATRIX_V,mul(unity_ObjectToWorld,v.vertex));
				//float3 normal = normalize(mul((float3x3)UNITY_MATRIX_MV,v.normal));
				//pos += float4(normal,0)* _OutlineScale;
				//o.vertex = mul(UNITY_MATRIX_P,pos);
 
				 // Метод обводки третий: изменение положения вершины путем удлинения нормальной линии под областью отсечения
				//o.vertex = UnityObjectToClipPos(v.vertex);
				//float3 viewNormal = normalize(mul((float3x3)UNITY_MATRIX_MV,v.normal));
				//float2 clipNormal = normalize(TransformViewToProjection(viewNormal.xy));
				//o.vertex.xy += clipNormal * _OutlineScale;
				return o;
			}
 
			fixed4 frag(v2f i):SV_Target{
			
				return _OutlineColor;
			}
 
			ENDCG
		}
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        
 
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
 
  
 
            struct v2f
            {
 
                float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal:TEXCOORD1;
				float3 worldPos:TEXCOORD2;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Diffuse;
 
            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
				 // окружающий свет
				float3 ambient = UNITY_LIGHTMODEL_AMBIENT;
               
				 // Истинный цвет текстуры
                fixed3 albedo = tex2D(_MainTex, i.uv).rgb;
 
				 // Диффузный
				fixed3 worldLightDir = UnityWorldSpaceLightDir(i.worldPos);
				float halfLambert = dot(worldLightDir,i.worldNormal) * 0.5 + 0.5;
 
				 // финальное диффузное отражение
				fixed3 diffuse = _LightColor0.rgb * albedo  * _Diffuse.rgb * halfLambert;
 
                return fixed4(ambient+diffuse,1);
            }
            ENDCG
        }
    }
 
	FallBack "DIFFUSE"
}
