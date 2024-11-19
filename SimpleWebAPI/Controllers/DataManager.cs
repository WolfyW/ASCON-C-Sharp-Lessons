using System.Text.Json;

namespace SimpleWebAPI.Controllers;

public class DataManager
{
    public int LastFreeIndex { get; private set; } = 0;
    public Dictionary<int, Data> DataStorage { get; private set; } = new Dictionary<int, Data>();

    public Data AddData(string dataStr)
    {
        var data = JsonSerializer.Deserialize<Data>(dataStr);
        return AddDataInternal(data);
    }

    public Data AddData(Data data)
    {
        return AddDataInternal(data);
    }

    private Data AddDataInternal(Data data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }
        data.Id = LastFreeIndex;
        DataStorage.Add(LastFreeIndex, data);
        LastFreeIndex++;

        return data;
    }

    public Data GetData(int index)
    {
        if (DataStorage.ContainsKey(index))
        {
            return DataStorage[index];
        }

        throw new ArgumentException("Нет такой информации");
    }        
}
