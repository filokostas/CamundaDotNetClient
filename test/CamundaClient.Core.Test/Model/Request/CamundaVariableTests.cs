using CamundaClient.Application.Dtos.Requests;

namespace CamundaClient.Core.Test.Model.Request;
public class CamundaVariableTests
{
    [Fact]
    public void Create_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var value = new { Name = "Test" };
        var type = "String";
        var valueInfo = new Dictionary<string, object> { { "infoKey", "infoValue" } };
        var local = true;

        // Act
        var camundaVariable = CamundaVariable.CreateTriggerVariable(value, type, valueInfo, local);

        // Assert
        Assert.Equal(value, camundaVariable.Value);
        Assert.Equal(type, camundaVariable.Type);
        Assert.Equal(valueInfo, camundaVariable.ValueInfo);
        Assert.True(camundaVariable.Local);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenValueIsNull()
    {
        // Arrange
        object? value = null;
        string? type = "String";
        var valueInfo = new Dictionary<string, object> { { "infoKey", "infoValue" } };
        var local = true;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => CamundaVariable.CreateTriggerVariable(value!, type, valueInfo, local));
        Assert.Equal("value", exception.ParamName);
    }
}
