// MockWebApi.Library

namespace MockWebApi.Library;

public record MockDemoResponse(
	Guid Id,
	string Name,
	string Description,
	MockDemoData Data,
	DateTime CriticalDate,
	DateTime ModifiedDate,
	bool IsValid);