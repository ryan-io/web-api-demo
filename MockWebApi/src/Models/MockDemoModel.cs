// MockWebApi.Library

using MockWebApi.Library;
using ErrorOr;
using MockWebApi.ServiceErrors;

namespace MockWebApi.Models;

public class MockDemoModel {
	public const int MAX_DESC_LENGTH = 200;

	public static ErrorOr<MockDemoModel> CreateNew(string name, string description, MockDemoData data, DateTime
		criticalDate) {
		return Validate(Guid.NewGuid(), ref name, ref description, ref data, criticalDate);
	}

	public static ErrorOr<MockDemoModel> CreateClone(Guid id, string name, string description, MockDemoData data,
		DateTime criticalDate) {
		return Validate(id, ref name, ref description, ref data, criticalDate);
	}

	static ErrorOr<MockDemoModel> Validate(Guid id, ref string name, ref string description, ref MockDemoData 
            data, DateTime criticalDate) {
		if (string.IsNullOrEmpty(name))
			return Errors.Models.NameEmpty();

		if (description.Length > MAX_DESC_LENGTH)
			return Errors.Models.DescriptionTooLong(MAX_DESC_LENGTH);

		if (data.IntegerData < 0)
			return Errors.Models.IntegerDataLessThanZero();

		return new MockDemoModel(id, name, description, data, criticalDate);;
	}

	MockDemoModel(Guid id, string name, string description, MockDemoData data, DateTime criticalDate) {
		Id           = id;
		Name         = name;
		Description  = description;
		Data         = data;
		CriticalDate = criticalDate;
	}

	public Guid         Id           { get; }
	public string       Name         { get; }
	public string       Description  { get; }
	public MockDemoData Data         { get; }
	public DateTime     CriticalDate { get; }
}