// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:7,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-8182-RGB,clip-8182-A,voffset-9968-OUT;n:type:ShaderForge.SFN_Tex2d,id:8182,x:32404,y:32601,ptovrint:False,ptlb:node_8182,ptin:_node_8182,varname:node_8182,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f13bbc1a5249176478ca3aad2a5c746b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:273,x:32226,y:32832,varname:node_273,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9968,x:32429,y:32867,varname:node_9968,prsc:2|A-273-R,B-5986-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8654,x:32127,y:33315,ptovrint:False,ptlb:Intensty,ptin:_Intensty,varname:node_8654,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Time,id:7160,x:31723,y:33522,varname:node_7160,prsc:2;n:type:ShaderForge.SFN_Cos,id:6080,x:31901,y:33526,varname:node_6080,prsc:2|IN-7160-T;n:type:ShaderForge.SFN_TexCoord,id:8143,x:31612,y:33376,varname:node_8143,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_OneMinus,id:6977,x:31943,y:33386,varname:node_6977,prsc:2|IN-1737-OUT;n:type:ShaderForge.SFN_Multiply,id:9823,x:32088,y:33462,varname:node_9823,prsc:2|A-6977-OUT,B-6080-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:8052,x:31695,y:33170,varname:node_8052,prsc:2;n:type:ShaderForge.SFN_Sin,id:9006,x:31904,y:33185,varname:node_9006,prsc:2|IN-8052-X;n:type:ShaderForge.SFN_Multiply,id:2559,x:32178,y:33142,varname:node_2559,prsc:2|A-9006-OUT,B-5474-OUT;n:type:ShaderForge.SFN_Multiply,id:8641,x:32281,y:33462,varname:node_8641,prsc:2|A-9823-OUT,B-3973-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3973,x:32033,y:33653,ptovrint:False,ptlb:Intensty_copy,ptin:_Intensty_copy,varname:_Intensty_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Add,id:1737,x:31778,y:33376,varname:node_1737,prsc:2|A-8143-V,B-8143-Z;n:type:ShaderForge.SFN_Multiply,id:5474,x:32329,y:33300,varname:node_5474,prsc:2|A-8654-OUT,B-8641-OUT;n:type:ShaderForge.SFN_Append,id:5986,x:32345,y:33030,varname:node_5986,prsc:2|A-2559-OUT,B-9265-OUT,C-2559-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9265,x:32065,y:33052,ptovrint:False,ptlb:node_9265,ptin:_node_9265,varname:node_9265,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:8182-3973-8654-9265;pass:END;sub:END;*/

Shader "Shader Forge/bush with wind" {
    Properties {
        _node_8182 ("node_8182", 2D) = "white" {}
        _Intensty_copy ("Intensty_copy", Float ) = 0.05
        _Intensty ("Intensty", Float ) = 10
        _node_9265 ("node_9265", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_8182; uniform float4 _node_8182_ST;
            uniform float _Intensty;
            uniform float _Intensty_copy;
            uniform float _node_9265;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                float4 node_7160 = _Time;
                float node_2559 = (sin(mul(unity_ObjectToWorld, v.vertex).r)*(_Intensty*(((1.0 - (o.uv0.g+o.uv0.b))*cos(node_7160.g))*_Intensty_copy)));
                v.vertex.xyz += (o.vertexColor.r*float3(node_2559,_node_9265,node_2559));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _node_8182_var = tex2D(_node_8182,TRANSFORM_TEX(i.uv0, _node_8182));
                clip(_node_8182_var.a - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _node_8182_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_8182; uniform float4 _node_8182_ST;
            uniform float _Intensty;
            uniform float _Intensty_copy;
            uniform float _node_9265;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                float4 node_7160 = _Time;
                float node_2559 = (sin(mul(unity_ObjectToWorld, v.vertex).r)*(_Intensty*(((1.0 - (o.uv0.g+o.uv0.b))*cos(node_7160.g))*_Intensty_copy)));
                v.vertex.xyz += (o.vertexColor.r*float3(node_2559,_node_9265,node_2559));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _node_8182_var = tex2D(_node_8182,TRANSFORM_TEX(i.uv0, _node_8182));
                clip(_node_8182_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
