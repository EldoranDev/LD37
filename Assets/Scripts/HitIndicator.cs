using UnityEngine;
using UnityEngine.UI;

public class HitIndicator : MonoBehaviour {

    public float HitLength;

    public Image ColorBlender;

    float _currentHitlength;

    public void Hit()
    {
        _currentHitlength = HitLength;
        var c = ColorBlender.color;
        c.a = 0.5f;

        ColorBlender.color = c;
    }

    // Update is called once per frame
    void Update () {

		if(_currentHitlength > 0)
        {
            _currentHitlength -= Time.deltaTime;
            var c = ColorBlender.color;
            c.a *= _currentHitlength / HitLength;
            ColorBlender.color = c;
        }
	}
}
