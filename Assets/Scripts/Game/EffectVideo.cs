using Common;
using UnityEngine;
using QFramework;
using EventType = Common.EventType;
using System;

namespace FourSeasons
{
	public partial class EffectVideo : ViewController
	{
		void Start()
		{
			// Code Here
            EventCenter.AddListener<string>(EventType.PlaySpringEffect, PlaySpringEffect);
		    EventCenter.AddListener<string>(EventType.PlaySummerEffect, PlaySummerEffect);
		    EventCenter.AddListener(EventType.PlayFallEffect, PlayFallEffect);
		    EventCenter.AddListener(EventType.PlayWinterEffect, PlayWinterEffect);
        }

	    void OnDestroy()
	    {
            EventCenter.RemoveListener<string>(EventType.PlaySpringEffect, PlaySpringEffect);
            EventCenter.RemoveListener<string>(EventType.PlaySummerEffect, PlaySummerEffect);
            EventCenter.RemoveListener(EventType.PlayFallEffect, PlayFallEffect);
            EventCenter.RemoveListener(EventType.PlayWinterEffect, PlayWinterEffect);
        }

        private void PlaySpringEffect(string name)
        {
            ResLoader.Allocate().LoadSync<GameObject>(name).Instantiate()
                .GetComponent<Effect>()
                .ApplySelfTo(e => e.Init(transform));
        }

        private void PlaySummerEffect(string name)
        {
            ResLoader.Allocate().LoadSync<GameObject>(name).Instantiate()
                .GetComponent<Effect>()
                .ApplySelfTo(e => e.Init(transform));
        }

        private void PlayFallEffect()
        {
            ResLoader.Allocate().LoadSync<GameObject>("FallEffect").Instantiate()
                .GetComponent<Effect>()
                .ApplySelfTo(e => e.Init(transform));
        }

        private void PlayWinterEffect()
        {
 
        }

        void Update()
	    {
            //OnlyForTest
	        //if (Input.GetKeyDown(KeyCode.Q))
	        //{               
	        //        ResLoader.Allocate().LoadSync<GameObject>("SpringEffect").Instantiate()
	        //                        .GetComponent<Effect>()
	        //                        .ApplySelfTo(e => e.Init(transform));
	        //}
         //   if (Input.GetKeyDown(KeyCode.W))
         //   {
         //       ResLoader.Allocate().LoadSync<GameObject>("SummerEffect").Instantiate()
         //           .GetComponent<Effect>()
         //           .ApplySelfTo(e => e.Init(transform));
         //   }
         //   if (Input.GetKeyDown(KeyCode.E))
         //   {
         //       ResLoader.Allocate().LoadSync<GameObject>("FullEffect").Instantiate()
         //           .GetComponent<Effect>()
         //           .ApplySelfTo(e => e.Init(transform));
         //   }
        }
	}
}
