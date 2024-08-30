using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        IFileReader fileReader;
        IVideoRepository videoRepository;

        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
        {
            this.fileReader = fileReader ?? new FileReader();
            this.videoRepository = videoRepository ?? new VideoRepository();
        }

        public string ReadVideoTitle()
        {
            string str = fileReader.Read("video.txt");
            Video video = JsonConvert.DeserializeObject<Video>(str);

            if (video == null)
            {
                return "Error parsing the video.";
            }

            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            List<int> videoIds = new List<int>();

            foreach (Video v in this.videoRepository.GetUnprocessedVideos())
            {
                videoIds.Add(v.Id);
            }

            return String.Join(",", videoIds);        
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}