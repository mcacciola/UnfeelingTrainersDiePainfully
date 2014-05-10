using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace UTDP
{
    class Music : System.Collections.ObjectModel.Collection<Song>
    {
        private int nextSong;
        private MediaState previousMediaState;
        private bool isPlaying = true;
        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        public Music()
        {
            // Setup music player
            MediaPlayer.IsRepeating = false;
            MediaPlayer.IsShuffled = false;
            MediaPlayer.Volume = .25f;

            previousMediaState = MediaState.Playing;
            nextSong = 0;
        }


        public void Play()
        {
            if ((MediaPlayer.State == MediaState.Stopped) && (previousMediaState == MediaState.Playing))
            {

                Song currentSong = this.Items[nextSong];
                MediaPlayer.Play(currentSong);
                nextSong++;

                if (nextSong > this.Count - 1)
                {
                    nextSong = 0;
                }

            }
            previousMediaState = MediaPlayer.State;
        }

        public void AddSong(Song newSong)
        {
            this.Add(newSong);
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        public void Pause()
        {
            MediaPlayer.Pause();
            isPlaying = false;
        }

        public void Resume()
        {
            MediaPlayer.Resume();
            isPlaying = true;
        }
    }
}
