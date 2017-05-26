using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Messaging;
using System.Reflection;
using System.Text;
using log4net;
using Newtonsoft.Json;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Shared.Custom
{
    public static class Utility
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> fullBatch, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "chunkSize",
                    chunkSize,
                    "Chunk size cannot be less than or equal to zero.");
            }

            if (fullBatch == null)
            {
                throw new ArgumentNullException("fullBatch", "Input to be split cannot be null.");
            }

            var cellCounter = 0;
            var chunk = new List<T>(chunkSize);

            foreach (var element in fullBatch)
            {
                if (cellCounter++ == chunkSize)
                {
                    yield return chunk;
                    chunk = new List<T>(chunkSize);
                    cellCounter = 1;
                }

                chunk.Add(element);
            }

            yield return chunk;
        }

        public static Message CreateMessage(object message)
        {
            Message msmqMessage;

            try
            {
                msmqMessage = new Message();
                msmqMessage.Label = Utility.GetMessageType(message.GetType());
                var jsonBody = JsonConvert.SerializeObject(message);
                msmqMessage.BodyStream = new MemoryStream(Encoding.Default.GetBytes(jsonBody));
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                throw;
            }

            return msmqMessage;
        }
        public static string GetMessageType(Type type)
        {
            return $"{type.FullName}, {type.Assembly.GetName().Name}";
        }

        public static object Get(this IDataRecord row, string fieldName, Type type)
        {
            try
            {
                int ordinal = row.GetOrdinal(fieldName);
                var value = row.IsDBNull(ordinal) ? GetDefault(type) : row.GetValue(ordinal);
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (null == value)
                        return null;
                }
                return Convert.ChangeType(value, Nullable.GetUnderlyingType(type) ?? type);
            }
            catch (Exception ex)
            {
                if (ex is IndexOutOfRangeException)
                {
                    //Log.Debug($"Column NOT found:{fieldName}");
                }
                else
                {
                    Log.Debug(ex);                    
                }
            }
            return GetDefault(type);
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static T Get<T>(this IDataRecord row, string fieldName) 
        {
            int ordinal = row.GetOrdinal(fieldName);
            return row.Get<T>(ordinal);
        }

        public static T Get<T>(this IDataRecord row, int ordinal)
        {
            var value = row.IsDBNull(ordinal) ? default(T) : row.GetValue(ordinal);
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public static string GetColumns(List<string> columnList)
        {
            return  string.Join(",", columnList.ToArray());
        }
        public static string GetColumns(List<string> columnList,string alias)
        {
            return $"{alias}.{string.Join($",{alias}.", columnList.ToArray())}";
        }
        public static string GetParameters(List<string> columnList)
        {
            return $"@{string.Join(",@", columnList.ToArray())}";
        }
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (prop.PropertyType.Name.Contains("Nullable"))
                    tb.Columns.Add(prop.Name,typeof(string));
                else
                    tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static string GetFolderPath(string folder)
        {
            return folder.EndsWith("\\") ? folder : $"{folder}\\";
        }

        public static string HasToEndsWith(this string value, string end)
        {
            return value.EndsWith(end) ? value : $"{value}{end}";
        }

        public static string ReplaceFromEnd(this string s, string suffix,string replaceWith, int number = 1)
        {
            var finalString = s;

            if (s.EndsWith(suffix))
            {
                finalString= s.Substring(0, s.Length - number);
                finalString = $@"{finalString}.zip";
            }
            
            return finalString;
        }

        public static void Report(this IProgress<DProgress> progress,string status, decimal? count = null, decimal? total = null)
        {
            var dp = DProgress.Report(status);

            if (count.HasValue && total.HasValue)
            {
                decimal percentage = decimal.Divide(count.Value, total.Value) * 100;
            }

            progress?.Report(dp);
        }

        public static string GetErrorMessage(Exception ex)
        {
            if (null == ex.InnerException)
            {
                return ex.Message;
            }
            return ex.InnerException.Message;
        }
    }
}

