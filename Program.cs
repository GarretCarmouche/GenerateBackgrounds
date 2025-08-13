class Program
{
    private static string watchDirectory = "/watchDirectory";
    public static void Main(string[] args)
    {
        int checkDelay = int.Parse("" + Environment.GetEnvironmentVariable("CHECKFREQUENCY")) * 1000;
        while (true) {
            Thread.Sleep(checkDelay);
            try
            {
                RunCheck();
            }catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
            }
        }
    }

    private static void RunCheck()
    {
        Console.WriteLine("Running scan");
        foreach(string source in Directory.GetDirectories(watchDirectory))
        {
            Console.WriteLine("Source: " + source);
            foreach(string video in Directory.GetDirectories(source))
            {
                Console.WriteLine("Video: " + video);

                if (File.Exists(video + "/background.jpg"))
                    continue;
                
                foreach(string file in Directory.GetFiles(video))
                {
                    Console.WriteLine("File: " + file);
                    if (file.EndsWith(".jpg"))
                    {
                        File.Copy(file, file.Substring(0, file.Length - Path.GetFileName(file).Length) + "/background.jpg", true);
                    }
                }
            }
        }
    }
}