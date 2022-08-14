
using TMPro;
using UnityEngine;

namespace Shiang
{
    /// <summary>
    /// Adopted from 
    /// https://web.archive.org/web/20201111204658/http://wiki.unity3d.com/index.php?title=FramesPerSecond#CSharp_HUDFPS.cs
    /// </summary>
    public class FramePerSecondUi : MonoBehaviour
    {
        // Attach this to a GUIText to make a frames/second indicator.
        //
        // It calculates frames/second over each updateInterval,
        // so the display does not keep changing wildly.
        //
        // It is also fairly accurate at very low FPS counts (<10).
        // We do this not by simply counting frames per interval, but
        // by accumulating FPS for each frame. This way we end up with
        // correct overall FPS even if the interval renders something like
        // 5.5 frames.

        float _accum = 0; // FPS accumulated over the interval
        float _timeleft; // Left time for current interval
        int _frames = 0; // Frames drawn over the interval

        [SerializeField] TMP_Text _text;
        [SerializeField] float _updateInterval = 0.5f;

        void Start()
        {
            if (!_text)
            {
                Debug.Log("UtilityFramesPerSecond needs a TMP_Text component!");
                enabled = false;
                return;
            }
            _timeleft = _updateInterval;
        }

        void Update()
        {
            _timeleft -= Time.deltaTime;
            _accum += Time.timeScale / Time.deltaTime;
            ++_frames;

            // Interval ended - update GUI text and start new interval
            if (_timeleft <= 0.0)
            {
                // display two fractional digits (f2 format)
                float fps = _accum / _frames;
                string format = string.Format("FPS {0:F2}", fps);
                _text.text = format;

                if (fps < 30)
                    _text.color = Color.yellow;
                else
                    if (fps < 10)
                    _text.color = Color.red;
                else
                    _text.color = Color.green;

                _timeleft = _updateInterval;
                _accum = 0.0F;
                _frames = 0;
            }
        }
    }
}