using CamundaClient.Core.Models.Requests;

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
        var camundaVariable = CamundaVariable.Create(value, type, valueInfo, local);

        // Assert
        Assert.Equal(value, camundaVariable.Value);
        Assert.Equal(type, camundaVariable.Type);
        Assert.Equal(valueInfo, camundaVariable.ValueInfo);
        Assert.True(camundaVariable.Local);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenTypeIsNull()
    {
        // Arrange
        var value = new { Name = "Test" };
        string type = null;
        var valueInfo = new Dictionary<string, object> { { "infoKey", "infoValue" } };
        var local = true;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => CamundaVariable.Create(value, type, valueInfo, local));
        Assert.Equal("type", exception.ParamName);
    }
}
