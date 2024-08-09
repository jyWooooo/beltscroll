using System;
using System.IO;
using UnityEngine;

public class UserGUIDManager
{
    private readonly string _path = $"{Application.persistentDataPath}/UserID.data";
    private Guid _userID;
    private bool _isReadYet = false;

    public Guid GetUserID()
    {
        if (!_isReadYet)
        {
            if (File.Exists(_path))
                _userID = ReadDataFile();
            else
            {
                _userID = Guid.NewGuid();
                CreateDataFile();
            }
            _isReadYet = true;
        }
        return _userID;
    }

    private void CreateDataFile()
    {
        using FileStream fs = new(_path, FileMode.Create, FileAccess.Write);
        {
            byte[] data = _userID.ToByteArray();
            fs.Write(data, 0, data.Length);
        }
    }

    private Guid ReadDataFile()
    {
        Guid res;

        using FileStream fs = new(_path, FileMode.Open, FileAccess.Read);
        {
            byte[] data = new byte[fs.Length];
            int remain = (int)fs.Length;
            int offset = 0;
            while (remain > 0)
            {
                int n = fs.Read(data, offset, remain);

                if (n == 0)
                    break;

                offset += n;
                remain -= n;
            }
            res = new Guid(data);
        }

        return res;
    }
}