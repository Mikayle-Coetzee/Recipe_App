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
        /// <summary>
        /// An instance of the MediaPlayer class, which is used to play audio files.
        /// </summary>
        private MediaPlayer mediaPlayer;

        /// <summary>
        /// A list of strings representing the names of the songs available in the 'Songs' folder. It will 
        /// be populated with the names of the available songs when the LoadSongs method is called.
        /// </summary>
        private List<string> songs;

        /// <summary>
        /// Holds the index of the currently playing song in the songs list.
        /// </summary>
        private int currentSongIndex = -1;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for MusicPlayerView.
        /// </summary>
        public MusicPlayerView()
        {
            InitializeComponent();
            mediaPlayer = new MediaPlayer();
            LoadSongs();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method loads the songs from the 'Songs' folder and populates the song list.
        /// </summary>
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

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method handles the mouse left button up event on a song item in the song list.
        /// Updates the current song index and applies bold styling to the selected song.
        /// </summary>
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

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method will play the next song in the music player.
        /// </summary>
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            PlayNextSong();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method plays the current song in the music player.
        /// </summary>
        private void PlayCurrentSong()
        {
            string currentSong = songs[currentSongIndex];

            // Extract artist and song title from the file name
            string[] songParts = currentSong.Split('_');
            string artist = songParts[0];
            string songTitle = songParts[1];

            // Get the path to the song file 
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

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method plays the next song in the music player.
        /// </summary>
        private void PlayNextSong()
        {
            currentSongIndex++;
            if (currentSongIndex >= songs.Count)
                currentSongIndex = 0;

            PlayCurrentSong();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method plays the previous song in the music player.
        /// </summary>
        private void PlayPreviousSong()
        {
            currentSongIndex--;
            if (currentSongIndex < 0)
                currentSongIndex = songs.Count - 1;

            PlayCurrentSong();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method plays the next song.
        /// </summary>
        private void btnNext_Click_1(object sender, RoutedEventArgs e)
        {
            PlayNextSong();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method plays the current song.
        /// </summary>
        private void btnPlay_Click_1(object sender, RoutedEventArgs e)
        {
            PlayCurrentSong();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method stops the currently playing song.
        /// </summary>
        private void btnPause_Click_1(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method plays the previous song.
        /// </summary>
        private void btnPrevious_Click_1(object sender, RoutedEventArgs e)
        {
            PlayPreviousSong();
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
