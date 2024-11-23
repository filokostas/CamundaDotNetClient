using CamundaClient.Application.Dtos.Requests;
using Newtonsoft.Json;

namespace CamundaClient.Infrastructure.Utilities.Tests;

public class CamundaVariableConverterTests
{
    [Fact]
    public void WriteJson_ShouldSerializeCamundaVariable()
    {
        // Arrange
        var variable = CamundaVariable.Create(
            value: "testValue",
            type: "String",
            valueInfo: new Dictionary<string, object> { { "infoKey", "infoValue" } },
            local: true
        );

        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new CamundaVariableConverter() },
            Formatting = Formatting.None
        };

        // Act
        var json = JsonConvert.SerializeObject(variable, settings);

        // Assert
        var expectedJson = "{\"value\":\"testValue\",\"type\":\"String\",\"valueInfo\":{\"infoKey\":\"infoValue\"},\"local\":true}";
        Assert.Equal(expectedJson, json);
    }

    [Fact]
    public void ReadJson_ShouldDeserializeCamundaVariable()
    {
        // Arrange
        var json = "{\"value\":\"testValue\",\"type\":\"String\",\"valueInfo\":{\"infoKey\":\"infoValue\"},\"local\":true}";
        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new CamundaVariableConverter() }
        };

        // Act
        var variable = JsonConvert.DeserializeObject<CamundaVariable>(json, settings);

        // Assert
        Assert.NotNull(variable);
        Assert.Equal("testValue", variable.Value);
        Assert.Equal("String", variable.Type);
        Assert.NotNull(variable.ValueInfo);
        Assert.Equal("infoValue", variable.ValueInfo["infoKey"]);
        Assert.True(variable.Local);
    }

    [Fact]
    public void ReadJson_ShouldThrowException_WhenValueIsNull()
    {
        // Arrange
        var json = "{\"type\":\"String\"}";
        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new CamundaVariableConverter() }
        };

        // Act & Assert
        Assert.Throws<JsonSerializationException>(() =>
            JsonConvert.DeserializeObject<CamundaVariable>(json, settings)
        );
    }
}
