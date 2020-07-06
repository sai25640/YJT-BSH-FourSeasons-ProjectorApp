using UnityEngine;
using QFramework;

namespace FourSeasons
{
	public partial class EffectVideo : ViewController
	{
		void Start()
		{
			// Code Here
		}

	    void Update()
	    {
	        if (Input.GetKeyDown(KeyCode.Q))
	        {               
	                ResLoader.Allocate().LoadSync<GameObject>("SpringEffect").Instantiate()
	                                .GetComponent<Effect>()
	                                .ApplySelfTo(e => e.Init(transform));
	        }
            if (Input.GetKeyDown(KeyCode.W))
            {
                ResLoader.Allocate().LoadSync<GameObject>("SummerEffect").Instantiate()
                    .GetComponent<Effect>()
                    .ApplySelfTo(e => e.Init(transform));
            }
        }
	}
}
