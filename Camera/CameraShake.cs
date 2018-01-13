using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IzzoLib
{
	/// <summary>
	/// Attach this to the camera and call Shake() with intensity as parameters.
	/// You can choose shake intensity and if it shakes rotation, position, or both.
	/// </summary>
	public class CameraShake : MonoBehaviour {

		private bool _shake_position;
		private bool _shake_rotation;

		private float _shake_intensity_min;
		private float _shake_intensity_max;
		private float _shake_decay;

		private Vector3 _original_position;
		private Quaternion _original_rotation;

		private bool _is_shake_running = false;

		/// <summary>
		/// Shake the camera. 
		/// </summary>
		/// <param name="shake_intensity_min">Shake intensity minimum.</param>
		/// <param name="shake_intensity_max">Shake intensity maximum.</param>
		/// <param name="shake_pos">If set to <c>true</c> shake position.</param>
		/// <param name="shake_rot">If set to <c>true</c> shake rot.</param>
		/// <param name="shake_decay">Shake decay.</param>
		public void Shake(float shake_intensity_min, float shake_intensity_max, bool shake_pos = true, bool shake_rot = false, float shake_decay = 0.02f)
		{
			this._shake_intensity_min = shake_intensity_min;
			this._shake_intensity_max = shake_intensity_max;
			this._shake_position = shake_pos;
			this._shake_rotation = shake_rot;
			this._shake_decay = shake_decay;
			_original_position = transform.position;
			_original_rotation = transform.rotation;
			StartCoroutine ("ShakeCamera");
		}

		private IEnumerator ShakeCamera()
		{
			if (!_is_shake_running)
			{
				_is_shake_running = true;
				float current_shake_intensity = Random.Range (_shake_intensity_min, _shake_intensity_max);

				while (current_shake_intensity > 0)
				{
					if (_shake_position)
						transform.position = _original_position + Random.insideUnitSphere * current_shake_intensity;

					if (_shake_rotation)
					{
						transform.rotation = new Quaternion(_original_rotation.x + Random.Range(-current_shake_intensity, current_shake_intensity) * .2f,
															_original_rotation.y + Random.Range(-current_shake_intensity, current_shake_intensity) * .2f,
															_original_rotation.z + Random.Range(-current_shake_intensity, current_shake_intensity) * .2f,
															_original_rotation.w + Random.Range(-current_shake_intensity, current_shake_intensity) * .2f);
					}
					current_shake_intensity -= _shake_decay;
					yield return null;
				}

				_is_shake_running = false;

			}
		}
	}

}
