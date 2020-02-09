using System;

namespace DOFactory.GangofFour.Adapter.RealWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            AudioPlayer audioPlayer = new AudioPlayer();
            audioPlayer.play("mp3", "hello.mp3");
            audioPlayer.play("mp4", "alone.mp4");
            audioPlayer.play("vlc", "far far away.vlc");
            audioPlayer.play("avi", "mind me.avi");

        }
        
        // media player interface
        public interface MediaPlayer {
            public void play(string audioType, string fileName);
        }

        // advance media player interface
        public interface AdvanceMediaPlayer {
            public void playVlc(string fileName);
            public void playMp4(string fileName);
        }

        // vlc player - advance media player implementation
        public class VlcPlayer : AdvanceMediaPlayer
        {
            public void playVlc(string fileName) {
                Console.WriteLine("Playing vlc file. Name: " + fileName);
            }
            public void playMp4(string fileName) { 
                // do nothing
            }
        }

        // mp4 player - advance medai player implementation
        public class Mp4Player : AdvanceMediaPlayer {
            public void playVlc(string fileName) { 
                // do nothing
            }
            public void playMp4(string fileName) {
                Console.WriteLine("Playing mp4 file. Name: " + fileName);
            }
        }

        // media adapter to allow audio player to play advance media formats
        public class MediaAdapter : MediaPlayer {
            AdvanceMediaPlayer advanceMediaPlayer;
            public MediaAdapter(string audioType)
            {
                if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase)) {
                    advanceMediaPlayer = new VlcPlayer();
                }
                else if(audioType.Equals("mp4",StringComparison.OrdinalIgnoreCase)){
                    advanceMediaPlayer = new Mp4Player();
                }
            }
            public void play(string audioType, string fileName) {
                if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase)) {
                    advanceMediaPlayer.playVlc(fileName);
                } else if (audioType.Equals("mp4",StringComparison.OrdinalIgnoreCase)) {
                    advanceMediaPlayer.playMp4(fileName);
                }
            }
        }
        //
        public class AudioPlayer : MediaPlayer {
            MediaPlayer mediaAdapter;

            public void play(string audioType, string fileName){
                // inbuilt support to play mp3 music files
                if (audioType.Equals("mp3", StringComparison.OrdinalIgnoreCase)) {
                    Console.WriteLine("Playing mp3 file. Name: " + fileName);
                }
                // media adapter is providing support to play other file formats
                else if (audioType.Equals("vlc",StringComparison.OrdinalIgnoreCase) ||
                        audioType.Equals("mp4",StringComparison.OrdinalIgnoreCase)) {
                    mediaAdapter = new MediaAdapter(audioType);
                    mediaAdapter.play(audioType, fileName);
                }
                else {
                    Console.WriteLine("Invalid media. " + audioType + " format not supported");
                }
            }
        }
    }
}
