// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT,clip-7698-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31946,y:32580,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:eaa753e89ff30074e966bae90c6eb83b,ntxv:0,isnm:False|UVIN-4304-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32206,y:32772,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-6854-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:31946,y:32751,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:31946,y:32909,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.02833048,c2:0.4603211,c3:0.9632353,c4:1;n:type:ShaderForge.SFN_Append,id:6528,x:30171,y:32619,varname:node_6528,prsc:2|A-7310-OUT,B-9822-OUT;n:type:ShaderForge.SFN_Time,id:844,x:30169,y:32780,varname:node_844,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:7310,x:29926,y:32653,ptovrint:False,ptlb:U speed,ptin:_Uspeed,varname:node_7310,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:9822,x:29928,y:32741,ptovrint:False,ptlb:V speed,ptin:_Vspeed,varname:node_9822,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:6103,x:30333,y:32659,varname:node_6103,prsc:2|A-6528-OUT,B-844-T;n:type:ShaderForge.SFN_TexCoord,id:3967,x:30333,y:32780,varname:node_3967,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:2530,x:30515,y:32659,varname:node_2530,prsc:2|A-6103-OUT,B-3967-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9859,x:30673,y:32655,varname:node_9859,prsc:2,tex:046185c8571d11149b190a2c57feb494,ntxv:0,isnm:False|UVIN-2530-OUT,TEX-974-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:974,x:30502,y:32806,ptovrint:False,ptlb:noise,ptin:_noise,varname:node_974,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:046185c8571d11149b190a2c57feb494,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:633,x:30156,y:32941,varname:node_633,prsc:2|A-6488-OUT,B-6457-OUT;n:type:ShaderForge.SFN_Time,id:6384,x:30154,y:33101,varname:node_6384,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5390,x:30318,y:32980,varname:node_5390,prsc:2|A-633-OUT,B-6384-T;n:type:ShaderForge.SFN_TexCoord,id:2433,x:30318,y:33101,varname:node_2433,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:6488,x:29921,y:32894,ptovrint:False,ptlb:2U Speed,ptin:_2USpeed,varname:node_6488,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.2;n:type:ShaderForge.SFN_ValueProperty,id:6457,x:29922,y:33020,ptovrint:False,ptlb:2V Speed,ptin:_2VSpeed,varname:node_6457,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Add,id:6093,x:30502,y:32980,varname:node_6093,prsc:2|A-5390-OUT,B-2433-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:6038,x:30698,y:32962,varname:node_6038,prsc:2,tex:046185c8571d11149b190a2c57feb494,ntxv:0,isnm:False|UVIN-6093-OUT,TEX-974-TEX;n:type:ShaderForge.SFN_Slider,id:4569,x:30128,y:32517,ptovrint:False,ptlb:dissolve,ptin:_dissolve,varname:node_4569,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4746115,max:1;n:type:ShaderForge.SFN_OneMinus,id:9282,x:30454,y:32502,varname:node_9282,prsc:2|IN-4569-OUT;n:type:ShaderForge.SFN_RemapRange,id:9559,x:30638,y:32502,varname:node_9559,prsc:2,frmn:0,frmx:1,tomn:-0.65,tomx:0.65|IN-9282-OUT;n:type:ShaderForge.SFN_Add,id:1226,x:30833,y:32543,varname:node_1226,prsc:2|A-9559-OUT,B-9859-R;n:type:ShaderForge.SFN_Add,id:3731,x:30852,y:32712,varname:node_3731,prsc:2|A-9559-OUT,B-6038-R;n:type:ShaderForge.SFN_Multiply,id:7837,x:31006,y:32543,varname:node_7837,prsc:2|A-1226-OUT,B-3731-OUT;n:type:ShaderForge.SFN_RemapRange,id:2697,x:31184,y:32543,varname:node_2697,prsc:2,frmn:0,frmx:1,tomn:-10,tomx:10|IN-7837-OUT;n:type:ShaderForge.SFN_Clamp01,id:2151,x:31373,y:32543,varname:node_2151,prsc:2|IN-2697-OUT;n:type:ShaderForge.SFN_OneMinus,id:3920,x:31532,y:32543,varname:node_3920,prsc:2|IN-2151-OUT;n:type:ShaderForge.SFN_Append,id:4304,x:31713,y:32554,varname:node_4304,prsc:2|A-3920-OUT,B-7249-OUT;n:type:ShaderForge.SFN_Vector1,id:7249,x:31545,y:32677,varname:node_7249,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:6854,x:31946,y:33109,ptovrint:False,ptlb:opacity,ptin:_opacity,varname:node_6854,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:5112,x:31972,y:32488,ptovrint:False,ptlb:strength,ptin:_strength,varname:node_5112,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;n:type:ShaderForge.SFN_Multiply,id:7698,x:32193,y:32514,varname:node_7698,prsc:2|A-5112-OUT,B-6074-R;proporder:6074-797-7310-9822-974-6488-6457-4569-6854-5112;pass:END;sub:END;*/

Shader "Shader Forge/electricNew" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.02833048,0.4603211,0.9632353,1)
        _Uspeed ("U speed", Float ) = 0.2
        _Vspeed ("V speed", Float ) = 0.2
        _noise ("noise", 2D) = "white" {}
        _2USpeed ("2U Speed", Float ) = -0.2
        _2VSpeed ("2V Speed", Float ) = 0.05
        _dissolve ("dissolve", Range(0, 1)) = 0.4746115
        _opacity ("opacity", Float ) = 2
        _strength ("strength", Float ) = 0.8
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
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _Uspeed;
            uniform float _Vspeed;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _2USpeed;
            uniform float _2VSpeed;
            uniform float _dissolve;
            uniform float _opacity;
            uniform float _strength;
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
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_9559 = ((1.0 - _dissolve)*1.3+-0.65);
                float4 node_844 = _Time;
                float2 node_2530 = ((float2(_Uspeed,_Vspeed)*node_844.g)+i.uv0);
                float4 node_9859 = tex2D(_noise,TRANSFORM_TEX(node_2530, _noise));
                float4 node_6384 = _Time;
                float2 node_6093 = ((float2(_2USpeed,_2VSpeed)*node_6384.g)+i.uv0);
                float4 node_6038 = tex2D(_noise,TRANSFORM_TEX(node_6093, _noise));
                float2 node_4304 = float2((1.0 - saturate((((node_9559+node_9859.r)*(node_9559+node_6038.r))*20.0+-10.0))),0.0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4304, _MainTex));
                clip((_strength*_MainTex_var.r) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*_opacity);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
