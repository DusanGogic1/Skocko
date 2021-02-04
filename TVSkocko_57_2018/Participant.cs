using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVSkocko_57_2018
{
    class Participant
    {
        public string Username { get; set; }
        public int Time { get; set; }
        public int NumberOfMoves { get; set; }
    }
    [Serializable]
    class ForSerialization
    {
        public float time;
        public string Username;
        public int[,] dg1, dg2;
        public int[] combination;
        public int nom;
        public ForSerialization()
        {
            dg1 = new int[6, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            dg2 = new int[6, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            combination = new int[4];
            time = 0;
        }

    }

    class DataSerializer
    {
        public void BinarySerialize(ForSerialization data, string filePath)
        {
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath)) File.Delete(filePath);
            fileStream = File.Create(filePath);
            bf.Serialize(fileStream, data);
            fileStream.Close();
        }

        public ForSerialization BinaryDeserialize(string filePath)
        {
            ForSerialization obj = null;

            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if(File.Exists(filePath))
            {
                try { 
                    fileStream = File.OpenRead(filePath);
                    obj = (ForSerialization)bf.Deserialize(fileStream);
                    fileStream.Close();
                }
                catch(Exception e)
                {
                }
            }

            return obj;
        }
    }
}
