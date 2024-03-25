// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Malbers/Color4x4"
{
	Properties
	{
		_Tint("Tint", Color) = (1,1,1,1)
		[Header(Albedo (A Gradient))]_Color1("Color 1", Color) = (1,0.1544118,0.1544118,0.291)
		_Color2("Color 2", Color) = (1,0.1544118,0.8017241,0.253)
		_Color3("Color 3", Color) = (0.2535501,0.1544118,1,0.541)
		_Color4("Color 4", Color) = (0.1544118,0.5451319,1,0.253)
		[Space(10)]_Color5("Color 5", Color) = (0.9533468,1,0.1544118,0.553)
		_Color6("Color 6", Color) = (0.2720588,0.1294625,0,0.097)
		_Color7("Color 7", Color) = (0.1544118,0.6151115,1,0.178)
		_Color8("Color 8", Color) = (0.4849697,0.5008695,0.5073529,0.078)
		[Space(10)]_Color9("Color 9", Color) = (0.3164301,0,0.7058823,0.134)
		_Color10("Color 10", Color) = (0.362069,0.4411765,0,0.759)
		_Color11("Color 11", Color) = (0.6691177,0.6691177,0.6691177,0.647)
		_Color12("Color 12", Color) = (0.5073529,0.1574544,0,0.128)
		[Space(10)]_Color13("Color 13", Color) = (1,0.5586207,0,0.272)
		_Color14("Color 14", Color) = (0,0.8025862,0.875,0.047)
		_Color15("Color 15", Color) = (1,0,0,0.391)
		_Color16("Color 16", Color) = (0.4080882,0.75,0.4811866,0.134)
		[Header(Metallic(R) Rough(G) Emmission(B))]_MRE1("MRE 1", Color) = (0,1,0,0)
		_MRE2("MRE 2", Color) = (0,1,0,0)
		_MRE3("MRE 3", Color) = (0,1,0,0)
		_MRE4("MRE 4", Color) = (0,1,0,0)
		[Space(10)]_MRE5("MRE 5", Color) = (0,1,0,0)
		_MRE6("MRE 6", Color) = (0,1,0,0)
		_MRE7("MRE 7", Color) = (0,1,0,0)
		_MRE8("MRE 8", Color) = (0,1,0,0)
		[Space(10)]_MRE9("MRE 9", Color) = (0,1,0,0)
		_MRE10("MRE 10", Color) = (0,1,0,0)
		_MRE11("MRE 11", Color) = (0,1,0,0)
		_MRE12("MRE 12", Color) = (0,1,0,0)
		[Space(10)]_MRE13("MRE 13", Color) = (0,1,0,0)
		_MRE14("MRE 14", Color) = (0,1,0,0)
		_MRE15("MRE 15", Color) = (0,1,0,0)
		_MRE16("MRE 16", Color) = (0,1,0,0)
		[Header(Emmision)]_EmissionPower1("Emission Power", Float) = 1
		[SingleLineTexture][Header(Gradient)]_Gradient("Gradient", 2D) = "white" {}
		_GradientIntensity("Gradient Intensity", Range( 0 , 1)) = 1
		_GradientColor("Gradient Color", Color) = (0,0,0,0)
		_GradientScale("Gradient Scale", Float) = 1
		_GradientOffset("Gradient Offset", Float) = 0
		_GradientPower("Gradient Power", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 5.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Gradient;
		uniform float4 _GradientColor;
		uniform float _GradientIntensity;
		uniform float _GradientScale;
		uniform float _GradientOffset;
		uniform float _GradientPower;
		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform float4 _Color3;
		uniform float4 _Color4;
		uniform float4 _Color5;
		uniform float4 _Color6;
		uniform float4 _Color7;
		uniform float4 _Color8;
		uniform float4 _Color9;
		uniform float4 _Color10;
		uniform float4 _Color11;
		uniform float4 _Color12;
		uniform float4 _Color13;
		uniform float4 _Color14;
		uniform float4 _Color15;
		uniform float4 _Color16;
		uniform float4 _Tint;
		uniform float _EmissionPower1;
		uniform float4 _MRE1;
		uniform float4 _MRE2;
		uniform float4 _MRE3;
		uniform float4 _MRE4;
		uniform float4 _MRE5;
		uniform float4 _MRE6;
		uniform float4 _MRE7;
		uniform float4 _MRE8;
		uniform float4 _MRE9;
		uniform float4 _MRE10;
		uniform float4 _MRE11;
		uniform float4 _MRE12;
		uniform float4 _MRE13;
		uniform float4 _MRE14;
		uniform float4 _MRE15;
		uniform float4 _MRE16;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord256 = i.uv_texcoord * float2( 1,4 );
			float4 clampResult328 = clamp( ( ( tex2D( _Gradient, uv_TexCoord256 ) + _GradientColor ) + ( 1.0 - _GradientIntensity ) ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 temp_cast_0 = (_GradientPower).xxxx;
			float temp_output_3_0_g742 = 1.0;
			float temp_output_7_0_g742 = 4.0;
			float temp_output_9_0_g742 = 4.0;
			float temp_output_8_0_g742 = 4.0;
			float temp_output_3_0_g740 = 2.0;
			float temp_output_7_0_g740 = 4.0;
			float temp_output_9_0_g740 = 4.0;
			float temp_output_8_0_g740 = 4.0;
			float temp_output_3_0_g743 = 3.0;
			float temp_output_7_0_g743 = 4.0;
			float temp_output_9_0_g743 = 4.0;
			float temp_output_8_0_g743 = 4.0;
			float temp_output_3_0_g741 = 4.0;
			float temp_output_7_0_g741 = 4.0;
			float temp_output_9_0_g741 = 4.0;
			float temp_output_8_0_g741 = 4.0;
			float temp_output_3_0_g737 = 1.0;
			float temp_output_7_0_g737 = 4.0;
			float temp_output_9_0_g737 = 3.0;
			float temp_output_8_0_g737 = 4.0;
			float temp_output_3_0_g738 = 2.0;
			float temp_output_7_0_g738 = 4.0;
			float temp_output_9_0_g738 = 3.0;
			float temp_output_8_0_g738 = 4.0;
			float temp_output_3_0_g745 = 3.0;
			float temp_output_7_0_g745 = 4.0;
			float temp_output_9_0_g745 = 3.0;
			float temp_output_8_0_g745 = 4.0;
			float temp_output_3_0_g735 = 4.0;
			float temp_output_7_0_g735 = 4.0;
			float temp_output_9_0_g735 = 3.0;
			float temp_output_8_0_g735 = 4.0;
			float temp_output_3_0_g733 = 1.0;
			float temp_output_7_0_g733 = 4.0;
			float temp_output_9_0_g733 = 2.0;
			float temp_output_8_0_g733 = 4.0;
			float temp_output_3_0_g725 = 2.0;
			float temp_output_7_0_g725 = 4.0;
			float temp_output_9_0_g725 = 2.0;
			float temp_output_8_0_g725 = 4.0;
			float temp_output_3_0_g746 = 3.0;
			float temp_output_7_0_g746 = 4.0;
			float temp_output_9_0_g746 = 2.0;
			float temp_output_8_0_g746 = 4.0;
			float temp_output_3_0_g739 = 4.0;
			float temp_output_7_0_g739 = 4.0;
			float temp_output_9_0_g739 = 2.0;
			float temp_output_8_0_g739 = 4.0;
			float temp_output_3_0_g732 = 1.0;
			float temp_output_7_0_g732 = 4.0;
			float temp_output_9_0_g732 = 1.0;
			float temp_output_8_0_g732 = 4.0;
			float temp_output_3_0_g736 = 2.0;
			float temp_output_7_0_g736 = 4.0;
			float temp_output_9_0_g736 = 1.0;
			float temp_output_8_0_g736 = 4.0;
			float temp_output_3_0_g744 = 3.0;
			float temp_output_7_0_g744 = 4.0;
			float temp_output_9_0_g744 = 1.0;
			float temp_output_8_0_g744 = 4.0;
			float temp_output_3_0_g734 = 4.0;
			float temp_output_7_0_g734 = 4.0;
			float temp_output_9_0_g734 = 1.0;
			float temp_output_8_0_g734 = 4.0;
			float4 temp_output_329_0 = ( ( ( _Color1 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g742 - 1.0 ) / temp_output_7_0_g742 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g742 / temp_output_7_0_g742 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g742 - 1.0 ) / temp_output_8_0_g742 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g742 / temp_output_8_0_g742 ) ) * 1.0 ) ) ) ) + ( _Color2 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g740 - 1.0 ) / temp_output_7_0_g740 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g740 / temp_output_7_0_g740 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g740 - 1.0 ) / temp_output_8_0_g740 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g740 / temp_output_8_0_g740 ) ) * 1.0 ) ) ) ) + ( _Color3 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g743 - 1.0 ) / temp_output_7_0_g743 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g743 / temp_output_7_0_g743 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g743 - 1.0 ) / temp_output_8_0_g743 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g743 / temp_output_8_0_g743 ) ) * 1.0 ) ) ) ) + ( _Color4 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g741 - 1.0 ) / temp_output_7_0_g741 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g741 / temp_output_7_0_g741 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g741 - 1.0 ) / temp_output_8_0_g741 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g741 / temp_output_8_0_g741 ) ) * 1.0 ) ) ) ) ) + ( ( _Color5 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g737 - 1.0 ) / temp_output_7_0_g737 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g737 / temp_output_7_0_g737 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g737 - 1.0 ) / temp_output_8_0_g737 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g737 / temp_output_8_0_g737 ) ) * 1.0 ) ) ) ) + ( _Color6 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g738 - 1.0 ) / temp_output_7_0_g738 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g738 / temp_output_7_0_g738 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g738 - 1.0 ) / temp_output_8_0_g738 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g738 / temp_output_8_0_g738 ) ) * 1.0 ) ) ) ) + ( _Color7 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g745 - 1.0 ) / temp_output_7_0_g745 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g745 / temp_output_7_0_g745 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g745 - 1.0 ) / temp_output_8_0_g745 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g745 / temp_output_8_0_g745 ) ) * 1.0 ) ) ) ) + ( _Color8 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g735 - 1.0 ) / temp_output_7_0_g735 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g735 / temp_output_7_0_g735 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g735 - 1.0 ) / temp_output_8_0_g735 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g735 / temp_output_8_0_g735 ) ) * 1.0 ) ) ) ) ) + ( ( _Color9 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g733 - 1.0 ) / temp_output_7_0_g733 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g733 / temp_output_7_0_g733 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g733 - 1.0 ) / temp_output_8_0_g733 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g733 / temp_output_8_0_g733 ) ) * 1.0 ) ) ) ) + ( _Color10 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g725 - 1.0 ) / temp_output_7_0_g725 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g725 / temp_output_7_0_g725 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g725 - 1.0 ) / temp_output_8_0_g725 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g725 / temp_output_8_0_g725 ) ) * 1.0 ) ) ) ) + ( _Color11 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g746 - 1.0 ) / temp_output_7_0_g746 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g746 / temp_output_7_0_g746 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g746 - 1.0 ) / temp_output_8_0_g746 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g746 / temp_output_8_0_g746 ) ) * 1.0 ) ) ) ) + ( _Color12 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g739 - 1.0 ) / temp_output_7_0_g739 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g739 / temp_output_7_0_g739 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g739 - 1.0 ) / temp_output_8_0_g739 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g739 / temp_output_8_0_g739 ) ) * 1.0 ) ) ) ) ) + ( ( _Color13 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g732 - 1.0 ) / temp_output_7_0_g732 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g732 / temp_output_7_0_g732 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g732 - 1.0 ) / temp_output_8_0_g732 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g732 / temp_output_8_0_g732 ) ) * 1.0 ) ) ) ) + ( _Color14 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g736 - 1.0 ) / temp_output_7_0_g736 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g736 / temp_output_7_0_g736 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g736 - 1.0 ) / temp_output_8_0_g736 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g736 / temp_output_8_0_g736 ) ) * 1.0 ) ) ) ) + ( _Color15 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g744 - 1.0 ) / temp_output_7_0_g744 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g744 / temp_output_7_0_g744 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g744 - 1.0 ) / temp_output_8_0_g744 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g744 / temp_output_8_0_g744 ) ) * 1.0 ) ) ) ) + ( _Color16 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g734 - 1.0 ) / temp_output_7_0_g734 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g734 / temp_output_7_0_g734 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g734 - 1.0 ) / temp_output_8_0_g734 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g734 / temp_output_8_0_g734 ) ) * 1.0 ) ) ) ) ) );
			float4 clampResult348 = clamp( ( pow( (clampResult328*_GradientScale + _GradientOffset) , temp_cast_0 ) + ( 1.0 - (temp_output_329_0).a ) ) , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
			o.Albedo = ( clampResult348 * temp_output_329_0 * _Tint ).rgb;
			float temp_output_3_0_g751 = 1.0;
			float temp_output_7_0_g751 = 4.0;
			float temp_output_9_0_g751 = 4.0;
			float temp_output_8_0_g751 = 4.0;
			float temp_output_3_0_g760 = 2.0;
			float temp_output_7_0_g760 = 4.0;
			float temp_output_9_0_g760 = 4.0;
			float temp_output_8_0_g760 = 4.0;
			float temp_output_3_0_g755 = 3.0;
			float temp_output_7_0_g755 = 4.0;
			float temp_output_9_0_g755 = 4.0;
			float temp_output_8_0_g755 = 4.0;
			float temp_output_3_0_g752 = 4.0;
			float temp_output_7_0_g752 = 4.0;
			float temp_output_9_0_g752 = 4.0;
			float temp_output_8_0_g752 = 4.0;
			float temp_output_3_0_g748 = 1.0;
			float temp_output_7_0_g748 = 4.0;
			float temp_output_9_0_g748 = 3.0;
			float temp_output_8_0_g748 = 4.0;
			float temp_output_3_0_g747 = 2.0;
			float temp_output_7_0_g747 = 4.0;
			float temp_output_9_0_g747 = 3.0;
			float temp_output_8_0_g747 = 4.0;
			float temp_output_3_0_g761 = 3.0;
			float temp_output_7_0_g761 = 4.0;
			float temp_output_9_0_g761 = 3.0;
			float temp_output_8_0_g761 = 4.0;
			float temp_output_3_0_g756 = 4.0;
			float temp_output_7_0_g756 = 4.0;
			float temp_output_9_0_g756 = 3.0;
			float temp_output_8_0_g756 = 4.0;
			float temp_output_3_0_g762 = 1.0;
			float temp_output_7_0_g762 = 4.0;
			float temp_output_9_0_g762 = 2.0;
			float temp_output_8_0_g762 = 4.0;
			float temp_output_3_0_g753 = 2.0;
			float temp_output_7_0_g753 = 4.0;
			float temp_output_9_0_g753 = 2.0;
			float temp_output_8_0_g753 = 4.0;
			float temp_output_3_0_g758 = 3.0;
			float temp_output_7_0_g758 = 4.0;
			float temp_output_9_0_g758 = 2.0;
			float temp_output_8_0_g758 = 4.0;
			float temp_output_3_0_g757 = 4.0;
			float temp_output_7_0_g757 = 4.0;
			float temp_output_9_0_g757 = 2.0;
			float temp_output_8_0_g757 = 4.0;
			float temp_output_3_0_g749 = 1.0;
			float temp_output_7_0_g749 = 4.0;
			float temp_output_9_0_g749 = 1.0;
			float temp_output_8_0_g749 = 4.0;
			float temp_output_3_0_g750 = 2.0;
			float temp_output_7_0_g750 = 4.0;
			float temp_output_9_0_g750 = 1.0;
			float temp_output_8_0_g750 = 4.0;
			float temp_output_3_0_g754 = 3.0;
			float temp_output_7_0_g754 = 4.0;
			float temp_output_9_0_g754 = 1.0;
			float temp_output_8_0_g754 = 4.0;
			float temp_output_3_0_g759 = 4.0;
			float temp_output_7_0_g759 = 4.0;
			float temp_output_9_0_g759 = 1.0;
			float temp_output_8_0_g759 = 4.0;
			float4 temp_output_344_0 = ( ( ( _MRE1 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g751 - 1.0 ) / temp_output_7_0_g751 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g751 / temp_output_7_0_g751 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g751 - 1.0 ) / temp_output_8_0_g751 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g751 / temp_output_8_0_g751 ) ) * 1.0 ) ) ) ) + ( _MRE2 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g760 - 1.0 ) / temp_output_7_0_g760 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g760 / temp_output_7_0_g760 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g760 - 1.0 ) / temp_output_8_0_g760 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g760 / temp_output_8_0_g760 ) ) * 1.0 ) ) ) ) + ( _MRE3 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g755 - 1.0 ) / temp_output_7_0_g755 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g755 / temp_output_7_0_g755 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g755 - 1.0 ) / temp_output_8_0_g755 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g755 / temp_output_8_0_g755 ) ) * 1.0 ) ) ) ) + ( _MRE4 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g752 - 1.0 ) / temp_output_7_0_g752 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g752 / temp_output_7_0_g752 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g752 - 1.0 ) / temp_output_8_0_g752 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g752 / temp_output_8_0_g752 ) ) * 1.0 ) ) ) ) ) + ( ( _MRE5 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g748 - 1.0 ) / temp_output_7_0_g748 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g748 / temp_output_7_0_g748 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g748 - 1.0 ) / temp_output_8_0_g748 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g748 / temp_output_8_0_g748 ) ) * 1.0 ) ) ) ) + ( _MRE6 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g747 - 1.0 ) / temp_output_7_0_g747 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g747 / temp_output_7_0_g747 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g747 - 1.0 ) / temp_output_8_0_g747 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g747 / temp_output_8_0_g747 ) ) * 1.0 ) ) ) ) + ( _MRE7 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g761 - 1.0 ) / temp_output_7_0_g761 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g761 / temp_output_7_0_g761 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g761 - 1.0 ) / temp_output_8_0_g761 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g761 / temp_output_8_0_g761 ) ) * 1.0 ) ) ) ) + ( _MRE8 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g756 - 1.0 ) / temp_output_7_0_g756 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g756 / temp_output_7_0_g756 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g756 - 1.0 ) / temp_output_8_0_g756 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g756 / temp_output_8_0_g756 ) ) * 1.0 ) ) ) ) ) + ( ( _MRE9 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g762 - 1.0 ) / temp_output_7_0_g762 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g762 / temp_output_7_0_g762 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g762 - 1.0 ) / temp_output_8_0_g762 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g762 / temp_output_8_0_g762 ) ) * 1.0 ) ) ) ) + ( _MRE10 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g753 - 1.0 ) / temp_output_7_0_g753 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g753 / temp_output_7_0_g753 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g753 - 1.0 ) / temp_output_8_0_g753 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g753 / temp_output_8_0_g753 ) ) * 1.0 ) ) ) ) + ( _MRE11 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g758 - 1.0 ) / temp_output_7_0_g758 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g758 / temp_output_7_0_g758 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g758 - 1.0 ) / temp_output_8_0_g758 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g758 / temp_output_8_0_g758 ) ) * 1.0 ) ) ) ) + ( _MRE12 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g757 - 1.0 ) / temp_output_7_0_g757 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g757 / temp_output_7_0_g757 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g757 - 1.0 ) / temp_output_8_0_g757 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g757 / temp_output_8_0_g757 ) ) * 1.0 ) ) ) ) ) + ( ( _MRE13 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g749 - 1.0 ) / temp_output_7_0_g749 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g749 / temp_output_7_0_g749 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g749 - 1.0 ) / temp_output_8_0_g749 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g749 / temp_output_8_0_g749 ) ) * 1.0 ) ) ) ) + ( _MRE14 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g750 - 1.0 ) / temp_output_7_0_g750 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g750 / temp_output_7_0_g750 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g750 - 1.0 ) / temp_output_8_0_g750 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g750 / temp_output_8_0_g750 ) ) * 1.0 ) ) ) ) + ( _MRE15 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g754 - 1.0 ) / temp_output_7_0_g754 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g754 / temp_output_7_0_g754 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g754 - 1.0 ) / temp_output_8_0_g754 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g754 / temp_output_8_0_g754 ) ) * 1.0 ) ) ) ) + ( _MRE16 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g759 - 1.0 ) / temp_output_7_0_g759 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g759 / temp_output_7_0_g759 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g759 - 1.0 ) / temp_output_8_0_g759 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g759 / temp_output_8_0_g759 ) ) * 1.0 ) ) ) ) ) );
			o.Emission = ( temp_output_329_0 * ( _EmissionPower1 * (temp_output_344_0).b ) ).rgb;
			o.Metallic = (temp_output_344_0).r;
			o.Smoothness = ( 1.0 - (temp_output_344_0).g );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
