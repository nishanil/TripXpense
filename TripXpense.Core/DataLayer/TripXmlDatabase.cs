using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TripXpense.Core.Model;

namespace TripXpense.Core.DataLayer
{
    static class TripXmlDatabase
    {
        private static readonly string _dbPath;
        
        /// <summary>
        /// Gets the TripDB.xml path based on the platform
        /// </summary>
        internal static string DatabaseFilePath
        {
            get
            {
                const string storeFilename = "TripDB.xml";
#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
				var path = storeFilename;
#else

#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
#else
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "../Library/"); // Library folder
#endif
                var path = Path.Combine(libraryPath, storeFilename);
#endif
                return path;
            }
        }

        static TripXmlDatabase()
        {
            _dbPath = DatabaseFilePath;
        }

        /// <summary>
        /// Reads Trips from xml
        /// </summary>
        internal static List<Trip> ReadXml()
        {
            var trips = new List<Trip>();
            if (File.Exists(_dbPath))
            {
                var serializer = new XmlSerializer(typeof(List<Trip>));
                using (var stream = new FileStream(_dbPath, FileMode.Open))
                {
                    trips = (List<Trip>)serializer.Deserialize(stream);
                }
            }
            return trips;
        }

        /// <summary>
        /// Writes Trips to xml in the path provided
        /// </summary>
        internal static void WriteXml(List<Trip> trips)
        {
            var serializer = new XmlSerializer(typeof(List<Trip>));
            using (var writer = new StreamWriter(_dbPath))
            {
                serializer.Serialize(writer, trips);
            }
        }
    }
}
