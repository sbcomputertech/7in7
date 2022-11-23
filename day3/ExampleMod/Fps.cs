using UnityEngine;

namespace ExampleMod;

public class Fps : MonoBehaviour
{
    private readonly double updateInterval = 0.5;
    private double _lastInterval; // Last interval end time
    private int _frames = 0; // Frames over current interval
    public static double fps; // Current FPS
    
    private void Start() {
        _lastInterval = Time.realtimeSinceStartup;
        _frames = 0;
    }
    
    private void Update() {
        ++_frames;
        var timeNow = Time.realtimeSinceStartup;
        if( timeNow > _lastInterval + updateInterval )
        {
            fps = _frames / (timeNow - _lastInterval);
            _frames = 0;
            _lastInterval = timeNow;
        }
    }
}