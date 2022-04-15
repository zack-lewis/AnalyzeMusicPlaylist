using System;

namespace AnalyzeMusicPlaylist
{
    public struct musicStruct
    {
        //Name Artist Album Genre Size Time Year Plays
        private string _name;
        private string _artist;
        private string _album;
        private string _genre;
        private long _size;
        private int _time;
        private int _year;
        private int _plays;

        public string Name { get => _name; set => _name = value; }
        public string Artist { get => _artist; set => _artist = value; }
        public string Album { get => _album; set => _album = value; }
        public string Genre { get => _genre; set => _genre = value; }
        public long Size { get => _size; set{ if(value <= 0){throw new System.Exception("Size cannot be negative");} _size = value;} }
        public int Time { get => _time; set{ if(value <= 0){throw new System.Exception("Time cannot be negative");} _time = value;} }
        public int Year { get => _year; set{ if(value <= 0){throw new System.Exception("Year cannot be negative");} _year = value;} }
        public int Plays { get => _plays; set{ if(value <= 0){throw new System.Exception("Plays cannot be negative");} _plays = value;} }

        public musicStruct(string name, string artist, string album, string genre, long size, int time, int year, int plays) : this() {
            this.Name = name;
            this.Artist = artist;
            this.Album = album;
            this.Genre = genre;
            this.Size = size;
            this.Time = time;
            this.Year = year;
            this.Plays = plays;
        }

        override public string ToString(){
            return String.Format("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", Name, Artist, Album, Genre, Size, Time, Year, Plays);
        }
    }
}