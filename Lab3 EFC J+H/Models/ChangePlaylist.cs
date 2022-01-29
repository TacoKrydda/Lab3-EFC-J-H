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
                    Console.WriteLine("Id is already taken or invalid data");
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
        public void AddNewPlaylist(int playlistId, string playlistName)
        {
            using (var context = new everyloopContext())
            {
                context.Playlists.Add(new Playlist() { PlaylistId = playlistId, Name = playlistName });
                context.SaveChanges();
            }
        }
        public void RunAddNewPlaylist()
        {
            var newPlayListId = newPlaylistId();
            if (newPlayListId != -1)
            {
                var newPlayListName = newPlaylistName();
                AddNewPlaylist(newPlayListId, newPlayListName);
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
        public void RunAddTrackToPlaylist()
        {
            var playlistId = SearchPlaylistId();
            var trackId = SearchTrackId();
            if (playlistId != -1)
            {
                if (trackId != -1)
                {
                    AddTrackToPlaylist(playlistId, playlistId);
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
        public void RemovePlaylist(int id)
        {
            using (var context = new everyloopContext())
            {
                var trackInPlaylist = context.PlaylistTracks.FirstOrDefault(x => x.PlaylistId == id);
                if (trackInPlaylist != null)
                {
                    context.PlaylistTracks.Remove(trackInPlaylist);
                }
                else
                {
                    Console.WriteLine("Couldn't find playlist id");
                }

                var playlist = context.Playlists.Where(p => p.PlaylistId == id).FirstOrDefault();
                if (playlist != null)
                {
                    context.Playlists.Remove(playlist);context.SaveChanges();
                }
                context.SaveChanges();
            }
        }

        public void RunRemovePlaylist()
        {
            var plId = PlaylistIdRemove();
            RemovePlaylist(plId);
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
            }
        }
        public void RunRemoveTrackFromPlaylist()
        {
            var playlistId = SearchPlaylistId();
            if (playlistId != -1)
            {
                var trackId = SearchTrackId();
                if (trackId != -1)
                {
                    RemoveTrackFromPlaylist(playlistId, trackId);
                }
                Console.WriteLine("Something wrong happened");
            }
            else if (playlistId == -1)
            {
                Console.WriteLine("Something wrong happened");
            }
            
        }
        #endregion

        public static int SearchPlaylistId()
        {
            Console.WriteLine("Enter playlist id");
            bool plIdIsValid = int.TryParse(Console.ReadLine(), out int plId);
            if (plIdIsValid &&  plId >= 0)
            {
                return plId;
            }
            else
            {
                return -1;
            }
        }
        public static int SearchTrackId()
        {
            Console.WriteLine("Enter track id");
            bool trackIdIsValid = int.TryParse(Console.ReadLine(), out int trackId);
            if (trackIdIsValid && trackId >= 0)
            {
                return trackId;
            }
            else
            {
                return -1;
            }
        }

        //// Method used to return 2 value (playlist id, track id)
        //public (int, int) plIdAndTrackId()
        //{
        //    Console.WriteLine("Enter playlist id");
        //    bool plIdIsValid = int.TryParse(Console.ReadLine(), out int plId);
        //    Console.WriteLine("Enter track id");
        //    bool trackIdIsValid = int.TryParse(Console.ReadLine(), out int trackId);
        //    if (plIdIsValid && trackIdIsValid && plId > 0 || trackId > 0)
        //    {
        //        return (plId, trackId);
        //    }
        //    else
        //    {
        //        return (-1, -1);
        //    }
        //}
    }
}
