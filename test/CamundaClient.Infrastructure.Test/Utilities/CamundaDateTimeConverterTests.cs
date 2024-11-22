using CamundaClient.Infrastructure.Utilities;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace CamundaClient.Infrastructure.Test.Utilities;
public class CamundaDateTimeConverterTests
{
    private readonly JsonSerializerSettings _serializerSettings;

    public CamundaDateTimeConverterTests()
    {
        _serializerSettings = new JsonSerializerSettings
        {
            Converters = { new CamundaDateTimeConverter() },
            ContractResolver = new CamelCasePropertyNamesContractResolver(), // Use camelCase property names
            DateParseHandling = DateParseHandling.None // Prevent automatic date parsing
        };
    }

    [Fact]
    public void Should_SerializeDateTime_ToCamundaDateFormat()
    {
        // Arrange
        var dateTime = new DateTime(2021, 12, 31, 23, 59, 59, 123, DateTimeKind.Local);

        // Act
        var json = JsonConvert.SerializeObject(dateTime, _serializerSettings);

        // Assert
        string expectedDateString = dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", CultureInfo.InvariantCulture);
        expectedDateString = RemoveTimezoneColon(expectedDateString);
        json.Should().Be($"\"{expectedDateString}\"");
    }

    [Fact]
    public void Should_DeserializeDateTime_FromCamundaDateFormat()
    {
        // Arrange
        var dateTimeString = "2021-12-31T23:59:59.123+0100";
        var json = $"\"{dateTimeString}\"";

        // Act
        var dateTime = JsonConvert.DeserializeObject<DateTime>(json, _serializerSettings);

        // Assert
        var adjustedDateString = AddTimezoneColon(dateTimeString);
        var expectedDateTime = DateTime.ParseExact(
            adjustedDateString,
            "yyyy-MM-dd'T'HH:mm:ss.fffK",
            CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal);

        dateTime.Should().Be(expectedDateTime);
    }

    [Fact]
    public void Should_HandleTimezoneOffsets_Correctly()
    {
        // Arrange
        var dateTimeString = "2021-12-31T23:59:59.123-0500";
        var json = $"\"{dateTimeString}\"";

        // Act
        var dateTime = JsonConvert.DeserializeObject<DateTime>(json, _serializerSettings);

        // Assert
        var expectedDateTime = DateTime.ParseExact(
            dateTimeString,
            "yyyy-MM-dd'T'HH:mm:ss.fffK",
            CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal);

        // Convert both DateTimes to UTC before comparison
        dateTime.ToUniversalTime().Should().Be(expectedDateTime);
    }

    [Fact]
    public void Should_SerializeTaskQuery_WithFormattedDate()
    {
        // Arrange
        var taskQuery = new TaskQuery
        {
            DueDate = new DateTime(2021, 12, 31, 23, 59, 59, 123, DateTimeKind.Local),
            Assignee = "john.doe"
        };

        // Act
        var json = JsonConvert.SerializeObject(taskQuery, _serializerSettings);

        // Assert
        string expectedDateString = taskQuery.DueDate.Value.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", CultureInfo.InvariantCulture);
        expectedDateString = RemoveTimezoneColon(expectedDateString);

        json.Should().Contain($"\"dueDate\":\"{expectedDateString}\"");
        json.Should().Contain("\"assignee\":\"john.doe\"");
    }

    private string RemoveTimezoneColon(string dateTime)
    {
        // Same as in the converter
        if (dateTime.Length > 6 && (dateTime[^6] == '+' || dateTime[^6] == '-'))
        {
            return dateTime.Remove(dateTime.Length - 3, 1);
        }
        return dateTime;
    }

    private string AddTimezoneColon(string dateTime)
    {
        // Same as in the converter
        if (dateTime.Length > 5 && (dateTime[^5] == '+' || dateTime[^5] == '-'))
        {
            return dateTime.Insert(dateTime.Length - 2, ":");
        }
        return dateTime;
    }
}

public class TaskQuery
{
    public DateTime? DueDate { get; set; }
    public string Assignee { get; set; }
}
