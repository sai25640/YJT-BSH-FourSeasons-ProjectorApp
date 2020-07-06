Shader "Custom/MyShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
			//原始代码保留
		_Num("Num",float) = 0.5 //方便调试的参数设置
    }
    SubShader
    {
		Tags { "Queue" = "Transparent"  "RenderType" = "Transparent"}

        LOD 200

        CGPROGRAM

		#pragma surface surf NoLighting alpha:auto
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float _Num;


		void surf(Input IN, inout SurfaceOutput o) //表面着色函数 每个顶点的颜色 都在 o 中反映
		{
			o.Emission = tex2D(_MainTex, IN.uv_MainTex).rgb;

			//这里给输出的Alpha赋值
			//由于我的视频没处理好，不是很对称。。。
			//对称的话应该是0.5
			//这里和0.43比较的是UV贴图的x轴的坐标（0~1,0.5就表示横坐标的一半）
			//右半边视频不显示，所以赋值alpha=0
			if (IN.uv_MainTex.y <=0.5)
			{
				o.Alpha = 0;
			}
			else
			{
				//左半边视频的Alpha值和右半边黑白视频的RGB的值一样
				//因为我这边处理的A黑白视频不是很好，所以获得了右半边UV的RGB后得比较一下
				//再给Alpha赋值
				o.Alpha = _Color.a*tex2D(_MainTex, float2(IN.uv_MainTex.x , IN.uv_MainTex.y - 0.5)).rgb;
			}
		}
        ENDCG
    }
}
