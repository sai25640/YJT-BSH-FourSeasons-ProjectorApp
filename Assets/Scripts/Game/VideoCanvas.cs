using UnityEngine;
using QFramework;
using UniRx;
namespace FourSeasons
{
	public partial class VideoCanvas : ViewController
	{
		void Start()
		{
		    Observable.EveryUpdate().Subscribe(_ =>
		    {
                //WholeVideo.frameCount
		    }).AddTo(this);
		}

	}
}
