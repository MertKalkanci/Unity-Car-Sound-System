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
        public bool sparkWhileAccelleration;
        [Range(0f, 1f)] public float rpm,sparkRate;
        public EngineAudipSetup setup;
        private CarAudio idle, lowAcceleration, mediumAcceleration, highAcceleration, limiter,spark1A,spark2A,spark3A;
        private AudioSource idleSource,lowAccel, medAccel, highAccel, limit,spark1,spark2,spark3;
        private float oldRpm = 0;
        void Awake()
        {
            getSetup();

            //initialise audio sources
            lowAccel = gameObject.AddComponent<AudioSource>();
            medAccel = gameObject.AddComponent<AudioSource>();
            highAccel = gameObject.AddComponent<AudioSource>();
            limit = gameObject.AddComponent<AudioSource>();
            idleSource = gameObject.AddComponent<AudioSource>();
            spark1 = gameObject.AddComponent<AudioSource>();
            spark2 = gameObject.AddComponent<AudioSource>();
            spark3 = gameObject.AddComponent<AudioSource>();


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
            sparkRate = setup.sparkRate;

            idle = setup.idle;
            lowAcceleration = setup.lowAcceleration;
            mediumAcceleration = setup.mediumAcceleration;
            highAcceleration = setup.highAcceleration;
            limiter = setup.limiter;
            
            spark1A = setup.spark1;
            spark2A = setup.spark2;
            spark3A = setup.spark3;
        }

        public void UpdateEngineSetup()
        {
            getSetup(); //to prevent bugs when updating engine audio from other scripts

            spark1.clip = spark1A.audio;
            spark2.clip = spark2A.audio;
            spark3.clip = spark3A.audio;

            lowAccel.clip = lowAcceleration.audio;
            medAccel.clip = mediumAcceleration.audio;
            highAccel.clip = highAcceleration.audio;
            limit.clip = limiter.audio;
            idleSource.clip = idle.audio;

            lowAccel.Play();
            medAccel.Play();
            highAccel.Play();
            limit.Play();
            idleSource.Play();
        }

        void Update()
        {

            rpm = rpm > 1 ? 1 : rpm;
            rpm = rpm < 0 ? 0 : rpm;

            if (sparkWhileAccelleration)
            {
                if (Mathf.Abs(oldRpm - rpm) > (1 - sparkRate))
                {
                    spark();
                }
                else if (oldRpm - rpm > (1 - sparkRate))
                {
                    spark();
                }
            }

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

            oldRpm = rpm;
        }
        private void spark()
        {
            switch ((int)UnityEngine.Random.Range(1, 3))
            {
                case 1:
                    spark1.Play();
                    break;
                case 2:
                    spark2.Play();
                    break;
                case 3:
                    spark3.Play();
                    break;
                default:
                    spark3.Play();
                    break;
            }
        }
    }
}