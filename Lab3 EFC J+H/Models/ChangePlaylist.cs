using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab3_EFC_J_H.Data;

namespace Lab3_EFC_J_H.Models
{
    public class ChangePlaylist
    {
        #region Menu
        public int textMenu()
        {
            Console.WriteLine("1. New playlist\n2. Add track to playlist\n3. Remove playlist\n" +
                "4. Remove track from playlist\n5. Search artists\n6. close program");
            bool isValid = int.TryParse(Console.ReadLine(), out int userInput);
            if (isValid)
            {
                return userInput;
            }
            else
            {
                return -1;
            }
        }
        public void menu()
        {
            var loop = true;
            while (loop)
            {
                Console.Clear();
                var userInput = textMenu();
                switch (userInput)
                {
                    case -1:
                        Console.WriteLine("Invalid data");
                        break;
                    case 1:
                        var newPlayListId = newPlaylistId();
                        var newPlayListName = newPlaylistName();
                        NewPlaylist(newPlayListId, newPlayListName);
                        break;
                    case 2:
                        var addResult = plIdAndTrackId();
                        AddTrackToPlaylist(addResult.Item1, addResult.Item2);
                        break;
                    case 3:
                        var plId = PlaylistIdRemove();
                        RemovePlaylst(plId);
                        break;
                    case 4:
                        var removeResult = plIdAndTrackId();
                        RemoveTrackFromPlaylist(removeResult.Item1, removeResult.Item2);
                        break;
                    case 5:
                        var searchResult = sortBy();
                        ReadFromDb(searchResult);
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
        public void returnToMenu()
        {
            menu();
        }
        #endregion

        #region 1. New playlist
        public int newPlaylistId()
        {
            Console.WriteLine("Enter the new playlist id");
            bool isValid = int.TryParse(Console.ReadLine(), out int id);
            using (var context = new everyloopContext())
            {
                var listOfIds = context.Playlists.Select(x => x.PlaylistId);
                if (!listOfIds.Contains(id) && isValid /*== true*/)
                {
                    return id;
                }
                else
                {
                    Console.WriteLine("Id is already taken or invalid data\nPress any key to continue");
                    Console.ReadKey();
                    menu();
                    return -1;
                }
            }
        }
        public string newPlaylistName()
        {
            Console.WriteLine("Enter the new playlist name");
            var playlistName = Console.ReadLine();
            return playlistName.ToString();
        }
        public void NewPlaylist(int playlistId, string playlistName)
        {
            using (var context = new everyloopContext())
            {
                //if (playlistId == -1)
                //{
                //    Console.WriteLine("Id is already taken or invalid data");
                //    Console.WriteLine("Press any key to continue");
                //    Console.ReadKey();
                //    menu();
                //}
                //else
                //{
                context.Playlists.Add(new Playlist() { PlaylistId = playlistId, Name = playlistName });
                context.SaveChanges();
                //}

            }

        }
        #endregion

        #region 2. Add track to playlist
        public void AddTrackToPlaylist(int plID, int trackID)
        {
            using (var context = new everyloopContext())
            {
                //var ltId = context.Playlists.Select(x => x.PlaylistId);
                //var tkId = context.Tracks.Select(x => x.TrackId);
                if (plID != -1 || trackID != -1 /*&& ltId.Contains(plID) && tkId.Contains(trackID)*/)
                {
                    context.PlaylistTracks.Add(new PlaylistTrack() { PlaylistId = plID, TrackId = trackID });
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Something wrong happened");
                }
            }
        }
        #endregion

        #region 3. Remove playlist
        public int PlaylistIdRemove()
        {
            Console.WriteLine("Enter the playlist id to remove");
            _ = int.TryParse(Console.ReadLine(), out int id);
            return id;
        }
        public void RemovePlaylst(int id)
        {
            using (var context = new everyloopContext())
            {
                var trackInPlaylist = context.PlaylistTracks.FirstOrDefault(x => x.PlaylistId == id);
                if (trackInPlaylist != null)
                {
                    context.PlaylistTracks.Remove(trackInPlaylist);
                }

                var playlist = context.Playlists.Where(p => p.PlaylistId == id).FirstOrDefault();
                if (playlist != null)
                {
                    context.Playlists.Remove(playlist);
                }
                context.SaveChanges();
            }
        }
        #endregion

        #region 4. Remove track from playlist
        public void RemoveTrackFromPlaylist(int plID, int trackID)
        {
            using (var context = new everyloopContext())
            {
                if (plID != -1 || trackID != -1)
                {
                    var track = (from t in context.PlaylistTracks
                                 where t.PlaylistId == plID &&
                                 t.TrackId == trackID
                                 select t).FirstOrDefault();

                    context.PlaylistTracks.Remove(track);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Something wrong happened");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    menu();
                }

            }

        }
        #endregion

        // Method used to return 2 value (playlist id, track id)
        public (int, int) plIdAndTrackId()
        {
            Console.WriteLine("Enter playlist id");
            bool plIdIsValid = int.TryParse(Console.ReadLine(), out int plId);
            Console.WriteLine("Enter track id");
            bool trackIdIsValid = int.TryParse(Console.ReadLine(), out int trackId);
            if (plIdIsValid && trackIdIsValid && plId > 0 || trackId > 0)
            {
                return (plId, trackId);
            }
            else
            {
                return (-1, -1);
            }
        }

        public string sortBy()
        {
            Console.WriteLine("Enter the artists first letter(s)");
            var userInput = Console.ReadLine();
            return userInput;
        }
        public void ReadFromDb(string searchWord)
        {
            using (var context = new everyloopContext())
            {
                var group = context.Artists
                .Where(p => p.Name.StartsWith(searchWord)).ToList();

                foreach (var artist in group)
                {
                    Console.WriteLine($"{artist.ArtistId} = {artist.Name}");
                }
            }
                
        }
    }
}
