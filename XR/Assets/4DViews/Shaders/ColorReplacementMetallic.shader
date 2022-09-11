Shader "4DViews/ColorReplacement/Metallic"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_inputColor("InputColor", Color) = (0.0,0.0,1.0,1)
		_replacementColor("ReplacementColor", Color) = (0.5,0.5,0.5,0)
		_range("Range ", Float) = 0.3
		_metallic("Metallic ", Float) = 2
		_smoothness("Smoothness ", Float) = 2
		_fuzziness("Fuzziness ", Float) = 0
		_normalOffset("NormalOffset ", Float) = 0
		_normalStrength("NormalStrength ", Float) = 0
		_emissionColor("EmissionColor", Color) = (0,0,0,0)
		_alpha("Alpha", Range(0.0,1.0)) = 1
	}

		SubShader
		{
			Tags {"RenderType" = "Transparent" "Queue" = "Transparent"}
				LOD 200
				 Pass {
					 ColorMask 0
				 }
			// Render normally

				ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha
				ColorMask RGB

		CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows alpha:fade
		   #pragma target 3.0

			sampler2D _MainTex;

			struct Input
			{
				float2 uv_MainTex;
				half3 normal;
				float3 pos;
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _inputColor;
			float4 _replacementColor;
			float _range;
			float _fuzziness;
			//float _fresnel;
			float _normalOffset;
			float _normalStrength;
			float _metallic;
			float _smoothness;
			float4 _emissionColor;
			float _alpha;
			CBUFFER_END

			half _Glossiness;
			half _Metallic;


			struct SurfaceDescriptionInputs
			{

				float3 WorldSpaceNormal;
				float3 WorldSpaceViewDirection;
				float2 uv0;

			};

			struct SurfaceDescription
			{
				float3 Albedo;
				float3 Normal;
				float3 Emission;
				float Metallic;
				float Smoothness;
				float Occlusion;
				float Alpha;
				float AlphaClipThreshold;
			};

			void Unity_ColorspaceConversion_RGB_HSV_float(float3 In, out float3 Out)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 P = lerp(float4(In.bg, K.wz), float4(In.gb, K.xy), step(In.b, In.g));
				float4 Q = lerp(float4(P.xyw, In.r), float4(In.r, P.yzx), step(P.x, In.r));
				float D = Q.x - min(Q.w, Q.y);
				float  E = 1e-10;
				Out = float3(abs(Q.z + (Q.w - Q.y) / (6.0 * D + E)), D / (Q.x + E), Q.x);
			}

			void Unity_ReplaceColor_float(float3 In, float3 From, float3 To, float Range, out float3 Out, float Fuzziness)
			{
				//because hue is looping
				if (From[0] - In[0] > 0.5) In[0] += 1;
				else if (In[0] - From[0] > 0.5) From[0] += 1;

				float3 In2 = In;
				float3 From2 = From;
				In2[0] *= In2[0];
				From2[0] *= From2[0];
				float Distance = distance(From2, In2);

				//float Distance = distance(From, In);
				Out = lerp(To, In, saturate((Distance - Range) / max(Fuzziness, 1e-5f)));
			}

			void Unity_ColorMask_float(float3 In, float3 MaskColor, float Range, out float Out, float Fuzziness)
			{
				float Distance = distance(MaskColor, In);
				Out = saturate(1 - (Distance - Range) / max(Fuzziness, 1e-5));
			}

			void Unity_Multiply_float(float A, float B, out float Out)
			{
				Out = A * B;
			}

			void Unity_Subtract_float(float A, float B, out float Out)
			{
				Out = A - B;
			}

			void Unity_Add_float(float A, float B, out float Out)
			{
				Out = A + B;
			}

			void Unity_Saturate_float(float In, out float Out)
			{
				Out = saturate(In);
			}

			void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
			{
				RGBA = float4(R, G, B, A);
				RGB = float3(R, G, B);
				RG = float2(R, G);
			}

			void Unity_ColorspaceConversion_HSV_RGB_float(float3 In, out float3 Out)
			{
				float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
				float3 P = abs(frac(In.xxx + K.xyz) * 6.0 - K.www);
				Out = In.z * lerp(K.xxx, saturate(P - K.xxx), In.y);
			}


			void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
			{
				Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
			}

			void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
			{
				Out = A * B;
			}

			SurfaceDescription PopulateSurfaceData(SurfaceDescriptionInputs IN)
			{
				SurfaceDescription surface = (SurfaceDescription)0;

				float4 _inputTexture_RGBA = tex2D(_MainTex, IN.uv0);
				float3 _inputTexture_HSV;
				Unity_ColorspaceConversion_RGB_HSV_float((_inputTexture_RGBA.xyz), _inputTexture_HSV);

				float3 _inputColor_HSV;
				Unity_ColorspaceConversion_RGB_HSV_float((_inputColor.xyz), _inputColor_HSV);

				float3 _replacementColor_HSV;
				Unity_ColorspaceConversion_RGB_HSV_float((_replacementColor.xyz), _replacementColor_HSV);

				float3 _ReplaceColor;
				Unity_ReplaceColor_float(_inputTexture_HSV, _inputColor_HSV, _replacementColor_HSV, _range, _ReplaceColor, _fuzziness);

				float _ColorMask;
				Unity_ColorMask_float(_ReplaceColor, _replacementColor_HSV, 0, _ColorMask, 0);

				/*
							float _resultSaturation0;
							Unity_Multiply_float(_ColorMask, _replacementColor_HSV[1], _resultSaturation0);

							float _resultSaturation1;
							Unity_Subtract_float(_replacementColor_HSV[1], _inputColor_HSV[1], _resultSaturation1);

							float _resultSaturation2;
							Unity_Multiply_float(_ColorMask, _resultSaturation1, _resultSaturation2);

							float _resultSaturation3;
							Unity_Add_float(_resultSaturation0, _resultSaturation2, _resultSaturation3);

							float _resultSaturation4;
							Unity_Add_float(_resultSaturation3, _inputTexture_HSV[1], _resultSaturation4);

							float _resultSaturation5;
							Unity_Saturate_float(_resultSaturation4, _resultSaturation5);
				*/

							float _resultValue0;
							Unity_Multiply_float(_ColorMask, _replacementColor_HSV[2], _resultValue0);

							float _resultValue1;
							Unity_Subtract_float(_replacementColor_HSV[2], _inputColor_HSV[2], _resultValue1);

							float _resultValue2;
							Unity_Multiply_float(_ColorMask, _resultValue1, _resultValue2);

							float _resultValue3;
							Unity_Add_float(_resultValue0, _resultValue2, _resultValue3);

							float _resultValue4;
							Unity_Add_float(_resultValue3, _inputTexture_HSV[2], _resultValue4);

							float _resultValue5;
							Unity_Saturate_float(_resultValue4, _resultValue5);


							float4 _resultHSV;
							float3 _dummy1;
							float2 _dummy2;
							Unity_Combine_float(_ReplaceColor[0], _ReplaceColor[1], _resultValue5, 0, _resultHSV, _dummy1, _dummy2);

							float3 _resultRGB;
							Unity_ColorspaceConversion_HSV_RGB_float((_resultHSV.xyz), _resultRGB);

							float _colorMaskSmoothness;
							Unity_Multiply_float(_resultValue5, _ColorMask, _colorMaskSmoothness);

							//float _Property_9196FF57_Out_0 = Vector1_4A23FF2D;
							//float _FresnelEffect_2E2F13EE_Out_3;
							//Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _Property_9196FF57_Out_0, _FresnelEffect_2E2F13EE_Out_3);

							//float4 _Multiply_BF7B0878_Out_2;
							//Unity_Multiply_float((_FresnelEffect_2E2F13EE_Out_3.xxxx), _emissionColor, _Multiply_BF7B0878_Out_2);

							float4 _outputEmission;
							Unity_Multiply_float((_colorMaskSmoothness.xxxx), _emissionColor, _outputEmission);


							float _alphaMask;
							Unity_Multiply_float((_ColorMask.xxxx), 1.0 - _alpha, _alphaMask);


							float _outputMetallic;
							Unity_Multiply_float(_metallic, _colorMaskSmoothness, _outputMetallic);

							float _outputSmoothness;
							Unity_Multiply_float(_smoothness, _colorMaskSmoothness, _outputSmoothness);

							surface.Albedo = _resultRGB;
							surface.Emission = (_outputEmission.xyz);
							surface.Metallic = _outputMetallic;
							surface.Smoothness = _outputSmoothness;
							surface.Occlusion = 1;
							surface.Alpha = 1.0 - _alphaMask;
							surface.AlphaClipThreshold = 0.5;
							return surface;
						}






			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				SurfaceDescriptionInputs surfaceInput = (SurfaceDescriptionInputs)0;

				surfaceInput.WorldSpaceNormal = UnityObjectToWorldNormal(IN.normal);

				surfaceInput.WorldSpaceViewDirection = _WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, IN.pos).xyz;

				surfaceInput.uv0 = IN.uv_MainTex;

				SurfaceDescription surf = PopulateSurfaceData(surfaceInput);

				o.Albedo = surf.Albedo.rgb;
				o.Metallic = surf.Metallic;
				o.Smoothness = surf.Smoothness;
				o.Alpha = surf.Alpha;
				o.Emission = surf.Emission;

			}
			ENDCG
		}
			FallBack "Diffuse"
}
