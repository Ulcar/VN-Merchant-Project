Shader "Unlit/RayMarch"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _CubePos("Cube Position", Vector) = (0,0,0,0)
        _CubePos2("Cube Position2", Vector) = (0,0,0,0)
        _BlendStrength("Blend strength", Float) = 1
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


#define MAX_STEPS 1000
#define MAX_DIST 1000
#define SURF_DIST 1e-4

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
                float3 cameraPos : TEXCOORD1;
                float3 hitPos : TEXCOORD2;
            };

 

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _CubePos;
            float4 _CubePos2;
            float _BlendStrength;

            float intersectSDF(float distA, float distB) {
                return max(distA, distB);
            }

            float unionSDF(float distA, float distB) {
                return min(distA, distB);
            }

            float differenceSDF(float distA, float distB) {
                return max(distA, -distB);
            }

            float sphereSDF(float3 p, float3 centre, float radius) {
                return distance(p, centre) - radius;
            }


            float opSmoothIntersection(float d1, float d2, float k) {
                float h = clamp(0.5 - 0.5 * (d2 - d1) / k, 0.0, 1.0);
                return lerp(d2, d1, h) - k * h * (1.0 - h);
            }

            float cubeSDF(float3 p, float3 centre, float3 size) {
                float3 o = abs(p - centre) - size;
                float ud = length(max(o, 0));
                float n = max(max(min(o.x, 0), min(o.y, 0)), min(o.z, 0));
                return ud + n;
            }

            float CylinderSDF(float3 eye, float3 centre, float2 h) {
                float2 d = abs(float2(length((eye).xz), eye.y)) - h;
                return length(max(d, 0.0)) + max(min(d.x, 0), min(d.y, 0));
            }

            float sdTorus(float3 p, float2 t)
            {
                float2 q = float2(length(p.xz) - t.x, p.y);
                return length(q) - t.y;
            }

            float sceneSDF(float3 samplePoint) {
                float3 trans = float3(10, 0, 0);
                float trans2 = float3(-12.1, 0, 0);
                float3 scale = float3(10, 10, 10);
                float sphereDist = sphereSDF(samplePoint / 1.2, _CubePos, 10) * 1.2;
                float cubeDist = cubeSDF(samplePoint, trans, scale) * 1.2;
                float torusDist = sdTorus(samplePoint, 10);
                float cylinderDist = CylinderSDF(samplePoint, trans, scale.xy);
                float sphere2 = sphereSDF(samplePoint / 1.2, trans, 10) * 1.2;
                float sphere3 = sphereSDF(samplePoint / 1.2,_CubePos2 , 10) * 1.2;
              //  return cylinderDist;
                float SDF2 = unionSDF(opSmoothIntersection(sphere2, sphereDist, _BlendStrength), sphereDist);
                float SDF3 = unionSDF(SDF2, sphere3);
                float SDF4 = unionSDF(opSmoothIntersection(sphere2, sphere3, _BlendStrength), SDF3);
                float SDF5 = unionSDF(opSmoothIntersection(sphereDist, sphere3, _BlendStrength), SDF4);
                return sphereDist;
            }



            float getDist(float3 p) 
            {
                float distance = sceneSDF(p);
                return distance;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.cameraPos = _WorldSpaceCameraPos;
                o.hitPos = mul(unity_ObjectToWorld, v.vertex);
              //  UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            float3 GetNormal(float3 p) 
            {
                float2 offset = float2(1, 0);
                float3 n = getDist(p) - float3(
                    getDist(p - offset.xyy),
                    getDist(p - offset.yxy),
                    getDist(p - offset.yyx)
                    );
                return normalize(n);
            }

            float Raymarch(float3 ro, float3 rd) 
            {
                float dO = 0;
                float dS = 0;
                for (int i = 0; i < MAX_STEPS; i++) 
                {
                    float3 p = ro + dO * rd;
                    dS = getDist(p);
                    dO += dS;
                    if (dS < SURF_DIST || dO > MAX_DIST)
                    {
                        
                        break;
                    }

                }

                return dO;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                float2 uv = i.uv -.5;
                float3 ro = i.cameraPos;
                float3 rd = normalize(i.hitPos - ro);

                float distance = Raymarch(ro, rd);
                // sample the texture
                fixed4 col = 0;
               // col.rgb = rd;
                if (distance < MAX_DIST) 
                {
                    float3 p = ro + rd * distance;
                    float3 n = GetNormal(p);
                    col.rgb = n;
                }

                else 
                {
                    discard;
                }

               

                return col;
            }
            ENDCG
        }
    }
}
