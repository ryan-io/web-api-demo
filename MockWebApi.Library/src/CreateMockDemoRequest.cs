// MockWebApi.Library

namespace MockWebApi.Library;

public record CreateMockDemoRequest(
	string Name,
	string Description,
	MockDemoData Data,
	DateTime CriticalDate,
	DateTime CreationDate);