using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public IEnumerator Shake(float duration,float magnitude)
    {
        Vector3 originPos = transform.localPosition;
        float elaped = 0.0f;

        while(elaped < duration)
        {
            //float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 3f) * magnitude;

            transform.localPosition = new Vector3(0, y, originPos.z);
            elaped += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originPos;
    }
}
