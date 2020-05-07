Shader "Hidden/GlassesEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OriginalTex("Texture", 2D) = "white" {}

		_BlurSamplingRate("Amount of samples for bluring", Range(2, 100)) = 30
		_BlurSize("Intenstity of blur", Range(-0.32, 0.32)) = 0.0

		_EllipsisCenter1("Center point of first ellipsis", Vector) = (0.25, 0.5, 0.0, 0.0)
		_EllipsisCenter2("Center point of second ellipsis", Vector) = (0.75, 0.5, 0.0, 0.0)
		_EllipsisA("Length of ellipsis' semi-major (horizontal) axises", Range(0.05, 0.25)) = 0.1
		_EllipsisB("Length of ellipsis' semi-minor (vertical) axises", Range(0.05, 0.25)) = 0.1

		_LensesColor("Lenses color", Color) = (0.5, 0.0, 0.5, 1.0) // color
	}

	CGINCLUDE
	float EllipsisPointSDF(float2 ellipsisCenter, float a, float b, float2 testedCoord)
	{
		//Signed Distance Field function for detecting if point is inside lens ellipsis
		//Using ellipsis equation
		float x2 = (testedCoord.x - ellipsisCenter.x) * (testedCoord.x - ellipsisCenter.x);
		float y2 = (testedCoord.y - ellipsisCenter.y) * (testedCoord.y - ellipsisCenter.y);
		return (x2 / (a * a)) + (y2 / (b * b)) - 1.0;
	}
	ENDCG

	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		//Vertical Blur
		Pass
		{
			CGPROGRAM
			//include useful shader functions
			#include "UnityCG.cginc"

			//define vertex and fragment shader
			#pragma vertex vert
			#pragma fragment frag

			//Original texture
			sampler2D _MainTex;
			
			//Blur settings
			uniform int _BlurSamplingRate;
			uniform float _BlurSize;

			//the object data that's put into the vertex shader
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			//the data that's used to generate fragments and can be read by the fragment shader
			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//the vertex shader
			v2f vert(appdata v)
			{
				v2f o;
				//convert the vertex positions from object space to clip space so they can be rendered
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			//the fragment shader
			fixed4 frag(v2f i) : SV_TARGET
			{
				//Init output color variable
				float4 outputColor = 0;

				//Iterate over blur samples
				for (float index = 0; index < _BlurSamplingRate; index++)
				{
					//Calculate the offset of the sample
					float offset = (index / (_BlurSamplingRate - 1) - 0.5) * _BlurSize;
					//Get uv coordinate of sample
					float2 uv = i.uv + float2(0, offset);
					//Accumulate samples
					outputColor += tex2D(_MainTex, uv);
				}

				//Get the average of samples
				return (outputColor / _BlurSamplingRate);
			}

			ENDCG
		}

		//Horizontal Blur
		Pass
		{
			CGPROGRAM
			//include useful shader functions
			#include "UnityCG.cginc"

			//define vertex and fragment shader
			#pragma vertex vert
			#pragma fragment frag

			//Vertically blurred texture
			sampler2D _MainTex;

			//Blur settings
			uniform int _BlurSamplingRate;
			uniform float _BlurSize;

			//the object data that's put into the vertex shader
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			//the data that's used to generate fragments and can be read by the fragment shader
			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//the vertex shader
			v2f vert(appdata v)
			{
				v2f o;
				//convert the vertex positions from object space to clip space so they can be rendered
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			//the fragment shader
			fixed4 frag(v2f i) : SV_TARGET
			{
				//Init output color variable
				float4 outputColor = 0;

				//Calculate inverted aspect ratio
				float invertedAspectRatio = _ScreenParams.y / _ScreenParams.x;

				//Iterate over blur samples
				for (float index = 0; index < _BlurSamplingRate; index++)
				{
					//Calculate the offset of the sample
					float offset = (index / (_BlurSamplingRate - 1) - 0.5) * _BlurSize * invertedAspectRatio;
					//Get uv coordinate of sample
					float2 uv = i.uv + float2(offset, 0);
					//Accummulate samples
					outputColor += tex2D(_MainTex, uv);
				}

				//Get the average of samples
				return (outputColor / _BlurSamplingRate);
			}

			ENDCG
		}

		//Lens
		Pass
		{
			CGPROGRAM
			//include useful shader functions
			#include "UnityCG.cginc"

			#pragma multi_compile _SAMPLES_LOW _SAMPLES_MEDIUM _SAMPLES_HIGH
			#pragma shader_feature GAUSS

			//define vertex and fragment shader
			#pragma vertex vert
			#pragma fragment frag

			//Blurred texture
			sampler2D _MainTex;

			//Original texture
			sampler2D _OriginalTex;

			//Lens settings
			uniform float2 _EllipsisCenter1;
			uniform float2 _EllipsisCenter2;

			uniform float _EllipsisA;
			uniform float _EllipsisB;

			uniform float4 _LensesColor;

			//the object data that's put into the vertex shader
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			//the data that's used to generate fragments and can be read by the fragment shader
			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//the vertex shader
			v2f vert(appdata v)
			{
				v2f o;
				//convert the vertex positions from object space to clip space so they can be rendered
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			//the fragment shader
			fixed4 frag(v2f i) : SV_TARGET
			{
				float4 blurredFragColor = tex2D(_MainTex, i.uv);
				float4 originalFragColor = tex2D(_OriginalTex, i.uv);

				//Calculate aspect ratio
				float aspectRatio = _ScreenParams.x / _ScreenParams.y;

				//Point distances from ellipsis
				// de* > 0 outside ellipsis
				// de* == 0 on the ellipsis
				// de* < 0 inside ellipsis
				float de1 = EllipsisPointSDF(_EllipsisCenter1, _EllipsisA, _EllipsisB * aspectRatio, i.uv);
				float de2 = EllipsisPointSDF(_EllipsisCenter2, _EllipsisA, _EllipsisB * aspectRatio, i.uv);

				bool pointInsideEllipsis = de1 <= 0 || de2 <= 0;
				return !pointInsideEllipsis * blurredFragColor + pointInsideEllipsis * (originalFragColor * _LensesColor);
			}

			ENDCG
		}
	}
}