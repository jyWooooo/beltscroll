using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public static class CSVReader
{
    private readonly static string SPLIT_REGEX = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private readonly static string LINE_SPLIT_REGEX = @"\r\n|\n\r|\n|\r";
    private readonly static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(TextAsset csvText)
    {
        var list = new List<Dictionary<string, object>>();
        string[] lines;

        string source;
        using (StreamReader sr = new(csvText.text))
            source = sr.ReadToEnd();
        lines = Regex.Split(source, LINE_SPLIT_REGEX);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_REGEX);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_REGEX);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                value = value.Replace("<br>", "\n");
                value = value.Replace("<c>", ",");

                object finalvalue = value;
                if (int.TryParse(value, out int n))
                    finalvalue = n;
                else if (float.TryParse(value, out float f))
                    finalvalue = f;
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}