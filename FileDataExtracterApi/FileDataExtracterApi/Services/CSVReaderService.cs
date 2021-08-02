using FileDataExtracterApi.Interfaces;
using FileDataExtracterApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileDataExtracterApi.Services
{
    public class CSVReaderService : ICSVReaderService<Artikel>
    {
        public async Task<List<Artikel>> ReadDataFromCSV(string filePath)
        {
            Artikel artikel;
            List<Artikel> artikels = new();
            string[] read;
            char[] seperators = { ',' };
            try
            {
                if (File.Exists(filePath))
                {
                    string data;
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        data = await sr.ReadLineAsync();
                        while ((data = sr.ReadLine()) != null)
                        {
                            read = data.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                            artikel = new Artikel
                            {
                                Key = read[0].ToString(),
                                ArtikelCode = read[1].ToString(),
                                ColorCode = read[2].ToString(),
                                Description = read[3].ToString(),
                                Price = float.Parse(read[4]),
                                DiscountPrice = float.Parse(read[5]),
                                DeliveredIn = read[6].ToString(),
                                Q1 = read[7].ToString(),
                                Size = int.Parse(read[8]),
                                Color = read[9].ToString()
                            };
                            artikels.Add(artikel);
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException();
                }
                
            }
            catch (Exception ex)
            {
                //log the excecption here
            }
            return artikels;
        }    
    }
}
