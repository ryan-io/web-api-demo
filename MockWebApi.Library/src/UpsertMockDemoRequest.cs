// MockWebApi.Library

namespace MockWebApi.Library;

public record UpsertMockDemoRequest(
	string Name,
	string Description,
	MockDemoData Data,
	DateTime CriticalDate,
	DateTime ModifiedDate,
	bool IsValid);