using System;
using System.Collections.Generic;
using System.IO;

namespace AnalyzeMusicPlaylist
{
    class Loader
    {
        public static List<musicStruct> LoadMusicList(string inFile)
        {
            List<musicStruct> musicList = new List<musicStruct>();

            using(StreamReader file = new StreamReader(inFile)){
                string line = file.ReadLine();
                int lineNumber = 1;
                int expectedColumnCount = 8;

                while((line = file.ReadLine()) != null){
                    string[] lineArray = line.Split("\t");
                    if(lineArray.Length < expectedColumnCount) {
                        throw new InvalidDataException($"Row {lineNumber} contains {lineArray.Length} values. It should contain {expectedColumnCount}.");
                    }
                    try {
                        string name = lineArray[0];
                        string artist = lineArray[1];
                        string album = lineArray[2];
                        string genre = lineArray[3]; 
                        var size = Int64.Parse(lineArray[4]);
                        var time = Int32.Parse(lineArray[5]);
                        var year = Int32.Parse(lineArray[6]);
                        var plays = Int32.Parse(lineArray[7]);
                        musicStruct newListItem = new musicStruct(name, artist, album, genre, size, time, year, plays);
                        musicList.Add(newListItem);
                    }
                    catch(Exception ex) {
                        throw new FormatException($"Row { lineNumber} threw exception { ex.Message } and has been skipped.");
                    }

                    lineNumber++;
                }
            }

            return musicList;
        }
    }
}