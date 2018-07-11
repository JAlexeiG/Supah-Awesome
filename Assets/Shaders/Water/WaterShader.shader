// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "water_shader"
{
	Properties
	{
		_Color0("Color 0", Color) = (0.4926471,0.8740366,1,1)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_wavespeed("wave speed", Range( 0.1 , 10)) = 0.1
		_vertexoffset("vertex offset", Range( 0 , 5)) = 0
		[Toggle]_ToggleSwitch1("Toggle Switch1", Float) = 1
		_TextureSample2("Texture Sample 2", 2D) = "bump" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_uv("uv", Range( 0.1 , 1)) = 0.2482353
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float _ToggleSwitch1;
		uniform sampler2D _TextureSample2;
		uniform float4 _TextureSample2_ST;
		uniform sampler2D _TextureSample1;
		uniform float _uv;
		uniform float4 _Color0;
		uniform sampler2D _TextureSample0;
		uniform float _wavespeed;
		uniform float _vertexoffset;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float speed18 = ( _Time.x * _wavespeed );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float2 temp_cast_0 = (( speed18 + (ase_vertex3Pos).y )).xx;
			float2 uv_TexCoord10 = v.texcoord.xy * float2( 1,1 ) + temp_cast_0;
			float3 ase_vertexNormal = v.normal.xyz;
			float3 VertexAnimation12 = ( ( tex2Dlod( _TextureSample0, float4( uv_TexCoord10, 0, 1.0) ).r - 0.5 ) * ( ase_vertexNormal * _vertexoffset ) );
			v.vertex.xyz += VertexAnimation12;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample2 = i.uv_texcoord * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float3 normalizeResult27 = normalize( ( cross( ddx( ase_worldPos ) , ddy( ase_worldPos ) ) + float3( 1E-09,0,0 ) ) );
			float3 Normal22 = lerp(UnpackNormal( tex2D( _TextureSample2, uv_TextureSample2 ) ),normalizeResult27,_ToggleSwitch1);
			o.Normal = Normal22;
			float2 temp_cast_0 = (_uv).xx;
			float2 uv_TexCoord36 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 panner37 = ( uv_TexCoord36 + 1.0 * _Time.y * temp_cast_0);
			float4 Albedo33 = ( tex2D( _TextureSample1, panner37 ) * _Color0 );
			o.Albedo = Albedo33.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14401
7;29;1522;788;3801.814;1172.072;3.305928;True;False
Node;AmplifyShaderEditor.CommentaryNode;14;-2516.786,-412.5117;Float;False;914.394;362.5317;Comment;4;18;17;16;15;Wave Speed;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-2466.787,-164.9786;Float;False;Property;_wavespeed;wave speed;2;0;Create;True;0.1;4.3;0.1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;16;-2395.89,-362.5114;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;1;-2414.857,20.71402;Float;False;2321.461;426.9865;Comment;12;13;12;11;10;9;8;7;6;5;4;3;2;Vertex Animation;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-2066.855,-227.4901;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;20;-1817.931,589.9936;Float;False;1244.412;443.4576;Comment;9;29;28;27;26;25;24;23;22;21;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;7;-2366.857,164.714;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;18;-1845.394,-319.8354;Float;False;speed;-1;True;1;0;FLOAT;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;8;-2126.858,180.714;Float;False;False;True;False;True;1;0;FLOAT3;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;23;-1806.436,854.825;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;9;-2046.858,100.714;Float;False;18;0;1;FLOAT;0
Node;AmplifyShaderEditor.DdyOpNode;24;-1598.436,934.8249;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DdxOpNode;29;-1598.436,838.825;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;2;-1838.857,100.714;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1049.692,-575.9413;Float;False;Property;_uv;uv;7;0;Create;True;0.2482353;0.128;0.1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-1020.056,-718.882;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-1662.857,68.71402;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CrossProductOpNode;25;-1470.436,870.825;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PannerNode;37;-751.2556,-677.2819;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;3;-1374.857,68.71402;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;6c141b1b2f7d1cc4a96c7d6d6128237c;31890676c5b178840848afa665cb5a2f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-1022.857,324.714;Float;False;Property;_vertexoffset;vertex offset;3;0;Create;True;0;0.86;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-1302.432,905.0737;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;1E-09,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalVertexDataNode;11;-942.8574,180.714;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;35;-543.4297,-665.2965;Float;True;Property;_TextureSample1;Texture Sample 1;6;0;Create;True;6c141b1b2f7d1cc4a96c7d6d6128237c;6c141b1b2f7d1cc4a96c7d6d6128237c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;32;-495.8647,-439.9776;Float;False;Property;_Color0;Color 0;0;0;Create;True;0.4926471,0.8740366,1,1;0.4926471,0.8740366,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode;27;-1166.435,934.8249;Float;False;1;0;FLOAT3;0.0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;21;-1443.816,639.9935;Float;True;Property;_TextureSample2;Texture Sample 2;5;0;Create;True;dd2fd2df93418444c8e280f1d34deeb5;dd2fd2df93418444c8e280f1d34deeb5;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-702.8578,260.714;Float;False;2;2;0;FLOAT3;1.0,0,0;False;1;FLOAT;0.0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;-686.8578,100.714;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-526.858,228.714;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT3;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-216.8556,-560.4822;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;28;-1019.291,842.2167;Float;False;Property;_ToggleSwitch1;Toggle Switch1;4;1;[Toggle];Create;True;1;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;33;-51.96077,-507.9953;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;22;-816.5178,853.2215;Float;False;Normal;-1;True;1;0;FLOAT3;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;34;-42.32434,-56.41653;Float;False;33;0;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;12;-366.8581,228.714;Float;False;VertexAnimation;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;19;-52.11121,214.8777;Float;False;12;0;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;-31.3252,46.85266;Float;False;22;0;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;200.308,-2.903014;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;water_shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;16;1
WireConnection;17;1;15;0
WireConnection;18;0;17;0
WireConnection;8;0;7;0
WireConnection;24;0;23;0
WireConnection;29;0;23;0
WireConnection;2;0;9;0
WireConnection;2;1;8;0
WireConnection;10;1;2;0
WireConnection;25;0;29;0
WireConnection;25;1;24;0
WireConnection;37;0;36;0
WireConnection;37;2;38;0
WireConnection;3;1;10;0
WireConnection;26;0;25;0
WireConnection;35;1;37;0
WireConnection;27;0;26;0
WireConnection;5;0;11;0
WireConnection;5;1;13;0
WireConnection;4;0;3;1
WireConnection;6;0;4;0
WireConnection;6;1;5;0
WireConnection;39;0;35;0
WireConnection;39;1;32;0
WireConnection;28;0;21;0
WireConnection;28;1;27;0
WireConnection;33;0;39;0
WireConnection;22;0;28;0
WireConnection;12;0;6;0
WireConnection;0;0;34;0
WireConnection;0;1;30;0
WireConnection;0;11;19;0
ASEEND*/
//CHKSM=7A9C4908A147D8F9B914027F90E08EFBB9BC0F14