using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnalyzeMusicPlaylist
{
    class Program
    {
        static void Main(string[] args)
        {
            List<musicStruct> musicList = new List<musicStruct>();
            
            if(args.Length != 2) {
                Console.WriteLine("Usage: AnalyzeMusicPlaylist <music_playlist_file_path> <report_file_path>");
                return;
            }

            string inFile = args[0];
            string outFile = args[1];
            try {
                musicList = Loader.LoadMusicList(inFile);
            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            generateReport(musicList, outFile);
        }

        static void generateReport(List<musicStruct> list, string reportFile) {
            StringBuilder output = new StringBuilder();
            string reportSeparator = "\n<==------------------==>\n";
            string listBullet = "\t=> ";

            output.AppendLine("Music Playlist Report");
            output.AppendLine(reportSeparator);

            // How many songs received 200 or more plays?
            int playsGT200 = (from item in list where item.Plays >= 200 select item).Count();
            output.AppendLine($"Number of songs with more than 200 plays: { playsGT200 }");
            output.AppendLine(reportSeparator);

            // How many songs are in the playlist with the Genre of “Alternative”?
            int genreAlternativeCount = (from item in list where item.Genre == "Alternative" select item).Count();
            output.AppendLine($"Number of songs in the genre 'Alternative': { genreAlternativeCount }");
            output.AppendLine(reportSeparator);

            // How many songs are in the playlist with the Genre of “Hip-Hop/Rap”?
            int genreHipHopRapCount = (from item in list where item.Genre == "Hip-Hop/Rap" select item).Count();
            output.AppendLine($"Number of songs in the genre 'Hip-Hop/Rap': { genreHipHopRapCount }");
            output.AppendLine(reportSeparator);

            // What songs are in the playlist from the album “Welcome to the Fishbowl?”
            List<musicStruct> albumWelcomeToTheFishbowl = (from item in list where item.Album == "Welcome to the Fishbowl" select item).ToList<musicStruct>();
            output.AppendLine("Songs in album 'Welcome to the Fishbowl':");
            foreach(musicStruct song in albumWelcomeToTheFishbowl){
                output.Append(listBullet);
                output.AppendLine(song.ToString());
            }
            output.AppendLine(reportSeparator);
            
            // What are the songs in the playlist from before 1970?
            var songsBefore1970 = from item in list where item.Year < 1970 select item;
            output.AppendLine("Songs before 1970:");
            foreach(var song in songsBefore1970){
                output.Append(listBullet);
                output.AppendLine(song.ToString());
            }
            output.AppendLine(reportSeparator);
            
            // What are the song names that are more than 85 characters long?
            var songNamesGT85Char = from item in list where item.Name.Length > 85 select item;
            output.AppendLine("Song names greater than 85 characters: ");
            foreach(var song in songNamesGT85Char) {
                output.Append(listBullet);
                output.AppendLine(song.ToString());
            }
            output.AppendLine(reportSeparator);
            
            // What is the longest song? (longest in Time)
            var longestSong = (from item in list orderby item.Time descending select item).FirstOrDefault();
            output.AppendLine($"Longest Song: ");
            output.Append(listBullet);
            output.AppendLine($"{ longestSong.ToString() }");
            output.AppendLine(reportSeparator);
            
            // What are the unique Genres in the playlist?
            var uniqueGenreList = (from item in list group item by item.Genre into GenreList select GenreList);
            output.AppendLine("Unique Genres: ");
            foreach(var g in uniqueGenreList) {
                output.Append(listBullet);
                output.AppendLine(g.Key);
            }
            output.AppendLine(reportSeparator);
            
            // How many songs were produced each year in the playlist?
            var songProducedByYear = from item in list group item by item.Year into songsByYear orderby songsByYear.Key descending select songsByYear;
            output.AppendLine("Number of songs produced each year: ");
            foreach(var song in songProducedByYear) {
                output.Append(listBullet);
                output.AppendLine(song.Key + ": " + song.Count());
            }
            output.AppendLine(reportSeparator);

            // What are the total plays per year  in the playlist?
            var playsPerYear = (from item in list group item by item.Year into yearlyPlays orderby yearlyPlays.Key descending select new {yearlyPlays.Key, Plays = yearlyPlays.Sum(x => x.Plays)});
            output.AppendLine("Total Plays per Year: ");
            foreach(var y in playsPerYear) {
                output.Append(listBullet);
                output.AppendLine($"{ y.Key }: { y.Plays }");
            }
            output.AppendLine(reportSeparator);

            // Print a list of the unique artists in the playlist.
            var uniqueArtists = from item in list group item by item.Artist into artistList orderby artistList.Key select artistList;
            output.AppendLine("Unique list of Artists: ");
            foreach(var a in uniqueArtists) {
                output.Append(listBullet);
                output.AppendLine($"{ a.Key }");
            }

            output.AppendLine("\n/////////////---- NOTHING FOLLOWS ----/////////////");

            try {
                using(StreamWriter fileStream = new StreamWriter(reportFile)) {
                    fileStream.Write(output);
                }
            }
            catch(Exception ex) {
                Console.WriteLine($"{ ex.Message }");
            }

        }
    }
}
