namespace gym_management_system
{
    using System;
    using System.IO;

    public static class DotEnv
    {
        // load environment variables from file given by filePath
        public static void Load(string filePath)
        {
            // if file does not exist, return 
            if (!File.Exists(filePath))
                return;

            // set environment variable by splitting the contents of the file into key value pairs separated by '=
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}