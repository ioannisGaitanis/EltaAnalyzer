using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CWebApplicationContext
    {
        public string ConnectionString { get; set; }

        public CWebApplicationContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<CSearch> GetAllAlbums()
        {
            List<CSearch> rootSearches = new List<CSearch>();
            List<CSearch> notRootSearches = new List<CSearch>();
            List<CSearch> helpNotRoot = new List<CSearch>();
            List<CSearch> allSearches = new List<CSearch>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from searches where PREVIOUS_PERFORMED IS NULL", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CSearchStatistics ss = new CSearchStatistics
                        {
                            TotalPackages = reader.GetInt32("TOTAL_PACKAGES"),
                            DeliveredPackages = reader.GetInt32("DELIVERED_PACKAGES"),
                            ReturnedPackages = reader.GetInt32("RETURNED_PACKAGES"),  
                            CanceledPackages = reader.GetInt32("CANCELLED_PACKAGES"),
                            LostPackages = reader.GetInt32("LOST_PACKAGES") 
                        };
                        
                        CSearch newSeacrh = new CSearch
                        {
                            TimeStamp = reader.GetDateTime("TIME_STAMP"),
                            PackagesInfo = ss,
                            Description = reader.GetString("DESCRIPTION"),
                            ChildSearch = new CSearch { TimeStamp = reader.GetDateTime("NEXT_PERFORMED") }
                        };
                        
                        rootSearches.Add(newSeacrh);
                        
                    }
                }

                foreach(CSearch search in rootSearches)
                {
                    AddChildNode(search);
                }

                
            }
            
            return rootSearches;
        }

        private void AddChildNode(CSearch search)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            if (search.ChildSearch != null)
            {
                MySqlCommand cmd = new MySqlCommand("select * from searches where TIME_STAMP=@tm", conn);
                cmd.Parameters.AddWithValue("@tm", search.ChildSearch.TimeStamp);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        CSearchStatistics ss = new CSearchStatistics
                        {
                            TotalPackages = reader.GetInt32("TOTAL_PACKAGES"),
                            DeliveredPackages = reader.GetInt32("DELIVERED_PACKAGES"),
                            ReturnedPackages = reader.GetInt32("RETURNED_PACKAGES"),
                            CanceledPackages = reader.GetInt32("CANCELLED_PACKAGES"),
                            LostPackages = reader.GetInt32("LOST_PACKAGES")
                        };
                        
                        if(!reader.IsDBNull(2))
                        {
                            CSearch newSeacrh = new CSearch
                            {
                                TimeStamp = reader.GetDateTime("TIME_STAMP"),
                                PackagesInfo = ss,
                                Description = reader.GetString("DESCRIPTION"),
                                ChildSearch = new CSearch { TimeStamp = reader.GetDateTime("NEXT_PERFORMED") }
                            };
                            search.ChildSearch = newSeacrh;
                            Console.Write("dddddddd   ");
                            Console.WriteLine(search.ChildSearch.PackagesInfo.PendingPackages);
                            AddChildNode(newSeacrh);
                        }
                        else
                        {
                            CSearch newSeacrh = new CSearch
                            {
                                TimeStamp = reader.GetDateTime("TIME_STAMP"),
                                PackagesInfo = ss,
                                Description = reader.GetString("DESCRIPTION"),
                                ChildSearch = null 
                            };
                            search.ChildSearch = newSeacrh;
                        }
                        

                        
                        
                    }
                    
                }
                
            }
            else return;
        }
    }
}