810;48;992;748;-1970.407;-263.6672;1.662267;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;256;288.1596,87.91077;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;257;-514.8902,1147.708;Float;False;Property;_Color5;Color 5;5;0;Create;True;0;0;0;False;1;Space(10);False;0.9533468,1,0.1544118,0.553;0.5660378,0.377519,0.3711285,0.5450981;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;262;-510.7125,1884.087;Float;False;Property;_Color8;Color 8;8;0;Create;True;0;0;0;False;0;False;0.4849697,0.5008695,0.5073529,0.078;0.5995304,0.4082413,0.6226414,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;258;-561.6131,3142.689;Float;False;Property;_Color13;Color 13;13;0;Create;True;0;0;0;False;1;Space(10);False;1,0.5586207,0,0.272;0.2358491,0.1996159,0.1546369,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;263;-545.7383,2633.965;Float;False;Property;_Color11;Color 11;11;0;Create;True;0;0;0;False;0;False;0.6691177,0.6691177,0.6691177,0.647;0.3113208,0.244575,0.2246796,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;260;-569.5535,3402.046;Float;False;Property;_Color14;Color 14;14;0;Create;True;0;0;0;False;0;False;0,0.8025862,0.875,0.047;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;264;547.1333,288.8422;Float;False;Property;_GradientColor;Gradient Color;36;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;269;-547.1475,2877.121;Float;False;Property;_Color12;Color 12;12;0;Create;True;0;0;0;False;0;False;0.5073529,0.1574544,0,0.128;0.4150943,0.2456527,0.1703453,0.509804;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;273;-591.2968,3879.068;Float;False;Property;_Color16;Color 16;16;0;Create;True;0;0;0;False;0;False;0.4080882,0.75,0.4811866,0.134;1,0.4910869,0,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;266;-556.0259,3635.911;Float;False;Property;_Color15;Color 15;15;0;Create;True;0;0;0;False;0;False;1,0,0,0.391;0.4150943,0.1037735,0.2168941,0.6509804;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;270;518.8171,472.1493;Float;False;Property;_GradientIntensity;Gradient Intensity;35;0;Create;True;0;0;0;False;0;False;1;0.755;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;259;532.3035,61.52296;Inherit;True;Property;_Gradient;Gradient;34;1;[SingleLineTexture];Create;True;0;0;0;False;1;Header(Gradient);False;-1;0f424a347039ef447a763d3d4b4782b0;0f424a347039ef447a763d3d4b4782b0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;261;-521.1363,626.6081;Float;False;Property;_Color3;Color 3;3;0;Create;True;0;0;0;False;0;False;0.2535501,0.1544118,1,0.541;0.2452827,0.2325557,0.2325557,0.541;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;268;-559.2659,2400.1;Float;False;Property;_Color10;Color 10;10;0;Create;True;0;0;0;False;0;False;0.362069,0.4411765,0,0.759;0.5,0.3972791,0.2712264,0.627451;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;275;-534.6641,392.7433;Float;False;Property;_Color2;Color 2;2;0;Create;True;0;0;0;False;0;False;1,0.1544118,0.8017241,0.253;0.8490566,0.5562637,0.3884834,0.2392157;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;265;-526.7234,133.3854;Float;False;Property;_Color1;Color 1;1;0;Create;True;0;0;0;False;1;Header(Albedo (A Gradient));False;1,0.1544118,0.1544118,0.291;0.4245283,0.1982467,0.2005823,0.291;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;271;-522.5457,870.9286;Float;False;Property;_Color4;Color 4;4;0;Create;True;0;0;0;False;0;False;0.1544118,0.5451319,1,0.253;0.4056604,0.08091336,0.04401031,0.8117647;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;272;-522.8309,1407.066;Float;False;Property;_Color6;Color 6;6;0;Create;True;0;0;0;False;0;False;0.2720588,0.1294625,0,0.097;0.490566,0.3123887,0.3240542,0.7254902;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;267;-509.303,1640.931;Float;False;Property;_Color7;Color 7;7;0;Create;True;0;0;0;False;0;False;0.1544118,0.6151115,1,0.178;0.3867925,0.3432073,0.3083393,0.1960784;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;274;-551.3253,2140.742;Float;False;Property;_Color9;Color 9;9;0;Create;True;0;0;0;False;1;Space(10);False;0.3164301,0,0.7058823,0.134;0.4339623,0.2533396,0.2108401,0.1960784;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;280;-207.1133,1411.907;Inherit;True;ColorShartSlot;-1;;738;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;2;False;9;FLOAT;3;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;277;-229.5064,2881.962;Inherit;True;ColorShartSlot;-1;;739;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;4;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;281;-243.5486,2404.941;Inherit;True;ColorShartSlot;-1;;725;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;2;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;293;-245.8953,3147.53;Inherit;True;ColorShartSlot;-1;;732;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;1;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;279;-235.6077,2145.583;Inherit;True;ColorShartSlot;-1;;733;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;1;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;284;-193.0711,1888.928;Inherit;True;ColorShartSlot;-1;;735;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;4;False;9;FLOAT;3;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;282;-253.836,3404.727;Inherit;True;ColorShartSlot;-1;;736;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;2;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;278;-199.1724,1152.549;Inherit;True;ColorShartSlot;-1;;737;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;1;False;9;FLOAT;3;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;283;-239.794,3883.909;Inherit;True;ColorShartSlot;-1;;734;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;4;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;291;-193.5855,1645.772;Inherit;True;ColorShartSlot;-1;;745;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;3;False;9;FLOAT;3;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;290;-211.0059,138.2261;Inherit;True;ColorShartSlot;-1;;742;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;1;False;9;FLOAT;4;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;292;829.6819,391.2721;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;286;-205.4187,631.4487;Inherit;True;ColorShartSlot;-1;;743;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;3;False;9;FLOAT;4;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;287;-240.3081,3640.752;Inherit;True;ColorShartSlot;-1;;744;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;3;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;289;855.199,86.99012;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;276;-230.0205,2638.806;Inherit;True;ColorShartSlot;-1;;746;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;3;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;288;-204.9043,874.6049;Inherit;True;ColorShartSlot;-1;;741;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;4;False;9;FLOAT;4;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;285;-218.9466,397.584;Inherit;True;ColorShartSlot;-1;;740;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;2;False;9;FLOAT;4;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;303;362.0073,1453.096;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;302;361.3183,1187.068;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;299;359.8215,1720.239;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;296;356.4282,1984.446;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;305;601.9778,2839.576;Float;False;Property;_MRE2;MRE 2;18;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;298;552.2485,5158.444;Float;False;Property;_MRE12;MRE 12;28;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;301;554.292,4942.316;Float;False;Property;_MRE11;MRE 11;27;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;300;557.5464,6229.815;Float;False;Property;_MRE16;MRE 16;32;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;297;559.8184,5802.033;Float;False;Property;_MRE14;MRE 14;30;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;311;556.5591,3968.35;Float;False;Property;_MRE7;MRE 7;23;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;307;554.6787,5568.261;Float;False;Property;_MRE13;MRE 13;29;0;Create;True;0;0;0;False;1;Space(10);False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;313;593.4414,2625.924;Float;False;Property;_MRE1;MRE 1;17;0;Create;True;0;0;0;False;1;Header(Metallic(R) Rough(G) Emmission(B));False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;295;1040.498,75.77015;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;304;561.4189,3756.696;Float;False;Property;_MRE6;MRE 6;22;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;294;561.9038,6013.687;Float;False;Property;_MRE15;MRE 15;31;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;308;597.8821,3050.848;Float;False;Property;_MRE3;MRE 3;19;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;309;560.7014,3530.267;Float;False;Property;_MRE5;MRE 5;21;0;Create;True;0;0;0;False;1;Space(10);False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;310;554.5208,4730.661;Float;False;Property;_MRE10;MRE 10;26;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;314;600.7974,3259.348;Float;False;Property;_MRE4;MRE 4;20;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;306;549.3809,4496.889;Float;False;Property;_MRE9;MRE 9;25;0;Create;True;0;0;0;False;1;Space(10);False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;312;554.5156,4184.477;Float;False;Property;_MRE8;MRE 8;24;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;316;867.7078,6001.553;Inherit;True;ColorShartSlot;-1;;754;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;3;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;319;860.446,3741.301;Inherit;True;ColorShartSlot;-1;;747;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;2;False;9;FLOAT;3;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;322;862.8147,3533.231;Inherit;True;ColorShartSlot;-1;;748;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;1;False;9;FLOAT;3;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;315;851.7717,5565.973;Inherit;True;ColorShartSlot;-1;;749;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;1;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;318;855.679,4178.713;Inherit;True;ColorShartSlot;-1;;756;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;4;False;9;FLOAT;3;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;324;877.594,2625.656;Inherit;True;ColorShartSlot;-1;;751;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;1;False;9;FLOAT;4;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;321;873.4221,3262.493;Inherit;True;ColorShartSlot;-1;;752;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;4;False;9;FLOAT;4;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;320;860.7371,4715.267;Inherit;True;ColorShartSlot;-1;;753;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;2;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;323;873.4548,3051.996;Inherit;True;ColorShartSlot;-1;;755;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;3;False;9;FLOAT;4;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;317;866.0349,5786.639;Inherit;True;ColorShartSlot;-1;;750;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;2;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;325;1321.758,324.4118;Float;False;Property;_GradientScale;Gradient Scale;37;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;328;1325.247,67.20997;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;327;862.4109,4930.181;Inherit;True;ColorShartSlot;-1;;758;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;3;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;330;858.7087,6224.051;Inherit;True;ColorShartSlot;-1;;759;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;4;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;331;846.4747,4494.602;Inherit;True;ColorShartSlot;-1;;762;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;1;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;326;1328.576,417.155;Float;False;Property;_GradientOffset;Gradient Offset;38;0;Create;True;0;0;0;False;0;False;0;0.23;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;334;862.1199,3956.216;Inherit;True;ColorShartSlot;-1;;761;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;3;False;9;FLOAT;3;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;329;689.7629,1490.179;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;332;853.4119,5152.679;Inherit;True;ColorShartSlot;-1;;757;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;4;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;333;875.3811,2841.581;Inherit;True;ColorShartSlot;-1;;760;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;1,1,1,1;False;3;FLOAT;2;False;9;FLOAT;4;False;7;FLOAT;4;False;8;FLOAT;4;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;335;1245.005,5733.872;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;337;1242.845,2960.129;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;339;1226.362,3693.714;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;338;1333.067,513.5909;Float;False;Property;_GradientPower;Gradient Power;39;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;341;1603.028,106.3281;Inherit;True;3;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;340;1733.138,385.1413;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;336;1239.708,4662.5;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;342;1910.574,105.6353;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;343;1955.269,393.4111;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;344;1633.28,3281.224;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;345;2197.731,1690.528;Inherit;False;Property;_EmissionPower1;Emission Power;33;0;Create;True;0;0;0;False;1;Header(Emmision);False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;346;2187.081,1825.551;Inherit;True;False;False;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;347;2203.216,104.3341;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;349;2249.315,1389.258;Inherit;True;False;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;350;2501.627,1677.187;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;348;2469.75,101.3337;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;357;2852.409,876.689;Float;False;Property;_Tint;Tint;0;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;355;2617.921,1385.25;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;351;2787.695,1612.243;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;353;3107.01,689.065;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;354;2243.338,1191.311;Inherit;True;True;False;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3722.291,1294.892;Float;False;True;-1;7;ASEMaterialInspector;0;0;Standard;Malbers/Color4x4;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.1;True;True;0;False;Opaque;;Geometry;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;259;1;256;0
WireConnection;280;38;272;0
WireConnection;277;38;269;0
WireConnection;281;38;268;0
WireConnection;293;38;258;0
WireConnection;279;38;274;0
WireConnection;284;38;262;0
WireConnection;282;38;260;0
WireConnection;278;38;257;0
WireConnection;283;38;273;0
WireConnection;291;38;267;0
WireConnection;290;38;265;0
WireConnection;292;0;270;0
WireConnection;286;38;261;0
WireConnection;287;38;266;0
WireConnection;289;0;259;0
WireConnection;289;1;264;0
WireConnection;276;38;263;0
WireConnection;288;38;271;0
WireConnection;285;38;275;0
WireConnection;303;0;278;0
WireConnection;303;1;280;0
WireConnection;303;2;291;0
WireConnection;303;3;284;0
WireConnection;302;0;290;0
WireConnection;302;1;285;0
WireConnection;302;2;286;0
WireConnection;302;3;288;0
WireConnection;299;0;279;0
WireConnection;299;1;281;0
WireConnection;299;2;276;0
WireConnection;299;3;277;0
WireConnection;296;0;293;0
WireConnection;296;1;282;0
WireConnection;296;2;287;0
WireConnection;296;3;283;0
WireConnection;295;0;289;0
WireConnection;295;1;292;0
WireConnection;316;38;294;0
WireConnection;319;38;304;0
WireConnection;322;38;309;0
WireConnection;315;38;307;0
WireConnection;318;38;312;0
WireConnection;324;38;313;0
WireConnection;321;38;314;0
WireConnection;320;38;310;0
WireConnection;323;38;308;0
WireConnection;317;38;297;0
WireConnection;328;0;295;0
WireConnection;327;38;301;0
WireConnection;330;38;300;0
WireConnection;331;38;306;0
WireConnection;334;38;311;0
WireConnection;329;0;302;0
WireConnection;329;1;303;0
WireConnection;329;2;299;0
WireConnection;329;3;296;0
WireConnection;332;38;298;0
WireConnection;333;38;305;0
WireConnection;335;0;315;0
WireConnection;335;1;317;0
WireConnection;335;2;316;0
WireConnection;335;3;330;0
WireConnection;337;0;324;0
WireConnection;337;1;333;0
WireConnection;337;2;323;0
WireConnection;337;3;321;0
WireConnection;339;0;322;0
WireConnection;339;1;319;0
WireConnection;339;2;334;0
WireConnection;339;3;318;0
WireConnection;341;0;328;0
WireConnection;341;1;325;0
WireConnection;341;2;326;0
WireConnection;340;0;329;0
WireConnection;336;0;331;0
WireConnection;336;1;320;0
WireConnection;336;2;327;0
WireConnection;336;3;332;0
WireConnection;342;0;341;0
WireConnection;342;1;338;0
WireConnection;343;0;340;0
WireConnection;344;0;337;0
WireConnection;344;1;339;0
WireConnection;344;2;336;0
WireConnection;344;3;335;0
WireConnection;346;0;344;0
WireConnection;347;0;342;0
WireConnection;347;1;343;0
WireConnection;349;0;344;0
WireConnection;350;0;345;0
WireConnection;350;1;346;0
WireConnection;348;0;347;0
WireConnection;355;0;349;0
WireConnection;351;0;329;0
WireConnection;351;1;350;0
WireConnection;353;0;348;0
WireConnection;353;1;329;0
WireConnection;353;2;357;0
WireConnection;354;0;344;0
WireConnection;0;0;353;0
WireConnection;0;2;351;0
WireConnection;0;3;354;0
WireConnection;0;4;355;0
ASEEND*/
//CHKSM=2F743E3855742334E0358A4BF2769E57900F7386