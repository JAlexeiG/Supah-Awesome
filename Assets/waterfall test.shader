// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "waterfall test"
{
	Properties
	{
		[Header(Translucency)]
		_Translucency("Strength", Range( 0 , 50)) = 1
		_TransNormalDistortion("Normal Distortion", Range( 0 , 1)) = 0.1
		_TransScattering("Scaterring Falloff", Range( 1 , 50)) = 2
		_TransDirect("Direct", Range( 0 , 1)) = 1
		_TransAmbient("Ambient", Range( 0 , 1)) = 0.2
		_TransShadow("Shadow", Range( 0 , 1)) = 0.9
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#pragma target 4.6
		#pragma surface surf StandardCustom keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		struct SurfaceOutputStandardCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			fixed Alpha;
			fixed3 Translucency;
		};

		uniform sampler2D _TextureSample0;
		uniform sampler2D _TextureSample1;
		uniform sampler2D _TextureSample2;
		uniform half _Translucency;
		uniform half _TransNormalDistortion;
		uniform half _TransScattering;
		uniform half _TransDirect;
		uniform half _TransAmbient;
		uniform half _TransShadow;

		inline half4 LightingStandardCustom(SurfaceOutputStandardCustom s, half3 viewDir, UnityGI gi )
		{
			#if !DIRECTIONAL
			float3 lightAtten = gi.light.color;
			#else
			float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, _TransShadow );
			#endif
			half3 lightDir = gi.light.dir + s.Normal * _TransNormalDistortion;
			half transVdotL = pow( saturate( dot( viewDir, -lightDir ) ), _TransScattering );
			half3 translucency = lightAtten * (transVdotL * _TransDirect + gi.indirect.diffuse * _TransAmbient) * s.Translucency;
			half4 c = half4( s.Albedo * translucency * _Translucency, 0 );

			SurfaceOutputStandard r;
			r.Albedo = s.Albedo;
			r.Normal = s.Normal;
			r.Emission = s.Emission;
			r.Metallic = s.Metallic;
			r.Smoothness = s.Smoothness;
			r.Occlusion = s.Occlusion;
			r.Alpha = s.Alpha;
			return LightingStandard (r, viewDir, gi) + c;
		}

		inline void LightingStandardCustom_GI(SurfaceOutputStandardCustom s, UnityGIInput data, inout UnityGI gi )
		{
			#if defined(UNITY_PASS_DEFERRED) && UNITY_ENABLE_REFLECTION_BUFFERS
				gi = UnityGlobalIllumination(data, s.Occlusion, s.Normal);
			#else
				UNITY_GLOSSY_ENV_FROM_SURFACE( g, s, data );
				gi = UnityGlobalIllumination( data, s.Occlusion, s.Normal, g );
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandardCustom o )
		{
			float2 _Vector1 = float2(10,10);
			float2 Offset15 = ( ( _Vector1.x - 1 ) * float3( _Vector1 ,  0.0 ).xy * _Vector1.x ) + float2( 2,2 );
			float2 uv_TexCoord4 = i.uv_texcoord * float2( 1,1 ) + Offset15;
			float2 panner2 = ( uv_TexCoord4 + 1.0 * _Time.y * float2( 0,-2 ));
			float2 uv_TexCoord5 = i.uv_texcoord * float2( 1,1 ) + Offset15;
			float2 panner6 = ( uv_TexCoord5 + 1.0 * _Time.y * float2( 0,-2.7 ));
			float2 uv_TexCoord9 = i.uv_texcoord * float2( 1,1 ) + Offset15;
			float2 panner10 = ( uv_TexCoord9 + 1.0 * _Time.y * float2( 0,-4 ));
			float temp_output_12_0 = pow( tex2D( _TextureSample2, panner10 ).r , 2.0 );
			float4 lerpResult8 = lerp( ( float4(0.05752595,0.7113381,0.9779412,0) * tex2D( _TextureSample0, panner2 ) ) , tex2D( _TextureSample1, panner6 ) , temp_output_12_0);
			o.Normal = lerpResult8.rgb;
			o.Albedo = lerpResult8.rgb;
			float3 temp_cast_11 = (temp_output_12_0).xxx;
			o.Translucency = temp_cast_11;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14401
-62;48;1522;788;659.1617;21.59422;1.374951;True;False
Node;AmplifyShaderEditor.Vector2Node;16;-964.6258,496.6577;Float;False;Constant;_Vector0;Vector 0;4;0;Create;True;2,2;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;17;-1038.51,789.5061;Float;False;Constant;_Vector1;Vector 1;4;0;Create;True;10,10;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ParallaxMappingNode;15;-702.6245,713.5342;Float;False;Normal;4;0;FLOAT2;0,0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-246.4438,336.5872;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-284.2762,998.1049;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,2;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;2;41.08057,296.2783;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-2;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;10;3.248263,957.7964;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-4;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-297.5262,646.4456;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,1.5;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;358.0806,305.2783;Float;True;Property;_TextureSample0;Texture Sample 0;8;0;Create;True;7f7f573fab2c196429373b7d5a9727d5;7f7f573fab2c196429373b7d5a9727d5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;18;361.0525,103.5263;Float;False;Constant;_Color0;Color 0;4;0;Create;True;0.05752595,0.7113381,0.9779412,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;359.4026,900.8516;Float;True;Property;_TextureSample2;Texture Sample 2;6;0;Create;True;29c0d31300cb5d94ca3e4a7f307bb168;29c0d31300cb5d94ca3e4a7f307bb168;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;6;-10.00165,606.1368;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-2.7;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;732.2891,304.2692;Float;False;2;2;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;12;687.6065,937.4153;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;2.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;306.9981,615.1368;Float;True;Property;_TextureSample1;Texture Sample 1;7;0;Create;True;7f7f573fab2c196429373b7d5a9727d5;7f7f573fab2c196429373b7d5a9727d5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;8;885.907,551.1178;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,1;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1155.889,379.2422;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;waterfall test;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;50;10;25;True;1;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;0;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;16;0
WireConnection;15;1;17;0
WireConnection;15;2;17;0
WireConnection;15;3;17;0
WireConnection;4;1;15;0
WireConnection;9;1;15;0
WireConnection;2;0;4;0
WireConnection;10;0;9;0
WireConnection;5;1;15;0
WireConnection;1;1;2;0
WireConnection;11;1;10;0
WireConnection;6;0;5;0
WireConnection;19;0;18;0
WireConnection;19;1;1;0
WireConnection;12;0;11;1
WireConnection;7;1;6;0
WireConnection;8;0;19;0
WireConnection;8;1;7;0
WireConnection;8;2;12;0
WireConnection;0;0;8;0
WireConnection;0;1;8;0
WireConnection;0;7;12;0
ASEEND*/
//CHKSM=63798431025F060954DAAA365031B1BC3B1E20D8