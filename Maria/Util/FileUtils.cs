using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Maria.Util {
    public class FileUtils {
        public static string GetWWWPersistentDataPath(string target) {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            string url = "file:///" + UnityEngine.Application.persistentDataPath + target;
#elif UNITY_IOS
            url = UnityEngine.Application.persistentDataPath + target
#elif UNITY_ANDROID
            url = "jar:file:///" + Application.persistentDataPath + target;
#endif
            return url;
        }

        public static string GetStreamingPath(string target) {
            return UnityEngine.Application.streamingAssetsPath + target;
        }

        public static string GetStringFromFileInStreaming(string filename) {
            string path = UnityEngine.Application.streamingAssetsPath + "/" + filename;
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fs);
            string res = reader.ReadToEnd();
            reader.Close();
            fs.Close();
            return res;
        }

        public static void GetStringFromFileInStreaming(string filename, Action<string> callback) {
            string path = UnityEngine.Application.streamingAssetsPath + "/" + filename;
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fs);
            string res = reader.ReadToEnd();
            callback(res);
            reader.Close();
            fs.Close();
        }

        public static byte[] GetBytesFromFileInStreaming(string filename) {
            string path = UnityEngine.Application.streamingAssetsPath + "/" + filename;
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fs);
            string content = reader.ReadToEnd();
            reader.Close();
            fs.Close();
            return Encoding.UTF8.GetBytes(content);
        }

        public static bool WriteStringToFileInStreaming(string data, string filename) {
            try {
                string path = UnityEngine.Application.streamingAssetsPath + "/" + filename;
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter writer = new StreamWriter(fs);
                writer.Write(data);
                writer.Close();
                fs.Close();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public static bool WriteBytesToFileInStreaming(byte[] data, string filename) {
            try {
                string path = UnityEngine.Application.streamingAssetsPath + "/" + filename;
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter writer = new StreamWriter(fs);
                string content = Encoding.UTF8.GetString(data);
                writer.Write(content);
                writer.Flush();
                writer.Close();
                fs.Close();
                return true;
            } catch (Exception) {
                return false;
            }
        }

    }
}
