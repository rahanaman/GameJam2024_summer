using GoogleSheet.Type;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Hamster.ZG.Type
{
    [Type(typeof(List<List<int>>), new string[] {"int_2DimArr", "int_2DimArr" })]
    public class ListListIntType : IType
    {
        public object DefaultValue => null;

        public object Read(string value)
        {
            List<List<int>> result = new List<List<int>>();
            var values = value.Split(':');
            for (int i = 0; i < values.Length; ++i) {
                List<int> data = new List<int>();
                var current = ReadUtil.GetBracketValueToArray(values[i]);
                for (int j = 0; j < current.Length; ++j) {
                    if (int.TryParse(current[j], out int datum)) {
                        data.Add(datum);
                    }
                }
                result.Add(data);
            }
            return result;
        }    

        public string Write(object value)
        {
            StringBuilder sb = new StringBuilder();
            List<List<int>> list = (List<List<int>>)value;
            for(int i = 0; i < list.Count; ++i) {
                sb.Append('[');
                for(int j = 0; j < list[i].Count; ++j) {
                    sb.Append(j);
                    if(j < list[i].Count - 1) {
                        sb.Append(',');
                    }
                }
                sb.Append(']');
                if(i < list.Count - 1) {
                    sb.Append(':');
                }
            }
            return sb.ToString();
        }
    }
}
