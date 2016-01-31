// Shader created with Shader Forge v1.17 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.17;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31721,y:32351,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a4cdca73d61814d33ac1587f6c163bca,ntxv:0,isnm:False|UVIN-4792-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32476,y:32794,varname:node_2393,prsc:2|A-6656-OUT,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32084,y:32769,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32096,y:32946,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Append,id:4309,x:31044,y:32147,varname:node_4309,prsc:2|A-9822-OUT,B-8217-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9822,x:30880,y:32053,ptovrint:False,ptlb:U_speed,ptin:_U_speed,varname:node_9822,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:8217,x:30809,y:32223,ptovrint:False,ptlb:V_speed,ptin:_V_speed,varname:node_8217,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Time,id:5304,x:30959,y:32461,varname:node_5304,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5156,x:31259,y:32192,varname:node_5156,prsc:2|A-4309-OUT,B-5304-T;n:type:ShaderForge.SFN_Add,id:4792,x:31520,y:32276,varname:node_4792,prsc:2|A-5156-OUT,B-2261-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2261,x:31269,y:32402,varname:node_2261,prsc:2,uv:0;n:type:ShaderForge.SFN_Sin,id:5378,x:31892,y:31667,varname:node_5378,prsc:2|IN-578-OUT;n:type:ShaderForge.SFN_Time,id:3229,x:31363,y:31902,varname:node_3229,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:1039,x:31432,y:31751,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_1039,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:578,x:31626,y:31874,varname:node_578,prsc:2|A-1039-OUT,B-3229-T;n:type:ShaderForge.SFN_Multiply,id:6656,x:32158,y:32333,varname:node_6656,prsc:2|A-309-OUT,B-6074-RGB;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:309,x:32108,y:31886,varname:node_309,prsc:2|IN-5378-OUT,IMIN-3053-OUT,IMAX-9503-OUT,OMIN-147-OUT,OMAX-65-OUT;n:type:ShaderForge.SFN_ValueProperty,id:147,x:31824,y:32042,ptovrint:False,ptlb:glow min,ptin:_glowmin,varname:node_147,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:65,x:31824,y:32139,ptovrint:False,ptlb:glow max,ptin:_glowmax,varname:node_65,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:3053,x:31824,y:31886,varname:node_3053,prsc:2,v1:-1;n:type:ShaderForge.SFN_Vector1,id:9503,x:31843,y:31951,varname:node_9503,prsc:2,v1:1;proporder:6074-797-9822-8217-1039-147-65;pass:END;sub:END;*/

Shader "Shader Forge/paralaxe" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _U_speed ("U_speed", Float ) = 0
        _V_speed ("V_speed", Float ) = 0
        _speed ("speed", Float ) = 1
        _glowmin ("glow min", Float ) = 0.2
        _glowmax ("glow max", Float ) = 1
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _U_speed;
            uniform float _V_speed;
            uniform float _speed;
            uniform float _glowmin;
            uniform float _glowmax;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_3229 = _Time + _TimeEditor;
                float node_3053 = (-1.0);
                float4 node_5304 = _Time + _TimeEditor;
                float2 node_4792 = ((float2(_U_speed,_V_speed)*node_5304.g)+i.uv0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4792, _MainTex));
                float3 emissive = (((_glowmin + ( (sin((_speed*node_3229.g)) - node_3053) * (_glowmax - _glowmin) ) / (1.0 - node_3053))*_MainTex_var.rgb)*i.vertexColor.rgb*_TintColor.rgb*2.0);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
