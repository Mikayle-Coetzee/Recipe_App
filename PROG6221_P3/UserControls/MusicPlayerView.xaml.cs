#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 3
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PROG6221_P3.UserControls
{
    /// <summary>
    /// Interaction logic for MusicPlayerView.xaml
    /// </summary>
    public partial class MusicPlayerView : UserControl
    {
        private MediaPlayer mediaPlayer;
        private List<string> songs;
        private int currentSongIndex = -1;

        public MusicPlayerView()
        {
            InitializeComponent();
            mediaPlayer = new MediaPlayer();
            LoadSongs();
        }

        private void LoadSongs()
        {
            string songsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Songs");

            if (Directory.Exists(songsFolder))
            {
                songs = Directory.GetFiles(songsFolder, "*.mp3")
                    .Select(Path.GetFileNameWithoutExtension)
                    .ToList();

                foreach (var song in songs)
                {
                    TextBlock songItem = new TextBlock
                    {
                        Text = song,
                        Margin = new Thickness(0, 5, 0, 5),
                        Cursor = Cursors.Hand
                    };

                    songItem.MouseLeftButtonUp += SongItem_MouseLeftButtonUp;
                    songList.Children.Add(songItem);
                }

                if (songs.Count > 0)
                    currentSongIndex = 0; // Set currentSongIndex to 0 if songs list is not empty
            }
        }


        private void SongItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                int clickedSongIndex = songList.Children.IndexOf(textBlock);

                if (clickedSongIndex != currentSongIndex)
                {
                    if (currentSongIndex >= 0 && currentSongIndex < songList.Children.Count)
                    {
                        var previousSongItem = (TextBlock)songList.Children[currentSongIndex];
                        previousSongItem.FontWeight = FontWeights.Normal;
                    }

                    textBlock.FontWeight = FontWeights.Bold;
                    currentSongIndex = clickedSongIndex;
                }
            }
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            PlayNextSong();
        }


        private void PlayCurrentSong()
        {
            string currentSong = songs[currentSongIndex];

            // Extract artist and song title from the file name
            string[] songParts = currentSong.Split('_');
            string artist = songParts[0];
            string songTitle = songParts[1];

            // Get the path to the song file (assuming it's in the "Songs" folder)
            string songPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Songs", $"{currentSong}.mp3");

            if (File.Exists(songPath))
            {
                // Update the song title and artist text
                txtSongTitle1.Text = songTitle;
                txtArtits1.Text = artist;

                // Load and play the song
                mediaPlayer.Open(new Uri(songPath));
                mediaPlayer.Play();
            }
            else
            {
                MessageBox.Show("The selected song file is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void PlayNextSong()
        {
            currentSongIndex++;
            if (currentSongIndex >= songs.Count)
                currentSongIndex = 0;

            PlayCurrentSong();
        }

        private void PlayPreviousSong()
        {
            currentSongIndex--;
            if (currentSongIndex < 0)
                currentSongIndex = songs.Count - 1;

            PlayCurrentSong();
        }

        private void btnNext_Click_1(object sender, RoutedEventArgs e)
        {
            PlayNextSong();
        }

        private void btnPlay_Click_1(object sender, RoutedEventArgs e)
        {
            PlayCurrentSong();
        }

        private void btnPause_Click_1(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void btnPrevious_Click_1(object sender, RoutedEventArgs e)
        {
            PlayPreviousSong();
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
