using UnityEngine;

namespace MertStudio.Car.Sounds
{
    [CreateAssetMenu(fileName = "Engine", menuName = "Car/EngineSound", order = 3)]
    public class EngineAudipSetup : ScriptableObject
    {
        public CarAudio idle, lowAcceleration, mediumAcceleration, highAcceleration, limiter;
    }

}