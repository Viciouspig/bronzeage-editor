﻿using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace Mumble {
    [RequireComponent(typeof(AudioSource))]
    public class MumbleAudioPlayer : MonoBehaviour {

        public float Gain = 1;
        private MumbleClient _mumbleClient;
        private UInt32 _session;
        //private AudioSource Source;
        private AudioClip _clip;
        private int _position = 0;

        const int SampleLengthSeconds = 1;
        const bool isStreamingAudio = true;

        void Start() {
            //Source = GetComponent<AudioSource>();
            print("outout rate " + AudioSettings.outputSampleRate);
            //_clip = AudioClip.Create("Received Audio", MumbleConstants.SAMPLE_RATE * SampleLengthSeconds, MumbleConstants.NUM_CHANNELS, MumbleConstants.SAMPLE_RATE, isStreamingAudio, OnAudioRead, OnAudioPositionSet);
            //Source.clip = _clip;
            //Source.Play();
        }
        public void Initialize(MumbleClient mumbleClient, UInt32 session)
        {
            _mumbleClient = mumbleClient;
            _session = session;
        }
        void OnAudioFilterRead(float[] data, int channels)
        //void OnAudioRead(float[] data)
        {
            //print("On audio read");
            if (_mumbleClient == null || !_mumbleClient.ConnectionSetupFinished)
                return;

            _mumbleClient.LoadArrayWithVoiceData(_session, data, 0, data.Length);

            //Debug.Log("playing audio with avg: " + data.Average() + " and max " + data.Max());
            if (Gain == 1)
                return;

            for (int i = 0; i < data.Length; i++)
                data[i] = Mathf.Clamp(data[i] * Gain, -1f, 1f);
            //Debug.Log("playing audio with avg: " + data.Average() + " and max " + data.Max());
        }
        void OnAudioPositionSet(int position)
        {
            //print("Position set " + position);
            _position = position;
        }
    }
}
