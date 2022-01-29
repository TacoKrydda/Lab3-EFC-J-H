using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab3_EFC_J_H.Data;

namespace Lab3_EFC_J_H.Models
{
    public class SearchMethods
    {
        #region SearchArtists
        public string searchArtistsText()
        {
            Console.WriteLine("Enter the artists first letter(s)");
            var userInput = Console.ReadLine();
            return userInput;
        }
        public void readFromDbForArtists(string searchWord)
        {
            using (var context = new everyloopContext())
            {
                var group = context.Artists
                .Where(p => p.Name.StartsWith(searchWord)).ToList();

                foreach (var artist in group)
                {
                    Console.WriteLine($"Artist id: {artist.ArtistId} = Name: {artist.Name}");
                }
            }
        }
        public void RunSearchArtists()
        {
            var searchResult = searchArtistsText();
            readFromDbForArtists(searchResult);
        }
        #endregion

        #region SearchPlaylist
        public string searchPlaylistText()
        {
            Console.WriteLine("Enter the playlist first letter(s)");
            var userInput = Console.ReadLine();
            return userInput;
        }
        public void readFromDbForPlaylist(string searchWord)
        {
            using (var context = new everyloopContext())
            {
                var group = context.Playlists
                .Where(p => p.Name.StartsWith(searchWord)).ToList();

                foreach (var artist in group)
                {
                    Console.WriteLine($"Playlist id: {artist.PlaylistId} = Name: {artist.Name}");
                }
            }
        }
        public void RunSearchPlaylist()
        {
            var playlistResult = searchPlaylistText();
            readFromDbForPlaylist(playlistResult);
        }
        #endregion
    }
}
