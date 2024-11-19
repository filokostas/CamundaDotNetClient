namespace CamundaClient.Infrastructure.Interfaces;
public interface IJsonSerializer
{
    string Serialize<T>(T obj);
    T Deserialize<T>(string json);
}
