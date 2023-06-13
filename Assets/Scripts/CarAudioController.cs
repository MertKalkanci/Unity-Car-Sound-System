using System;
using System.Collections.Generic;
using UnityEngine;
namespace MertStudio.Car.Sounds
{
    [Serializable]
    public class CarAudio // audio class
    {
        public AudioClip audio;
        public AnimationCurve volumeCurve,pitchCurve;
    }
    public class CarAudioController : MonoBehaviour
    {
        [Range(0f, 1f)] public float rpm;
        public EngineAudipSetup setup;
        private CarAudio idle, lowAcceleration, mediumAcceleration, highAcceleration, limiter;
        private AudioSource idleSource,lowAccel, medAccel, highAccel, limit;
        void Awake()
        {
            getSetup();

            //initialise audio sources
            lowAccel = gameObject.AddComponent<AudioSource>();
            medAccel = gameObject.AddComponent<AudioSource>();
            highAccel = gameObject.AddComponent<AudioSource>();
            limit = gameObject.AddComponent<AudioSource>();
            idleSource = gameObject.AddComponent<AudioSource>();

            lowAccel.loop = true;
            medAccel.loop = true;
            highAccel.loop = true;
            limit.loop = true;
            idleSource.loop = true;

            UpdateEngineSetup();

            lowAccel.Play();
            medAccel.Play();
            highAccel.Play();
            limit.Play();
            idleSource.Play();

        }
        private void getSetup()
        {
            idle = setup.idle;
            lowAcceleration = setup.lowAcceleration;
            mediumAcceleration = setup.mediumAcceleration;
            highAcceleration = setup.highAcceleration;
            limiter = setup.limiter;
        }
        public void UpdateEngineSetup()
        {
            getSetup(); //to prevent bugs when updating engine audio from other scripts

            lowAccel.clip = lowAcceleration.audio;
            medAccel.clip = mediumAcceleration.audio;
            highAccel.clip = highAcceleration.audio;
            limit.clip = limiter.audio;
            idleSource.clip = idle.audio;
        }

        void Update()
        {
            rpm = rpm > 1 ? 1 : rpm;
            rpm = rpm < 0 ? 0 : rpm;

            lowAccel.volume = lowAcceleration.volumeCurve.Evaluate(rpm);
            lowAccel.pitch = lowAcceleration.volumeCurve.Evaluate(rpm);

            medAccel.volume = mediumAcceleration.volumeCurve.Evaluate(rpm);
            medAccel.pitch = mediumAcceleration.volumeCurve.Evaluate(rpm);

            highAccel.volume = highAcceleration.volumeCurve.Evaluate(rpm);
            highAccel.pitch = highAcceleration.volumeCurve.Evaluate(rpm);


            limit.volume = limiter.volumeCurve.Evaluate(rpm);
            limit.pitch = limiter.volumeCurve.Evaluate(rpm);

            idleSource.volume = idle.volumeCurve.Evaluate(rpm);
            idleSource.pitch = idle.volumeCurve.Evaluate(rpm);
        }
    }
}