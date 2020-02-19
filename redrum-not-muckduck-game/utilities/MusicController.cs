using NAudio.Wave;
using System.Threading;

namespace redrum_not_muckduck_game
{
    public static class MusicController
    {
        internal static AudioFileReader audioFile;
        internal static WaveOutEvent outputDevice;

        // Takes in an audio file & controls the wait time to start
        internal static void PlaySound(string musicFile, int milliSeconds = 0)
        {
            audioFile = new AudioFileReader(musicFile);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
            Thread.Sleep(milliSeconds);
        }
        public static void StopMusic()
        {
            outputDevice.Dispose();
        }
    }
}
